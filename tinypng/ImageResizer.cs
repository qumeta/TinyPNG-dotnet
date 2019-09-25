using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using TinifyAPI;

namespace tinypng
{
    [Command(
          Name = "dotnet tinypng",
          FullName = "dotnet-tinypng",
          Description = "Uses the TinyPNG API to compress images")]
    [HelpOption]
    public partial class ImageResizer
    {
        [Required(ErrorMessage = "You must specify the path to a directory or file to compress")]
        [Argument(0, Name = "path", Description = "Path to the file or directory to compress")]
        [FileOrDirectoryExists]
        public string Path { get; }

        [Option(CommandOptionType.SingleValue, Description = "Your TinyPNG API key")]
        public string ApiKey { get; }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            if (!await SetApiKeyAsync(console))
            {
                app.ExtendedHelpText = Constants.ExtendedHelpText();
                app.ShowHelp();
                return Program.ERROR;
            }

            var squasher = new ImageSquasher(console);
            await squasher.SquashFileAsync(GetFilesToSquash(console, Path));

            console.WriteLine($"Compression complete.");
            console.WriteLine($"{Tinify.CompressionCount} compressions this month");

            return Program.OK;
        }

        string readApikey()
        {
            // .tinypny/apikey
            var file = Constants.ApiKeySettingFile;
            if (File.Exists(file))
                return File.ReadAllText(file);

            return "";
        }

        void writeApikey(string apiKey)
        {
            var file = Constants.ApiKeySettingFile;
            Directory.CreateDirectory(new FileInfo(file).Directory.FullName);
            File.WriteAllText(Constants.ApiKeySettingFile, apiKey);
        }

        async Task<bool> SetApiKeyAsync(IConsole console)
        {
            try
            {
                var apiKey = string.IsNullOrEmpty(ApiKey)
                    ? this.readApikey()
                    : ApiKey;

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    console.Error.WriteLine("Error: No API Key provided");
                    return false;
                }

                Tinify.Key = apiKey;
                await Tinify.Validate();
                console.WriteLine("TinyPng API Key verified");

                // save 
                this.writeApikey(apiKey);
                return true;
            }
            catch (System.Exception ex)
            {
                console.Error.WriteLine("Validation of TinyPng API key failed.");
                console.Error.WriteLine(ex);
                return false;
            }
        }

        static string[] GetFilesToSquash(IConsole console, string path)
        {
            console.WriteLine($"Checking '{path}'...");
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                console.WriteLine($"Path '{path}' is a directory, compressing all files");
                return Directory.GetFiles(path);
            }
            else
            {
                console.WriteLine($"Path '{path}' is a file");
                return new[] { path };
            }
        }
    }
}

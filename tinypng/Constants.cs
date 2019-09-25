using System;
using System.Collections.Generic;

namespace tinypng
{
    public class Constants
    {
        public static string ApiKeySettingFile { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.tinypng\apikey";

        public static ISet<string> SupportedExtensions { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".png",
            ".jpeg",
            ".jpg",
        };

        public static string ExtendedHelpText() { return @"
You must provide your TinyPNG API key to use this tool 
(see https://tinypng.com/developers for details). This 
can be provided either as an argument, or by saving the 
apikey in " + ApiKeySettingFile + @" file. 
Only png, jpeg, and jpg, extensions are supported
";}
    }
}

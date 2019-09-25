# TinyPNG-dotnet

A simple tool for compressing PNG and JPEG files using the [TinyPNG API](https://tinypng.com/).

Can compress all the PNG or JPEG files in a directory or just single files.

## Installation

The latest release of dotnet-tinypng requires the [3.0.100](https://www.microsoft.com/net/download/dotnet-core/sdk-3.0.100) .NET Core 3.0 SDK or newer.
Once installed, run this command:

```
dotnet tool install --global dotnet-tinypng
```

## Usage

```
Usage: dotnet tinypng [arguments] [options]

Arguments:
  path  Path to the file or directory to squash

Options:
  -?|-h|--help            Show help information
  -a|--api-key <API_KEY>  Your TinyPNG API key

You must provide your TinyPNG API key to use this tool
(see https://tinypng.com/developers for details).
Only png, jpeg, and jpg, extensions are supported.
```

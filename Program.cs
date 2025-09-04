using System;
using System.Drawing;
using ImgToGBX.Services;
using ImgToGBX.Models;

try
{
    // Parse command line arguments
    var commandLineParser = new CommandLineParserService();
    var config = commandLineParser.ParseArguments(args);
    
    // Validate configuration
    config.Validate();
    
    // Initialize services
    var blockConfigService = new BlockConfigurationService(config.ConfigFilePath);
    var imageProcessorService = new ImageProcessorService();
    var mapGeneratorService = new MapGeneratorService(blockConfigService);

    Console.WriteLine("ImgToGBX - Image to TrackMania Map Converter");
    Console.WriteLine("==========================================");
    Console.WriteLine($"Input Image: {config.InputImagePath}");
    Console.WriteLine($"Output Map: {config.OutputMapPath}");
    Console.WriteLine($"Resolution: {config.ResolutionX}x{config.ResolutionY}");
    Console.WriteLine($"Config File: {config.ConfigFilePath}");
    Console.WriteLine();

    // Load the base map
    var map = mapGeneratorService.LoadMap(config.InputMapPath);
    
    // Read and resize the image
    var resizedImage = imageProcessorService.ReadAndResizeImage(
        config.InputImagePath, 
        config.ResolutionX, 
        config.ResolutionY
        );
    
    // Generate the map from the image
    mapGeneratorService.GenerateMapFromImage(resizedImage, map);
    
    // Save the final map
    mapGeneratorService.SaveMap(map, config.OutputMapPath);
    
    Console.WriteLine($"✅ Successfully generated map: {config.OutputMapPath}");
    Console.WriteLine($"📁 Map saved to: {config.OutputMapPath}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Configuration Error: {ex.Message}");
    Console.WriteLine();
    Console.WriteLine("Use --help or -h for usage information.");
    Environment.Exit(1);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"File Not Found: {ex.Message}");
    Console.WriteLine();
    Console.WriteLine("Make sure the required files exist:");
    Console.WriteLine("  - Input image in the 'imgs/' directory");
    Console.WriteLine("  - Base map in the 'map/' directory");
    Console.WriteLine("  - Configuration file in the 'config/' directory");
    Environment.Exit(1);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine();
    Console.WriteLine("Use --help or -h for usage information.");
    Environment.Exit(1);
}





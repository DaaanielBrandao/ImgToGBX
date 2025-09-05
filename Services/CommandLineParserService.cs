using ImgToGBX.Models;

namespace ImgToGBX.Services
{
    public class CommandLineParserService
    {
        public ApplicationConfig ParseArguments(string[] args)
        {
            var config = new ApplicationConfig();

            // Check for help first
            if (args.Contains("--help") || args.Contains("-h"))
            {
                ShowHelp();
                Environment.Exit(0);
            }

            // Parse arguments
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--image-path":
                    case "-i":
                        if (i + 1 < args.Length)
                            config.InputImagePath = args[++i];
                        break;
                    case "--output-map-path":
                    case "-o":
                        if (i + 1 < args.Length)
                            config.OutputMapPath = args[++i];
                        break;
                    case "--input-map-path":
                    case "-b":
                        if (i + 1 < args.Length)
                            config.InputMapPath = args[++i];
                        break;
                    case "--resolution-x":
                    case "-x":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int x))
                            config.ResolutionX = x;
                        break;
                    case "--resolution-y":
                    case "-y":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int y))
                            config.ResolutionY = y;
                        break;
                    case "--config-file":
                    case "-c":
                        if (i + 1 < args.Length)
                            config.ConfigFilePath = args[++i];
                        break;
                    case "--height":
                    case "-z":
                        if (i + 1 < args.Length && int.TryParse(args[++i], out int height))
                            config.Height = height;
                        break;
                }
            }

            return config;
        }

        private void ShowHelp()
        {
            Console.WriteLine("ImgToGBX - Convert images to TrackMania maps");
            Console.WriteLine();
            Console.WriteLine("Usage: ImgToGBX [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -i, --image-path <path>        Path of the input image file (default: Imgs/example.png.png)");
            Console.WriteLine("  -o, --output-map-path <path>   Path for the output map (default: OutputMap/test)");
            Console.WriteLine("  -b, --input-map-path <path>    Path of the base map to use (default: nothing)");
            Console.WriteLine("  -x, --resolution-x <number>    Width resolution for the image (max: 256, default: 48)");
            Console.WriteLine("  -y, --resolution-y <number>    Height resolution for the image (max: 256, default: 48)");
            Console.WriteLine("  -z, --height <number>          Height (Y coordinate) for placing blocks (default: 10)");
            Console.WriteLine("  -c, --config-file <path>       Path to the color-to-block mapping configuration file");
            Console.WriteLine("                                 (default: config/color-to-block-mapping.json)");
            Console.WriteLine("  -h, --help                     Show this help information");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  ImgToGBX");
            Console.WriteLine("  ImgToGBX -i myimage.png -o mymap -x 32 -y 32");
            Console.WriteLine("  ImgToGBX --image-path test.png --output-map-path result --resolution-x 24 --resolution-y 24");
        }
    }
}

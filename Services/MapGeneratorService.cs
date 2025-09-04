using GBX.NET;
using GBX.NET.Engines.Game;
using GBX.NET.LZO;
using ImgToGBX.Models;
using System.Drawing;


namespace ImgToGBX.Services
{
    public class MapGeneratorService
    {
        private readonly BlockConfigurationService _blockConfigService;
        private readonly ApplicationConfig _config;

        public MapGeneratorService(BlockConfigurationService blockConfigService, ApplicationConfig config)
        {
            _blockConfigService = blockConfigService;
            _config = config;
        }

        public CGameCtnChallenge LoadMap(string mapPath)
        {
            if (!File.Exists(mapPath))
            {
                throw new FileNotFoundException($"Map file not found: {mapPath}");
            }

            Gbx.LZO = new Lzo();
            var gbx = Gbx.Parse<CGameCtnChallenge>(mapPath);
            return gbx.Node;
        }

        public void GenerateMapFromImage(Bitmap image, CGameCtnChallenge map)
        {
            using (image)
            {
                var pixelData = new ImageProcessorService().GetPixelData(image);
                
                foreach (var (x, y, pixelColor) in pixelData)
                {
                    var colorMapping = _blockConfigService.FindClosestColorMapping(
                        pixelColor.R, pixelColor.G, pixelColor.B);

                    if (colorMapping != null)
                    {
                        CGameCtnBlock block = CreateBlock(colorMapping, x, y);
                        map.PlaceBlock(block);
                    }
                }
            }
        }

        private CGameCtnBlock CreateBlock(ColorMapping colorMapping, int x, int y)
        {
            CGameCtnBlock block = new CGameCtnBlock();
            block.Coord = new Int3(x, _config.Height, y);
            block.Name = colorMapping.BlockName;
            block.Color = colorMapping.GetDifficultyColor();            
            return block;
        }

        public void SaveMap(CGameCtnChallenge map, string outputPath)
        {
            // Ensure output directory exists
            string? outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            map.MapName = outputPath.Substring(0, outputPath.Length - ".Map.Gbx".Length);;
            map.Save(outputPath);
        }
    }
}

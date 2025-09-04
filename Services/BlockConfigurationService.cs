using System.Text.Json;
using ImgToGBX.Models;

namespace ImgToGBX.Services
{
    public class BlockConfigurationService
    {
        private readonly List<ColorMapping> _colorMappings;

        public BlockConfigurationService(string configFilePath = "config/color-to-block-mapping.json")
        {
            _colorMappings = LoadColorMappings(configFilePath);
        }

        private List<ColorMapping> LoadColorMappings(string configFilePath)
        {
            try
            {
                if (!File.Exists(configFilePath))
                {
                    throw new FileNotFoundException($"Configuration file not found: {configFilePath}");
                }

                string jsonContent = File.ReadAllText(configFilePath);
                var config = JsonSerializer.Deserialize<ColorMappingConfig>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return config?.ColorMappings ?? new List<ColorMapping>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load color mappings from {configFilePath}: {ex.Message}", ex);
            }
        }

        public List<ColorMapping> GetColorMappings()
        {
            return _colorMappings.ToList();
        }

        public ColorMapping? FindClosestColorMapping(int r, int g, int b)
        {
            if (!_colorMappings.Any())
                return null;

            double minDistance = double.MaxValue;
            ColorMapping? closestMapping = null;

            foreach (var mapping in _colorMappings)
            {
                var (blockR, blockG, blockB) = mapping.GetRgbValues();
                
                double distance = Math.Sqrt(
                    Math.Pow(blockR - r, 2) +
                    Math.Pow(blockG - g, 2) +
                    Math.Pow(blockB - b, 2)
                );

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMapping = mapping;
                }
            }

            return closestMapping;
        }
    }
}

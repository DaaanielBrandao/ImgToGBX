namespace ImgToGBX.Models
{
    public class ApplicationConfig
    {
        public string InputImagePath { get; set; } = "Imgs/example.png";
        public string OutputMapPath { get; set; } = "OutputMap/test.Map.Gbx";
        public string InputMapPath { get; set; } = "InputMap/nothing.Map.Gbx";
        public int ResolutionX { get; set; } = 48;
        public int ResolutionY { get; set; } = 48;
        public string ConfigFilePath { get; set; } = "config/color-to-block-mapping.json";
        public bool ShowHelp { get; set; } = false;

        public void Validate()
        {
            if (ResolutionX > 48 || ResolutionY > 48)
            {
                throw new ArgumentException("Resolution cannot exceed 48x48 pixels");
            }

            if (ResolutionX <= 0 || ResolutionY <= 0)
            {
                throw new ArgumentException("Resolution must be positive");
            }

            if (string.IsNullOrWhiteSpace(InputImagePath))
            {
                throw new ArgumentException("Image Path cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(OutputMapPath))
            {
                throw new ArgumentException("Output map Path cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(InputMapPath))
            {
                throw new ArgumentException("Input map name cannot be empty");
            }
        }
    }
}


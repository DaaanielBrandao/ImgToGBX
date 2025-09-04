using GBX.NET.Engines.Game;
using System;
using GBX.NET;
using GBX.NET.Engines.Game;
using GBX.NET.LZO;
using System.Drawing;
using System.Numerics;


namespace ImgToGBX.Models
{
    public class ColorMapping
    {
        public string HexColor { get; set; } = string.Empty;
        public string BlockName { get; set; } = string.Empty;
        public string DiffColor { get; set; } = string.Empty;

        public DifficultyColor GetDifficultyColor()
        {
            return DiffColor.ToLower() switch
            {
                "default" => DifficultyColor.Default,
                "white" => DifficultyColor.White,
                "green" => DifficultyColor.Green,
                "blue" => DifficultyColor.Blue,
                "red" => DifficultyColor.Red,
                "black" => DifficultyColor.Black,
                _ => DifficultyColor.Default
            };
        }

        public (int R, int G, int B) GetRgbValues()
        {
            if (HexColor.Length != 6)
                throw new ArgumentException("Hex color must be 6 characters long");

            int r = Convert.ToInt32(HexColor.Substring(0, 2), 16);
            int g = Convert.ToInt32(HexColor.Substring(2, 2), 16);
            int b = Convert.ToInt32(HexColor.Substring(4, 2), 16);

            return (r, g, b);
        }
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImgToGBX.Services
{
    public class ImageProcessorService
    {
        public Bitmap ReadAndResizeImage(string inputImagePath, int width, int height)
        {
            if (!File.Exists(inputImagePath))
            {
                throw new FileNotFoundException($"Input image not found: {inputImagePath}");
            }

            using (Image originalImage = Image.FromFile(inputImagePath))
            {
                Bitmap resizedImage = ResizeImage(originalImage, width, height);         
                return resizedImage;
            }
        }

        private static Bitmap ResizeImage(Image originalImage, int width, int height)
        {
            // Create a new bitmap with the desired size
            Bitmap resizedBitmap = new Bitmap(width, height);
            
            // Use a graphics object to draw the original image onto the resized bitmap
            using (Graphics graphics = Graphics.FromImage(resizedBitmap))
            {
                // Set quality settings for the resizing
                graphics.CompositingQuality = CompositingQuality.AssumeLinear;
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                
                // Draw the resized image
                graphics.DrawImage(originalImage, 0, 0, width, height);
            }
            
            // Return the resized Bitmap object
            return resizedBitmap;
        }

        public IEnumerable<(int X, int Y, Color Color)> GetPixelData(Bitmap image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    yield return (x, y, pixelColor);
                }
            }
        }
    }
}

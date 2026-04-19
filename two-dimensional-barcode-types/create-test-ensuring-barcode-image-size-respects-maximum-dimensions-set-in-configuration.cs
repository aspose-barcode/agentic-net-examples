using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Maximum dimensions in points (1 point = 1/72 inch)
        float maxWidthPoints = 200f;
        float maxHeightPoints = 100f;

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Test123";

            // Use Interpolation mode so ImageWidth/ImageHeight are respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the maximum image size
            generator.Parameters.ImageWidth.Point = maxWidthPoints;
            generator.Parameters.ImageHeight.Point = maxHeightPoints;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Convert max size from points to pixels using the current resolution (default 96 dpi)
                float dpi = generator.Parameters.Resolution;
                int maxWidthPixels = (int)Math.Ceiling(maxWidthPoints * dpi / 72f);
                int maxHeightPixels = (int)Math.Ceiling(maxHeightPoints * dpi / 72f);

                // Verify the generated image does not exceed the configured maximum dimensions
                if (actualWidth > maxWidthPixels || actualHeight > maxHeightPixels)
                {
                    throw new InvalidOperationException(
                        $"Generated image size {actualWidth}x{actualHeight} exceeds the maximum {maxWidthPixels}x{maxHeightPixels} pixels.");
                }

                // Save the image (optional, for visual verification)
                const string outputPath = "barcode_test.png";
                bitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Barcode generated with size {actualWidth}x{actualHeight} pixels and saved to {outputPath}.");
            }
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, applying padding to accommodate rotation,
/// rotating the barcode, and saving the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, calculates necessary padding for a 45° rotation,
    /// applies the padding and rotation, and saves the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode
        const string codeText = "1234567890";

        // Desired rotation angle in degrees
        const float rotationAngle = 45f;

        // Initialize the barcode generator with Code128 symbology and the sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set basic barcode appearance parameters
            generator.Parameters.Barcode.XDimension.Point = 2f;      // Width of the smallest bar element
            generator.Parameters.Barcode.BarHeight.Point = 40f;    // Height of the barcode bars
            generator.Parameters.AutoSizeMode = AutoSizeMode.None; // Disable automatic sizing

            // Generate a temporary image to retrieve the original dimensions (width & height)
            using (var tempImage = generator.GenerateBarCodeImage())
            {
                int originalWidth = tempImage.Width;
                int originalHeight = tempImage.Height;

                // Convert rotation angle to radians for trigonometric calculations
                double rad = rotationAngle * Math.PI / 180.0;

                // Compute absolute cosine and sine values for the rotation
                double cos = Math.Abs(Math.Cos(rad));
                double sin = Math.Abs(Math.Sin(rad));

                // Determine the dimensions of the bounding box after rotation
                double newWidth = originalWidth * cos + originalHeight * sin;
                double newHeight = originalWidth * sin + originalHeight * cos;

                // Calculate the required padding to center the rotated barcode
                float padX = (float)((newWidth - originalWidth) / 2.0);
                float padY = (float)((newHeight - originalHeight) / 2.0);

                // Apply symmetric padding on all sides of the barcode
                generator.Parameters.Barcode.Padding.Left.Point = padX;
                generator.Parameters.Barcode.Padding.Right.Point = padX;
                generator.Parameters.Barcode.Padding.Top.Point = padY;
                generator.Parameters.Barcode.Padding.Bottom.Point = padY;

                // Set the rotation angle for the barcode image
                generator.Parameters.RotationAngle = rotationAngle;

                // Define the output file path in the system's temporary directory
                string outputPath = Path.Combine(Path.GetTempPath(), "rotated_barcode.png");

                // Save the final rotated barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);

                // Inform the user where the image was saved
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
    }
}
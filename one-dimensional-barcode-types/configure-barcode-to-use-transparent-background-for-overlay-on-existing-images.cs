using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a base image, generating a barcode with a transparent background,
/// overlaying the barcode onto the base image, and saving the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the final image.
        string outputPath = "barcode_overlay.png";

        // Create a blank base image (400x200) with a white background.
        using (var baseImage = new Bitmap(400, 200, PixelFormat.Format32bppArgb))
        {
            // Fill the entire base image with white color.
            using (var graphics = Graphics.FromImage(baseImage))
            {
                graphics.Clear(Color.White);
            }

            // Initialize a barcode generator for Code128 with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Make the barcode background transparent.
                generator.Parameters.BackColor = Color.Transparent;

                // Configure barcode size and scaling mode.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 80f;

                // Generate the barcode image.
                using (var barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Calculate coordinates to center the barcode on the base image.
                    int x = (baseImage.Width - barcodeImage.Width) / 2;
                    int y = (baseImage.Height - barcodeImage.Height) / 2;

                    // Draw the barcode onto the base image at the calculated position.
                    using (var graphics = Graphics.FromImage(baseImage))
                    {
                        graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);
                    }
                }
            }

            // Save the combined image as a PNG file.
            baseImage.Save(outputPath, ImageFormat.Png);
        }

        // Output the full path of the saved image to the console.
        Console.WriteLine($"Barcode overlay image saved to: {Path.GetFullPath(outputPath)}");
    }
}
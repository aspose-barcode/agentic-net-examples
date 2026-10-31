// Title: Embed a Logo into a Han Xin Barcode
// Description: Demonstrates how to generate a Han Xin barcode and overlay a centered logo image without compromising readability.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set error correction levels, and combine barcode images with custom graphics. Typical use cases include branding barcodes with company logos while maintaining scan reliability. Developers often need to embed images, adjust error correction, and export the final barcode in common image formats.
// Prompt: Provide option to embed logo image at center of Han Xin barcode without affecting readability.
// Tags: hanxin, barcode, logo, image overlay, error correction, aspose.barcode, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a centered logo into a Han Xin barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Han Xin barcode, adds a red square logo at its center,
    /// and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinLogoDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full path for the final image
        string finalImagePath = Path.Combine(outputFolder, "HanXin_With_Logo.png");

        // Generate Han Xin barcode with optional error correction to improve readability when a logo is added
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "Sample123"))
        {
            // Set error correction level (L2) to increase tolerance for the overlaid logo
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Produce the barcode image as a bitmap
            using (var barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (red square) of size 50x50 pixels
                using (var logoBmp = new Bitmap(50, 50))
                {
                    // Fill the logo bitmap with a solid red color
                    using (var logoGraphics = Graphics.FromImage(logoBmp))
                    {
                        logoGraphics.Clear(Color.Red);
                    }

                    // Calculate the position to center the logo on the barcode
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw the logo onto the barcode image at the calculated position
                    using (var graphics = Graphics.FromImage(barcodeBmp))
                    {
                        graphics.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image (barcode with embedded logo) as PNG
                    barcodeBmp.Save(finalImagePath, ImageFormat.Png);
                }
            }
        }

        // Output the location of the saved file
        Console.WriteLine("Barcode with embedded logo saved to:");
        Console.WriteLine(finalImagePath);
    }
}
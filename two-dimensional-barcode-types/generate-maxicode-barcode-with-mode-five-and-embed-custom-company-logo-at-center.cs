// Title: Generate MaxiCode Mode 5 Barcode with Embedded Logo
// Description: This example creates a MaxiCode barcode in mode 5, overlays a custom company logo at the center of the barcode image, and saves the result as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode generation of MaxiCode symbology combined with image compositing using Aspose.Drawing. Typical scenarios include adding branding to shipping labels or product packaging where MaxiCode is required. Developers often use BarcodeGenerator, MaxiCodeMode, and Graphics objects to customize barcode appearance.
/// Prompt: Generate a MaxiCode barcode with mode five and embed a custom company logo at the center.
/// Tags: maxicode, barcode generation, logo overlay, png, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a MaxiCode barcode (mode 5) and embed a custom logo at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, adds the logo, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode5_WithLogo.png");

        // --------------------------------------------------------------------
        // Create a simple placeholder logo (a solid red square) as a bitmap.
        // In real scenarios, replace this with a company logo loaded from a file.
        // --------------------------------------------------------------------
        using (Bitmap logo = new Bitmap(100, 100))
        {
            using (Graphics gLogo = Graphics.FromImage(logo))
            {
                // Fill the entire bitmap with red color.
                gLogo.Clear(Color.Red);
            }

            // --------------------------------------------------------------
            // Initialise the MaxiCode generator with the desired text payload.
            // --------------------------------------------------------------
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode Mode5"))
            {
                // Set the MaxiCode mode to Mode5 (used for specific data structures).
                generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;

                // Generate the barcode image as a bitmap.
                using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                {
                    // --------------------------------------------------------------
                    // Calculate logo dimensions: 25 % of the smaller barcode side.
                    // --------------------------------------------------------------
                    int logoSize = Math.Min(barcodeBmp.Width, barcodeBmp.Height) / 4;
                    int logoX = (barcodeBmp.Width - logoSize) / 2;
                    int logoY = (barcodeBmp.Height - logoSize) / 2;

                    // Draw the logo onto the barcode bitmap, centered.
                    using (Graphics g = Graphics.FromImage(barcodeBmp))
                    {
                        Rectangle destRect = new Rectangle(logoX, logoY, logoSize, logoSize);
                        g.DrawImage(logo, destRect);
                    }

                    // Save the combined image to the specified PNG file.
                    barcodeBmp.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"MaxiCode barcode with logo saved to: {outputPath}");
    }
}
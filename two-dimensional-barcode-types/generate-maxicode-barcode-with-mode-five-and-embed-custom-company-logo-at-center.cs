// Title: Generate MaxiCode Mode 5 barcode with embedded logo
// Description: Demonstrates creating a MaxiCode barcode in mode 5 and overlaying a custom company logo at its center, then saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology and image manipulation. It showcases the use of BarcodeGenerator, MaxiCodeMode, and Aspose.Drawing classes to produce a barcode, embed graphics, and export the result. Developers often need to combine barcodes with branding elements for packaging or shipping labels, and this snippet provides a clear pattern for such tasks.
// Prompt: Generate a MaxiCode barcode with mode five and embed a custom company logo at the center.
// Tags: maxicode, barcode generation, logo overlay, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode barcode (mode 5) and embeds a logo image at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, overlays the logo if present, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the output image and the optional logo
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode5.png");
        string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "logo.png");
        string codeText = "Sample Text";

        // Initialize the barcode generator for MaxiCode with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Configure the generator to use MaxiCode mode 5
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;

            // Generate the barcode image as a Bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // If a logo file exists, overlay it onto the barcode
                if (File.Exists(logoPath))
                {
                    // Load the logo image
                    using (Bitmap logoBitmap = (Bitmap)Image.FromFile(logoPath))
                    {
                        // Calculate coordinates to center the logo on the barcode
                        int x = (barcodeBitmap.Width - logoBitmap.Width) / 2;
                        int y = (barcodeBitmap.Height - logoBitmap.Height) / 2;

                        // Draw the logo onto the barcode bitmap
                        using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                        {
                            graphics.DrawImage(logoBitmap, x, y, logoBitmap.Width, logoBitmap.Height);
                        }
                    }
                }

                // Save the final image to the specified output path in PNG format
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeBitmap.Save(fs, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}
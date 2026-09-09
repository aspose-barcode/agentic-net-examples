// Title: Embed a Logo into a Han Xin Barcode Using Aspose.BarCode
// Description: Shows how to generate a Han Xin 2D barcode and overlay a centered logo image while preserving readability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and customization category. It demonstrates how to create a Han Xin barcode, configure error correction and version, and embed a custom logo at the center without affecting scanability. Developers commonly use these APIs to produce branded barcodes for packaging, marketing, or authentication scenarios.
// Prompt: Provide option to embed logo image at center of Han Xin barcode without affecting readability.
// Tags: hanxin, logo, embed, png, barcodegenerator, graphics

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a logo image into a Han Xin barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Han Xin barcode, draws a logo at its center, and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "HanXinLogoDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the logo image and the final barcode image
        string logoPath = Path.Combine(tempFolder, "logo.png");
        string finalPath = Path.Combine(tempFolder, "hanxin_with_logo.png");

        // --------------------------------------------------------------------
        // Create a simple logo image (red circle on a transparent background)
        // --------------------------------------------------------------------
        using (var logoBmp = new Bitmap(80, 80))
        {
            using (var graphics = Graphics.FromImage(logoBmp))
            {
                // Transparent background
                graphics.Clear(Color.Transparent);
                // Draw a solid red circle
                using (var brush = new SolidBrush(Color.Red))
                {
                    graphics.FillEllipse(brush, 0, 0, 80, 80);
                }
            }
            // Save the logo as PNG
            logoBmp.Save(logoPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------
        // Generate the Han Xin barcode with optional error correction
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "Sample123"))
        {
            // Set error correction level (L2 = 15%)
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            // Let the encoder automatically choose the smallest version
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Render the barcode to a bitmap
            using (var barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Load the previously created logo image
                using (var logoBmp = new Bitmap(logoPath))
                {
                    // Compute coordinates to center the logo on the barcode
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw the logo onto the barcode bitmap
                    using (var graphics = Graphics.FromImage(barcodeBmp))
                    {
                        graphics.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image as PNG
                    barcodeBmp.Save(finalPath, ImageFormat.Png);
                }
            }
        }

        // Output the location of the saved barcode image
        Console.WriteLine("Han Xin barcode with embedded logo saved to:");
        Console.WriteLine(finalPath);
    }
}
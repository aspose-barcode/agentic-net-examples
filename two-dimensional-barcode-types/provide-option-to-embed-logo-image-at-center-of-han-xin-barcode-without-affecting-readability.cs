// Title: Embed a logo into a Han Xin barcode image
// Description: Demonstrates how to generate a Han Xin barcode with Aspose.BarCode, overlay a custom logo at its center, and save the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image manipulation and error‑correction settings. It showcases the use of BarcodeGenerator, HanXin parameters, and Aspose.Drawing graphics to combine a barcode with additional graphics—common tasks when branding products or creating custom scanner‑friendly images.
// Prompt: Provide option to embed logo image at center of Han Xin barcode without affecting readability.
// Tags: hanxin, logo, embed, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Demonstrates embedding a logo at the center of a Han Xin barcode using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, overlays a logo, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinLogo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Path for the final barcode image
        string outputPath = Path.Combine(outputFolder, "HanXin_With_Logo.png");

        // Sample text to encode
        const string codeText = "HanXin Barcode with Logo";

        // Generate Han Xin barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set a moderate error correction level to tolerate the logo overlay
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Generate the barcode image
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (red circle with transparent background)
                using (Bitmap logo = new Bitmap(100, 100))
                {
                    using (Graphics gLogo = Graphics.FromImage(logo))
                    {
                        // Transparent background
                        gLogo.Clear(Color.Transparent);
                        gLogo.SmoothingMode = SmoothingMode.AntiAlias;

                        // Draw a red circle
                        using (SolidBrush brush = new SolidBrush(Color.Red))
                        {
                            gLogo.FillEllipse(brush, 0, 0, 100, 100);
                        }
                    }

                    // Calculate position to center the logo on the barcode
                    int posX = (barcodeImage.Width - logo.Width) / 2;
                    int posY = (barcodeImage.Height - logo.Height) / 2;

                    // Draw the logo onto the barcode image
                    using (Graphics gBarcode = Graphics.FromImage(barcodeImage))
                    {
                        gBarcode.DrawImage(logo, posX, posY, logo.Width, logo.Height);
                    }
                }

                // Save the combined image as PNG
                barcodeImage.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the image was saved
        Console.WriteLine("Barcode with embedded logo saved to:");
        Console.WriteLine(outputPath);
    }
}
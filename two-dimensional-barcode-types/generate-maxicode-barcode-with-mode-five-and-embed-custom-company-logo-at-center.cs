// Title: Generate MaxiCode Mode 5 Barcode with Embedded Logo
// Description: Creates a MaxiCode barcode in mode 5, overlays a custom 100x100 logo at the centre, and saves the result as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode's ComplexBarcodeGenerator for creating MaxiCode symbols, a high‑density 2‑D barcode used in logistics. It shows how to configure MaxiCodeStandardCodetext, generate the barcode image, and embed additional graphics (e.g., a company logo) using Aspose.Drawing. Typical use cases include shipping labels, parcel tracking, and retail inventory where a visual brand element is required alongside the barcode.
// Prompt: Generate a MaxiCode barcode with mode five and embed a custom company logo at the center.
// Tags: maxicode, barcode, logo, image overlay, complexbarcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a MaxiCode barcode (mode 5) and embed a custom logo at its centre.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, overlays the logo, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final PNG image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode_mode5.png");

        // Prepare MaxiCode standard codetext for mode 5.
        var maxiCodeData = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode5,
            Message = "Sample Data for MaxiCode Mode 5"
        };

        // Generate the MaxiCode barcode image using ComplexBarcodeGenerator.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            using (Bitmap barcodeBitmap = complexGenerator.GenerateBarCodeImage())
            {
                // Create a simple 100x100 logo bitmap programmatically.
                int logoSize = 100;
                using (Bitmap logoBitmap = new Bitmap(logoSize, logoSize))
                {
                    using (Graphics gLogo = Graphics.FromImage(logoBitmap))
                    {
                        // Fill the logo background with white.
                        gLogo.Clear(Color.White);

                        // Draw a solid blue square covering the entire logo area.
                        using (Brush blueBrush = new SolidBrush(Color.Blue))
                        {
                            gLogo.FillRectangle(blueBrush, 0, 0, logoSize, logoSize);
                        }

                        // Render the word "Logo" in white using a cross‑platform font.
                        using (Font font = new Font("Helvetica", 12f, FontStyle.Bold))
                        using (Brush whiteBrush = new SolidBrush(Color.White))
                        {
                            gLogo.DrawString("Logo", font, whiteBrush, new PointF(10, 40));
                        }
                    }

                    // Overlay the logo onto the centre of the barcode image.
                    using (Graphics gBarcode = Graphics.FromImage(barcodeBitmap))
                    {
                        int x = (barcodeBitmap.Width - logoBitmap.Width) / 2;
                        int y = (barcodeBitmap.Height - logoBitmap.Height) / 2;
                        gBarcode.DrawImage(logoBitmap, x, y, logoBitmap.Width, logoBitmap.Height);
                    }
                }

                // Save the final image with the embedded logo to the specified file.
                using (var outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeBitmap.Save(outStream, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"MaxiCode barcode with embedded logo saved to: {outputPath}");
    }
}
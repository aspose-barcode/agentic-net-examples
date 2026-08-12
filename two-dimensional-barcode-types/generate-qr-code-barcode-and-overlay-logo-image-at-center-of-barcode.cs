// Title: Generate QR Code with Centered Logo Overlay
// Description: Demonstrates creating a QR Code barcode, applying high error correction, and overlaying a custom logo image at the center, then saving as PNG.
// Category-Description: This example belongs to the barcode generation and image manipulation category of Aspose.BarCode. It showcases using BarcodeGenerator to create QR codes, adjusting QR error correction levels, and combining generated barcodes with graphics via Aspose.Drawing. Developers often need to embed logos or branding into QR codes while maintaining scannability, and this pattern illustrates the typical workflow.
// Prompt: Generate QR Code barcode and overlay a logo image at center of barcode.
// Tags: qr code, logo overlay, barcode generation, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and overlaying a logo at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, adds logo, saves PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_with_logo.png");

        // Initialize QR code generator with the desired text/content
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level to ensure the QR remains readable after logo overlay
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the QR code as a bitmap image
            using (Bitmap qrBitmap = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (100x100) with a white background and a red ellipse
                using (Bitmap logoBitmap = new Bitmap(100, 100))
                {
                    using (Graphics gLogo = Graphics.FromImage(logoBitmap))
                    {
                        // Fill background with white
                        gLogo.Clear(Color.White);
                        // Draw a red ellipse as the logo shape
                        using (Pen pen = new Pen(Color.Red, 5f))
                        {
                            gLogo.DrawEllipse(pen, 10, 10, 80, 80);
                        }
                    }

                    // Overlay the logo onto the center of the QR code bitmap
                    using (Graphics g = Graphics.FromImage(qrBitmap))
                    {
                        int x = (qrBitmap.Width - logoBitmap.Width) / 2;   // Horizontal offset
                        int y = (qrBitmap.Height - logoBitmap.Height) / 2; // Vertical offset
                        g.DrawImage(logoBitmap, x, y, logoBitmap.Width, logoBitmap.Height);
                    }

                    // Save the combined image to a PNG file
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        qrBitmap.Save(outStream, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine($"QR code with logo saved to: {outputPath}");
    }
}
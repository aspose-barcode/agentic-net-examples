// Title: Generate QR Code with Centered Logo Overlay
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, overlay a custom logo at its center, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image manipulation. It showcases the use of BarcodeGenerator, QR error correction settings, and Aspose.Drawing graphics to combine a barcode with a logo. Developers often need to embed branding into QR codes for marketing or product packaging, and this pattern illustrates the typical workflow.
// Prompt: Generate QR Code barcode and overlay a logo image at center of barcode.
// Tags: qr code, barcode generation, logo overlay, image processing, aspose.barcode, aspose.drawing, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and overlaying a logo at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, adds a red square logo in the middle,
    /// and saves the combined image as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the final image will be saved.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string resultPath = Path.Combine(outputDir, "QrCodeWithLogo.png");

        // Create a QR Code barcode generator with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the module size (pixel dimension) of the QR Code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Optional: configure the QR Code error correction level (Level M = ~15% recovery).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Render the barcode into a memory stream in PNG format.
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Load the generated barcode image into a Bitmap for further drawing.
                using (Bitmap barcodeBitmap = new Bitmap(barcodeStream))
                {
                    // Determine logo size as 20% of the barcode width.
                    int logoSize = barcodeBitmap.Width / 5;

                    // Create a simple logo bitmap (a solid red square).
                    using (Bitmap logoBitmap = new Bitmap(logoSize, logoSize))
                    {
                        using (Graphics gLogo = Graphics.FromImage(logoBitmap))
                        {
                            gLogo.Clear(Color.Red);
                        }

                        // Overlay the logo onto the center of the barcode image.
                        using (Graphics g = Graphics.FromImage(barcodeBitmap))
                        {
                            int x = (barcodeBitmap.Width - logoBitmap.Width) / 2;
                            int y = (barcodeBitmap.Height - logoBitmap.Height) / 2;
                            g.DrawImage(logoBitmap, new Rectangle(x, y, logoBitmap.Width, logoBitmap.Height));
                        }

                        // Save the final combined image to the specified file path.
                        barcodeBitmap.Save(resultPath, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine($"QR Code with logo saved to: {resultPath}");
    }
}
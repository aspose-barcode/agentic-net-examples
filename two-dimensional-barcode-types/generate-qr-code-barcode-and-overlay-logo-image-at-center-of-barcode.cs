// Title: Generate QR Code with Centered Logo
// Description: Demonstrates creating a QR Code barcode, applying high error correction, and overlaying a logo image at the barcode's center, then saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image compositing. It showcases key API classes such as BarcodeGenerator, EncodeTypes, QRErrorLevel, and Aspose.Drawing graphics objects. Developers commonly use these APIs to embed branding or custom graphics into QR codes for marketing, product packaging, or authentication scenarios.
// Prompt: Generate QR Code barcode and overlay a logo image at center of barcode.
// Tags: qr code, barcode generation, logo overlay, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode and overlays a logo image at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code, adds the logo, and saves the final image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the output barcode image and the logo image.
        const string barcodePath = "qr_with_logo.png";
        const string logoPath = "logo.png";

        // Ensure a logo image exists; create a simple placeholder if it is missing.
        if (!File.Exists(logoPath))
        {
            using (var logoBmp = new Bitmap(100, 100))
            {
                using (var g = Graphics.FromImage(logoBmp))
                {
                    g.Clear(Color.White);
                    using (var brush = new SolidBrush(Color.Red))
                    {
                        // Draw a red circle as a placeholder logo.
                        g.FillEllipse(brush, 10, 10, 80, 80);
                    }
                }
                // Save the placeholder logo as a PNG file.
                logoBmp.Save(logoPath, ImageFormat.Png);
            }
        }

        // Initialize the QR Code generator with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level to tolerate the logo overlay.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Configure automatic size mode and specify image dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Generate the QR Code as a bitmap image.
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Load the logo image from file.
                using (Bitmap logoBmp = (Bitmap)Image.FromFile(logoPath))
                {
                    // Compute the top‑left coordinates to center the logo on the QR Code.
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw the logo onto the QR Code bitmap.
                    using (Graphics graphics = Graphics.FromImage(barcodeBmp))
                    {
                        graphics.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image with the logo overlay.
                    barcodeBmp.Save(barcodePath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"QR code with logo saved to '{barcodePath}'.");
    }
}
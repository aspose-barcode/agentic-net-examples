// Title: Generate QR Code with Centered Logo Overlay
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, overlaying a custom logo image at the center, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It showcases the use of BarcodeGenerator to produce a QR Code, Aspose.Drawing.Bitmap and Graphics for image processing, and typical steps for adding visual elements such as logos. Developers working on branding, marketing materials, or custom scanning solutions often need to embed logos into barcodes while preserving scanability.
// Prompt: Generate QR Code barcode and overlay a logo image at center of barcode.
// Tags: qr code, barcode generation, logo overlay, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code, adds a centered logo, and saves the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, draws a red square logo at its center, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final image.
        string outputPath = "qr_with_logo.png";

        // Text to encode in the QR Code.
        string qrText = "https://www.example.com";

        // Initialize the QR Code generator with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Optional: set the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Generate the QR Code as a bitmap image.
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (80x80 red square) to overlay.
                int logoSize = 80;
                using (Bitmap logoBmp = new Bitmap(logoSize, logoSize, PixelFormat.Format32bppArgb))
                {
                    // Fill the logo bitmap with a solid red color.
                    using (Graphics gLogo = Graphics.FromImage(logoBmp))
                    {
                        gLogo.Clear(Color.Red);
                    }

                    // Compute coordinates to center the logo on the QR Code.
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw the logo onto the QR Code bitmap at the calculated position.
                    using (Graphics g = Graphics.FromImage(barcodeBmp))
                    {
                        g.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image as a PNG file.
                    barcodeBmp.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Output the full path of the saved image for verification.
        Console.WriteLine($"QR code with logo saved to: {Path.GetFullPath(outputPath)}");
    }
}
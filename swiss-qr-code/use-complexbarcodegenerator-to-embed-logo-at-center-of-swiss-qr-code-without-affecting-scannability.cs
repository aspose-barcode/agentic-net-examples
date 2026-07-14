// Title: Swiss QR Code with Embedded Center Logo using ComplexBarcodeGenerator
// Description: Generates a Swiss QR bill QR code and overlays a logo at its center while preserving scan reliability.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for Swiss QR bills, showing how to configure QR error correction, embed a central logo, and save as PNG. Uses ComplexBarcodeGenerator, SwissQRCodetext, and drawing classes. Ideal for developers needing to customize QR codes with branding without compromising readability.
// Prompt: Use ComplexBarcodeGenerator to embed a logo at the center of the Swiss QR Code without affecting scannability.
// Tags: swiss qr, barcode, logo overlay, complexbarcodegenerator, qrcode, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss QR bill QR code, embeds a logo at its centre,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code, adds the logo if available,
    /// and writes the output file.
    /// </summary>
    static void Main()
    {
        // Output file for the final QR code image
        const string outputPath = "SwissQR_with_logo.png";

        // Optional logo file to embed at the centre of the QR code
        const string logoPath = "logo.png";

        // Prepare Swiss QR bill data (mandatory fields)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the Swiss QR code with high error correction (Level H) to tolerate a logo
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create the QR code bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // If a logo file exists, overlay it at the centre of the QR code
                if (File.Exists(logoPath))
                {
                    using (var logoImage = new Bitmap(logoPath))
                    {
                        // Limit logo size to 20% of the QR code dimensions while preserving aspect ratio
                        int maxLogoWidth = (int)(barcodeBitmap.Width * 0.2);
                        int maxLogoHeight = (int)(barcodeBitmap.Height * 0.2);
                        float widthRatio = (float)maxLogoWidth / logoImage.Width;
                        float heightRatio = (float)maxLogoHeight / logoImage.Height;
                        float scale = Math.Min(widthRatio, heightRatio);
                        int logoWidth = (int)(logoImage.Width * scale);
                        int logoHeight = (int)(logoImage.Height * scale);

                        // Calculate centre position for the logo
                        int posX = (barcodeBitmap.Width - logoWidth) / 2;
                        int posY = (barcodeBitmap.Height - logoHeight) / 2;

                        // Draw the logo onto the QR code bitmap
                        using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                        {
                            graphics.DrawImage(logoImage, posX, posY, logoWidth, logoHeight);
                        }
                    }
                }

                // Save the final image in PNG format
                barcodeBitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Swiss QR code saved to {outputPath}");
    }
}
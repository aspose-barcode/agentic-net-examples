// Title: Embed a logo into a Swiss QR Code using ComplexBarcodeGenerator
// Description: Demonstrates how to generate a Swiss QR Bill QR code and overlay a custom logo at its center while preserving scan reliability.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator, QR error correction settings, and image compositing. Typical use cases include branding QR codes for invoices or payment slips without compromising readability. Developers often need to combine barcode data with graphics, adjust error correction levels, and export to common image formats.
// Prompt: Use ComplexBarcodeGenerator to embed a logo at the center of the Swiss QR Code without affecting scannability.
// Tags: swissqr, logo, complexbarcodegenerator, qr, png, generation, barcode symbology, image compositing

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Swiss QR Bill QR code and embeds a custom logo at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the QR code, draws a logo, composites them, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final image
        string outputPath = "SwissQR_with_logo.png";

        // --------------------------------------------------------------------
        // Prepare Swiss QR Bill data (mandatory fields for a valid QR bill)
        // --------------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ---------------------------------------------------------------
        // Create the complex barcode generator with the prepared QR data
        // ---------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Use the highest error correction level to tolerate the logo overlay
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the base QR code image
            using (Bitmap qrBitmap = generator.GenerateBarCodeImage())
            {
                // -----------------------------------------------------------
                // Create a simple logo bitmap (red square with white "A" text)
                // -----------------------------------------------------------
                using (Bitmap logoBitmap = new Bitmap(100, 100))
                {
                    using (Graphics logoGraphics = Graphics.FromImage(logoBitmap))
                    {
                        // Fill background with red
                        logoGraphics.Clear(Color.Red);

                        // Draw a white letter "A" in the center of the logo
                        using (Font font = new Font("Arial", 48, FontStyle.Bold))
                        {
                            var textSize = logoGraphics.MeasureString("A", font);
                            float textX = (logoBitmap.Width - textSize.Width) / 2f;
                            float textY = (logoBitmap.Height - textSize.Height) / 2f;
                            logoGraphics.DrawString("A", font, new SolidBrush(Color.White), textX, textY);
                        }
                    }

                    // Calculate the top‑left position to center the logo on the QR code
                    int posX = (qrBitmap.Width - logoBitmap.Width) / 2;
                    int posY = (qrBitmap.Height - logoBitmap.Height) / 2;

                    // Composite the logo onto the QR code image
                    using (Graphics qrGraphics = Graphics.FromImage(qrBitmap))
                    {
                        qrGraphics.DrawImage(logoBitmap, posX, posY, logoBitmap.Width, logoBitmap.Height);
                    }
                }

                // Save the composited image as PNG
                qrBitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"Swiss QR Code with embedded logo saved to: {Path.GetFullPath(outputPath)}");
            }
        }
    }
}
// Title: Embed Logo in Swiss QR Code using ComplexBarcodeGenerator
// Description: Demonstrates how to generate a Swiss QR Code with a custom logo placed at its center while preserving scan reliability.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and related classes such as SwissQRCodetext, Address, and QR error correction settings. Typical use cases include creating payment QR codes with branding elements. Developers often need to embed images or logos into QR codes without compromising readability, and this snippet illustrates that workflow.
// Prompt: Use ComplexBarcodeGenerator to embed a logo at the center of the Swiss QR Code without affecting scannability.
// Tags: swiss qr code, logo embedding, complex barcode, barcode generation, aspnet.barcode, qr error correction, png output

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a logo into a Swiss QR Code using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Code with a centered red square logo and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQRWithLogo.png");

        // Build the Swiss QR Code data structure with payment details
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH9300762011623852957";
        swissQRCode.Bill.Amount = 199.95m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main Street",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Jane Smith",
            Street = "Second Street",
            HouseNo = "2",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Initialize the generator with the Swiss QR Code data
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Configure barcode appearance: module size and high error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the base QR code image
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Create a simple red square logo (80x80 pixels)
                using (Bitmap logo = new Bitmap(80, 80))
                {
                    using (Graphics gLogo = Graphics.FromImage(logo))
                    {
                        gLogo.Clear(Color.Red);
                    }

                    // Overlay the logo onto the center of the QR code image
                    using (Graphics g = Graphics.FromImage(barcodeBitmap))
                    {
                        int x = (barcodeBitmap.Width - logo.Width) / 2;
                        int y = (barcodeBitmap.Height - logo.Height) / 2;
                        g.DrawImage(logo, x, y, logo.Width, logo.Height);
                    }
                }

                // Save the final image with the embedded logo to the specified path
                barcodeBitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Swiss QR Code with embedded logo saved to: {outputPath}");
    }
}
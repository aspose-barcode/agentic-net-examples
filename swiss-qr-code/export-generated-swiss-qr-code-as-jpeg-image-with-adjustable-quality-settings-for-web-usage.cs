// Title: Export Swiss QR Code as JPEG with Web‑Optimized Settings
// Description: Demonstrates generating a Swiss QR Code (payment QR bill) and exporting it as a JPEG image optimized for web usage.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode types such as Swiss QR Codes. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and BarCodeImageFormat classes to create payment QR bills, adjust image resolution, anti‑aliasing, and module size, and save the result in a web‑friendly JPEG format. Developers working with financial QR codes or needing image‑based barcode outputs for web applications will find this pattern useful.
// Prompt: Export the generated Swiss QR Code as a JPEG image with adjustable quality settings for web usage.
// Tags: swiss qr code, barcode generation, jpeg output, web optimization, aspose.barcode, complexbarcodegenerator, swissqr, image format

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a Swiss QR Code (QR bill) and saves it as a JPEG image optimized for web usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates QR bill data, configures image settings, and writes the JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.jpeg");

        // Build the Swiss QR Code payload with required bill information.
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Populate mandatory creditor address fields.
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Populate optional debtor address fields (included for completeness).
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode with settings suitable for web usage.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Lower the resolution to 72 DPI to reduce file size.
            generator.Parameters.Resolution = 72f;

            // Disable anti‑aliasing to further shrink the JPEG.
            generator.Parameters.UseAntiAlias = false;

            // Increase module size (pixel dimension) for readability at low resolution.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}
// Title: Generate Swiss QR Code Barcode in ASP.NET MVC
// Description: Demonstrates how to create a Swiss QR Code (QR‑Bill) using Aspose.BarCode and save it as a PNG image, suitable for returning from an ASP.NET MVC controller action.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR Code (QR‑Bill) creation. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings to produce a high‑resolution PNG. Developers building payment or invoicing solutions often need to generate QR‑Bills programmatically and deliver them via web APIs.
// Prompt: Integrate Swiss QR Code generation into an ASP.NET MVC controller action returning the barcode image as HTTP response.
// Tags: swiss qr code, barcode generation, asp.net mvc, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Console demo of Swiss QR Code generation using Aspose.BarCode.
/// In a real ASP.NET MVC app, the same logic would be placed in a controller action.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds a Swiss QR Code (QR‑Bill) and writes it to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real ASP.NET MVC application this logic would be placed in a controller action
        // that returns the image as an HTTP response. The console app demonstrates the core barcode
        // generation logic required for such integration.

        // Prepare Swiss QR Code data
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Set creditor address details
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Set debtor address details
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure barcode appearance and encoding
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Define output file path in the temporary folder
            string outputPath = Path.Combine(Path.GetTempPath(), "SwissQRBill.png");

            // Save the barcode image to a memory stream, then write to file
            using (var stream = new MemoryStream())
            {
                generator.Save(stream, BarCodeImageFormat.Png);
                File.WriteAllBytes(outputPath, stream.ToArray());
            }

            Console.WriteLine($"Swiss QR Code barcode saved to: {outputPath}");
        }
    }
}
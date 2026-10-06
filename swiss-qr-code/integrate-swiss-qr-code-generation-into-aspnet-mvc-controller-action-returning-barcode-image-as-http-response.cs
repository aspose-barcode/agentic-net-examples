// Title: Generate Swiss QR Code using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR Code (QR‑Bill) with Aspose.BarCode, configuring bill details, and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related classes to produce QR‑Bill compliant Swiss QR Codes. Typical use cases include generating payment QR codes for invoices or financial documents in .NET applications. Developers often need to set bill parameters, adjust barcode appearance, and output the image for web or desktop consumption.
// Prompt: Integrate Swiss QR Code generation into an ASP.NET MVC controller action returning the barcode image as HTTP response.
// Tags: swiss qr, barcode generation, png, aspnet mvc, aspose.barcode, complexbarcode, qr-bill

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss QR Code (QR‑Bill) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode, saves it to a temporary file, and outputs a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated request payload for Swiss QR Code
        var swissQr = new SwissQRCodetext();

        // Populate bill details required for a Swiss QR‑Bill
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Creditor (payee) address information
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Debtor (payer) address information
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the barcode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set visual parameters: module size and QR encoding mode
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image to a temporary file (simulating an HTTP response body)
            string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Swiss QR Code image saved to: {outputPath}");

            // Also output a Base64 representation of the image (simulating HTTP response content)
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Base64 representation of the image:");
                Console.WriteLine(base64);
            }
        }
    }
}
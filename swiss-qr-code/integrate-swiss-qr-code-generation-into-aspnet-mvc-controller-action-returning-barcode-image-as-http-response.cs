// Title: Generate Swiss QR Code and output as PNG (Base64 and file)
// Description: Demonstrates creating a Swiss QR Code for a payment bill using Aspose.BarCode, rendering it to a PNG image, and showing the result as a Base64 string and a temporary file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR Code (QR‑Bill) creation. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings to produce payment‑ready QR codes. Developers building invoicing, banking, or e‑commerce solutions often need to generate QR‑Bill images for Swiss payments, and this snippet illustrates the typical workflow for such use cases.
// Prompt: Integrate Swiss QR Code generation into an ASP.NET MVC controller action returning the barcode image as HTTP response.
// Tags: swiss qr code, barcode generation, png, aspnet mvc, aspose.barcode, complexbarcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a Swiss QR Code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR code, saves it to a memory stream, prints Base64 and writes to a temporary file.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real ASP.NET MVC application this logic would be placed in a controller action
        // that returns the image as an HTTP response. Here we demonstrate the core barcode generation
        // in a console program.

        // ------------------------------------------------------------
        // Create Swiss QR Code data (QR‑Bill)
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // ------------------------------------------------------------
        // Generate the barcode image
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure visual and encoding parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // ------------------------------------------------------------
                // Output the image as a Base64 string (useful for embedding in HTML)
                // ------------------------------------------------------------
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Swiss QR Code image (Base64 PNG):");
                Console.WriteLine(base64);

                // ------------------------------------------------------------
                // Write the PNG to a temporary file for verification
                // ------------------------------------------------------------
                string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");
                File.WriteAllBytes(outputPath, ms.ToArray());
                Console.WriteLine($"Image saved to: {outputPath}");
            }
        }
    }
}
// Title: Generate Swiss QR Code and Save as PNG in ASP.NET MVC-like Example
// Description: Demonstrates how to create a Swiss QR Code using Aspose.BarCode, suitable for returning the barcode image from an ASP.NET MVC controller.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and SwissQRCodetext to produce Swiss QR Bill barcodes, a common requirement for financial applications in Switzerland. Developers often need to generate these barcodes on the fly and deliver them as image responses in web APIs or MVC actions.
// Prompt: Integrate Swiss QR Code generation into an ASP.NET MVC controller action returning the barcode image as HTTP response.
// Tags: swiss qr, barcode generation, aspnet mvc, png, complexbarcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that simulates an ASP.NET MVC controller action which generates a Swiss QR Code
/// and saves the resulting image to a PNG file. The same logic can be used to write the image directly
/// to an HTTP response stream in a real MVC controller.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the Swiss QR Code, saves it as PNG, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare Swiss QR Bill data (mandatory fields only)
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor name
        swissQr.Bill.Creditor.CountryCode = "CH";             // ISO country code (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";       // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // ------------------------------------------------------------
        // 2. Generate the Swiss QR Code using ComplexBarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // ------------------------------------------------------------
            // 3. Save the barcode image to a PNG file
            // ------------------------------------------------------------
            const string outputPath = "SwissQR.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Swiss QR Code image saved to '{Path.GetFullPath(outputPath)}'.");
        }
    }
}
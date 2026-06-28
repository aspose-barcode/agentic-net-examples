using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a Swiss QR Code using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Swiss QR Code image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

        // Instantiate a Swiss QR code text object which holds all required bill data.
        var swissQr = new SwissQRCodetext();

        // Set creditor (payee) mandatory details.
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Assign the creditor's IBAN (must be a valid Swiss IBAN).
        swissQr.Bill.Account = "CH9300762011623852957";

        // Specify the amount to be paid.
        swissQr.Bill.Amount = 199.95m;

        // Choose the QR bill version (standard V2.0).
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a barcode generator for the Swiss QR code and save it as a PNG image.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"Swiss QR Code image saved to: {outputFile}");
    }
}
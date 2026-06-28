using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss QR Bill barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Swiss QR Bill, encodes it, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Build the full path for the output JPEG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.jpeg");

        // Initialize a new Swiss QR code text object.
        var swissQr = new SwissQRCodetext();

        // Populate the creditor information and bill details.
        swissQr.Bill.Creditor.Name = "John Doe";                     // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";                  // Creditor's country (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";            // IBAN account number
        swissQr.Bill.Amount = 199.95m;                             // Amount to be paid
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // Generate the barcode using the ComplexBarcodeGenerator and save it as JPEG.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}
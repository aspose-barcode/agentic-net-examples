using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR code using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Swiss QR code with mandatory fields,
    /// configures high error correction, and saves the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR codetext and populate required bill information
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";             // Creditor's country code (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";        // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // Create a barcode generator for the Swiss QR code
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set QR error correction level to high (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Determine output file path in the current working directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"Swiss QR code saved to: {outputPath}");
        }
    }
}
using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss QR code with maximum error correction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Swiss QR bill codetext, configures error correction,
    /// generates the barcode image, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a Swiss QR codetext with only the required fields.
        var swissQr = new SwissQRCodetext();

        // Set creditor (payee) details.
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Set the IBAN account number.
        swissQr.Bill.Account = "CH9300762011623852957";

        // Set the payment amount.
        swissQr.Bill.Amount = 199.95m;

        // Specify the QR bill version (V2.0).
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a barcode generator for the Swiss QR codetext.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure QR code to use the highest Reed‑Solomon error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to a PNG file.
            generator.Save("SwissQR_H.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Swiss QR barcode generated with maximum error correction (LevelH).");
    }
}
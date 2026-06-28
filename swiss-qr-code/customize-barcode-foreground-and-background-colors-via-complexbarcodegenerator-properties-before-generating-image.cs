using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR code barcode with custom colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Swiss QR code, configures barcode colors,
    /// and saves the generated image to a file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a Swiss QR codetext with required fields
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();

        // Set creditor (payee) details
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Set account number (IBAN) and payment amount
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;

        // Specify the QR bill version to use
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ------------------------------------------------------------
        // 2. Generate the barcode with custom colors
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to yellow
            generator.Parameters.BackColor = Color.Yellow;

            // Define the output file path
            string outputPath = "complex_barcode.png";

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the image was saved
            Console.WriteLine($"Barcode image saved to {outputPath}");
        }
    }
}
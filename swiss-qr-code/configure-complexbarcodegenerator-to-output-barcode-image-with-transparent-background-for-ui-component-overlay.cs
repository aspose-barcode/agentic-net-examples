using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss QR code barcode with a transparent background
/// and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "transparent_barcode.png";

        // Create and configure the Swiss QR code text with required bill details.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";             // Creditor's country code (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";       // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // Generate the barcode using the configured Swiss QR code text.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the background color of the barcode to transparent.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG file with the specified output path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}
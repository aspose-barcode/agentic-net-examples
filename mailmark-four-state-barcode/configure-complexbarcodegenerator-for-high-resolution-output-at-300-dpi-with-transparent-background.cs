using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Swiss QR bill barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Swiss QR code image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a simple Swiss QR codetext as an example of complex barcode data
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";             // Creditor's country (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";       // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // ------------------------------------------------------------
        // 2. Define the output file path for the generated PNG image
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

        // ------------------------------------------------------------
        // 3. Create a ComplexBarcodeGenerator, configure resolution and background,
        //    then save the barcode image as PNG
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set image resolution to 300 DPI for high quality
            generator.Parameters.Resolution = 300f;

            // Use a transparent background for the barcode image
            generator.Parameters.BackColor = Color.Transparent;

            // Save the generated barcode to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 4. Inform the user where the barcode image has been saved
        // ------------------------------------------------------------
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
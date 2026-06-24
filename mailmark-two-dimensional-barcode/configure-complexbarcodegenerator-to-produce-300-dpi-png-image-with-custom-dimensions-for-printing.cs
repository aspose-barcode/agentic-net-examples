using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR bill barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Swiss QR bill, configures barcode parameters,
    /// and saves the generated barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a Swiss QR code text object and populate required bill fields.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";             // Creditor's country (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";       // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

        // Initialize the complex barcode generator with the Swiss QR code data.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the image resolution to 300 dots per inch (dpi) for high-quality output.
            generator.Parameters.Resolution = 300f;

            // Define custom image dimensions in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;   // Width of the barcode image
            generator.Parameters.ImageHeight.Point = 150f; // Height of the barcode image

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}
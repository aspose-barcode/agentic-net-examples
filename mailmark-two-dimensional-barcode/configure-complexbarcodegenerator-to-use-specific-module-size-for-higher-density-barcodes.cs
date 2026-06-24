using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR code using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Swiss QR codetext, configures the generator,
    /// and saves the resulting barcode image to a file.
    /// </summary>
    static void Main()
    {
        // Create a Swiss QR codetext object which holds bill information
        var swissQr = new SwissQRCodetext();

        // Populate creditor details
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Set account number, amount, and QR bill version
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Initialize the ComplexBarcodeGenerator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure a smaller XDimension (module size) for higher barcode density
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // 0.5 point per module

            // Optionally increase the image resolution for better visual quality
            generator.Parameters.Resolution = 300f;

            // Define output file path
            string outputPath = "SwissQR_HighDensity.png";

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}
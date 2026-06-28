using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR Code barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Swiss QR Code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "SwissQR.png";

        // Create a Swiss QR code text object and populate mandatory bill fields.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";             // Creditor's country (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";       // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // Initialize the barcode generator with the prepared Swiss QR code text.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure image dimensions (in points). 1 point = 1/72 inch.
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 400f;

            // Set padding (margins) around the barcode to ensure proper whitespace.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: define the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved image to the console for verification.
        Console.WriteLine($"Swiss QR Code saved to: {Path.GetFullPath(outputPath)}");
    }
}
// Title: Generate a high‑density Swiss QR barcode using ComplexBarcodeGenerator
// Description: Demonstrates how to create a Swiss QR code with a custom, smaller module size for increased barcode density, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and SwissQRCodetext classes. Developers often need to generate QR‑based payment codes (Swiss QR) with specific visual characteristics such as higher density for compact printing. The snippet illustrates setting barcode parameters like XDimension and saving the result in a common image format.
// Prompt: Configure ComplexBarcodeGenerator to use a specific module size for higher density barcodes.
// Tags: barcode symbology, complex barcode generation, swiss qr, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss QR barcode with a custom module size for higher density.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

        // Initialize Swiss QR code text and populate required bill fields.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create the complex barcode generator with the prepared Swiss QR data.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Adjust the module size (XDimension) to a smaller value for higher barcode density.
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Swiss QR barcode saved to: {outputPath}");
    }
}
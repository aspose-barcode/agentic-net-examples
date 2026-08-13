// Title: Generate Swiss QR Code and save as PNG with custom size and margins
// Description: Demonstrates creating a Swiss QR Bill barcode using Aspose.BarCode, configuring image dimensions and padding, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Bill. It showcases the use of ComplexBarcodeGenerator and related parameter settings for image size and margins, common tasks for developers needing customized barcode images for invoices or payment slips.
// Prompt: Save the generated Swiss QR Code to a PNG file with custom dimensions and margins.
// Tags: swiss qr code, barcode generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss QR Bill barcode and saving it as a PNG with custom dimensions and margins.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures size and padding, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated PNG image
        string outputPath = "SwissQR.png";

        // Prepare Swiss QR bill data with required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Initialize the generator for a complex barcode (Swiss QR) and apply custom settings
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set custom image dimensions (in points)
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 400f;

            // Set custom padding (margins) around the barcode (in points)
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the configured barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved image for verification
        Console.WriteLine($"Swiss QR Code saved to: {Path.GetFullPath(outputPath)}");
    }
}
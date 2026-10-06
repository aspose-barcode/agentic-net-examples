// Title: Rotate Swiss QR Code using ComplexBarcodeGenerator
// Description: Generates a Swiss QR bill barcode and rotates the image by 90 degrees before saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator class to create a Swiss QR bill, configure its parameters (including rotation), and export the result. Developers working with payment QR codes, custom barcode layouts, or image transformations commonly use these APIs to meet regulatory and branding requirements.
// Prompt: Enable barcode rotation by setting ComplexBarcodeGenerator RotationAngle property before generating the image.
// Tags: swiss qr, rotation, complexbarcodegenerator, png, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Swiss QR bill barcode, rotate it, and save the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares the output folder, builds the Swiss QR codetext,
    /// applies a 90‑degree rotation, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the generated image will be stored
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Construct Swiss QR codetext with required bill fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator, set the rotation angle, and save the image
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Rotate the barcode 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Define the full path for the output PNG file
            string outputPath = Path.Combine(outputDir, "SwissQR_Rotated.png");

            // Generate and save the rotated barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}
// Title: Rotate Swiss QR barcode using ComplexBarcodeGenerator
// Description: This example creates a Swiss QR bill barcode, sets a 90-degree rotation, and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode ComplexBarcode generation for Swiss QR bills, covering barcode creation, parameter customization (including rotation), and image export. Useful for developers needing to embed rotated QR codes in documents or UI, leveraging ComplexBarcodeGenerator and BarCodeImageFormat classes.
// Prompt: Enable barcode rotation by setting ComplexBarcodeGenerator RotationAngle property before generating the image.
// Tags: swiss qr, barcode rotation, complexbarcodegenerator, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Swiss QR bill barcode, applies a rotation, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, rotates it, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR code text with required bill information
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Define output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR_Rotated.png");

        // Generate the barcode with a 90-degree rotation and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set rotation angle (in degrees) before saving
            generator.Parameters.RotationAngle = 90f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the rotated barcode image was saved
        Console.WriteLine($"Rotated barcode saved to: {outputPath}");
    }
}
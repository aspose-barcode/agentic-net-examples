// Title: Rotate Swiss QR Barcode Using ComplexBarcodeGenerator
// Description: Demonstrates how to rotate a Swiss QR barcode image by setting the RotationAngle property before saving the image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use ComplexBarcodeGenerator with SwissQRCodetext to create a QR bill, configure required fields, and apply image rotation. Developers working with Swiss QR codes, QR bills, or any complex barcode formats often need to adjust orientation for layout or printing requirements, using classes like ComplexBarcodeGenerator, BarCodeImageFormat, and related parameter objects.
/// <summary>
/// Provides an entry point that generates a rotated Swiss QR barcode and saves it as a PNG file.
/// </summary>
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Main program class for the barcode rotation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR barcode, rotates it 90 degrees clockwise, and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file path
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeRotationExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQR_Rotated.png");

        // --------------------------------------------------------------------
        // Create Swiss QR codetext and populate required bill fields
        // --------------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // --------------------------------------------------------------------
        // Generate the barcode with a 90-degree rotation
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set rotation angle (degrees clockwise)
            generator.Parameters.RotationAngle = 90f;
            // Save the rotated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Rotated Swiss QR barcode saved to: {outputPath}");
    }
}
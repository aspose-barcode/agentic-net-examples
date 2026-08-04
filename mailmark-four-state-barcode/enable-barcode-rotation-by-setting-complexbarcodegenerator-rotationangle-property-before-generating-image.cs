// Title: Rotate Swiss QR Code Barcode Using ComplexBarcodeGenerator
// Description: Demonstrates how to rotate a Swiss QR Code barcode image by setting the RotationAngle property before saving the image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and its Parameters to manipulate barcode appearance, such as rotation. Developers working with Swiss QR Codes, QR payments, or custom barcode layouts often need to adjust orientation for branding or layout requirements. The example highlights key classes like SwissQRCodetext, ComplexBarcodeGenerator, and the RotationAngle property, providing a quick reference for similar scenarios.
/// Prompt: Enable barcode rotation by setting ComplexBarcodeGenerator RotationAngle property before generating the image.
// Tags: swissqr, rotation, complexbarcodegenerator, barcode, image, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a rotated Swiss QR Code barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a Swiss QR Code, sets rotation, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR Code data with required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator using the Swiss QR Code data
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the rotation angle (e.g., 45 degrees) before generating the image
            generator.Parameters.RotationAngle = 45f;

            // Define output file path and save the rotated barcode image
            string outputPath = "rotatedSwissQR.png";
            generator.Save(outputPath);

            // Output the full path of the saved image for verification
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}
// Title: Rotate SwissQR barcode image using ComplexBarcodeGenerator
// Description: Demonstrates how to rotate a SwissQR barcode by setting the RotationAngle property before saving the image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and SwissQRCodetext classes to create and manipulate Swiss QR Bill barcodes. Typical use cases include generating payment QR codes with custom orientation for printing or display. Developers often need to adjust rotation, size, and other parameters when integrating barcodes into documents or UI layouts.
// Prompt: Enable barcode rotation by setting ComplexBarcodeGenerator RotationAngle property before generating the image.
// Tags: swissqr, rotation, complexbarcodegenerator, barcodegeneration, png, aspnet, aspnetcore, csharp

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a rotated SwissQR barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a SwissQR codetext, sets a rotation angle,
    /// and saves the resulting barcode image to a file.
    /// </summary>
    static void Main()
    {
        // Create a SwissQR codetext and populate required fields for a payment bill
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Initialize ComplexBarcodeGenerator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set rotation angle (e.g., 90 degrees) before generating the image
            generator.Parameters.RotationAngle = 90f;

            // Save the rotated barcode image to a PNG file
            generator.Save("rotated_swissqr.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated and saved as rotated_swissqr.png");
    }
}
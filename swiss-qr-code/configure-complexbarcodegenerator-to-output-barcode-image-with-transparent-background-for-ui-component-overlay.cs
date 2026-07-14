// Title: Generate Swiss QR barcode with transparent background
// Description: Demonstrates configuring ComplexBarcodeGenerator to produce a PNG barcode image with a transparent background, suitable for overlay in UI components.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use ComplexBarcodeGenerator with Swiss QR codetext, set visual parameters like background transparency, and save to PNG. Developers working with QR codes for payment standards or UI overlays often need to customize colors and output formats using the Aspose.BarCode.Generation and Aspose.BarCode.ComplexBarcode APIs.
// Prompt: Configure ComplexBarcodeGenerator to output a barcode image with transparent background for UI component overlay.
// Tags: swissqr, qr, transparent background, png, complexbarcodegenerator, aspnet, aspnetcore, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss QR barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it as a PNG with transparency.
    /// </summary>
    static void Main()
    {
        // Prepare SwissQR codetext with required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator with the codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set transparent background for the image
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the barcode (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define the output file path (current directory)
            string outputPath = Path.Combine(Environment.CurrentDirectory, "transparent_qr.png");

            // Save the barcode as PNG, which supports transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode image generated with transparent background.");
    }
}
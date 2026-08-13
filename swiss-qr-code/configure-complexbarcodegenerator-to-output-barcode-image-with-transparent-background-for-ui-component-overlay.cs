// Title: Generate Swiss QR Bill barcode with transparent background using ComplexBarcodeGenerator
// Description: Creates a Swiss QR bill barcode, sets a transparent background, and saves it as a PNG image suitable for UI overlay.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing how to use the ComplexBarcodeGenerator class to produce structured barcodes such as Swiss QR bills. It demonstrates typical tasks like configuring visual parameters (background and bar colors) and exporting to formats that support transparency. Developers working with financial documents, UI components, or custom barcode rendering will find this pattern useful for integrating barcodes into graphics or web pages.
// Prompt: Configure ComplexBarcodeGenerator to output a barcode image with transparent background for UI component overlay.
// Tags: swiss qr, complex barcode, transparent background, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR bill barcode with a transparent background using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures transparency, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR bill data (mandatory fields)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate barcode with transparent background
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the image background to transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Optional: set the barcode (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Define output file path (PNG supports transparency)
            string outputPath = "transparent_barcode.png";

            // Save the barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}
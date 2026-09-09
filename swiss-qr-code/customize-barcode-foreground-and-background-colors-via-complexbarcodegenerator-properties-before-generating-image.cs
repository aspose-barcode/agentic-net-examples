// Title: Customizing Foreground and Background Colors of a Swiss QR Barcode
// Description: Demonstrates how to set the bar (foreground) and background colors of a Swiss QR barcode using Aspose.BarCode's ComplexBarcodeGenerator before saving the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization of complex barcodes. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter classes to modify colors, a common requirement when integrating barcodes into branded documents or UI. Developers often need to adjust foreground and background colors to match corporate style guidelines while generating PNG images.
// Prompt: Customize barcode foreground and background colors via ComplexBarcodeGenerator properties before generating the image.
// Tags: barcode, swissqr, color customization, complexbarcodegenerator, png, aspnet, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss QR barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss QR barcode, applies custom colors, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQR.png");

        // Build the Swiss QR codetext with required bill information
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the barcode and apply custom colors
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;
            // Set the background color to yellow
            generator.Parameters.BackColor = Color.Yellow;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}
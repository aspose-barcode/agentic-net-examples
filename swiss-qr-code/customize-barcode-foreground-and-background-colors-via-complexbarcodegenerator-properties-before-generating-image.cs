// Title: Customizing Foreground and Background Colors of a Swiss QR Barcode
// Description: Demonstrates how to set the bar (foreground) and background colors of a Swiss QR code using Aspose.BarCode's ComplexBarcodeGenerator before saving the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on color customization for complex barcodes such as Swiss QR. It showcases the use of ComplexBarcodeGenerator, its Parameters property, and related classes like SwissQRCodetext. Developers often need to adjust the visual appearance of generated barcodes for branding or UI integration, and this snippet illustrates the typical steps: create codetext, configure colors, and export to PNG.
// Prompt: Customize barcode foreground and background colors via ComplexBarcodeGenerator properties before generating the image.
// Tags: swissqr, barcode, color customization, complexbarcodegenerator, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Swiss QR bill barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss QR code, applies custom colors, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR_CustomColors.png");

        // Build the Swiss QR codetext with required billing information.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Initialize the ComplexBarcodeGenerator with the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the foreground (bars) color to blue.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color to yellow.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}
// Title: Customize SwissQR barcode colors with ComplexBarcodeGenerator
// Description: Demonstrates how to set foreground and background colors for a SwissQR barcode before generating a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and its Parameters to customize visual appearance such as bar and background colors. Developers often need to tailor barcode colors to match branding or UI themes while generating QR codes for payments or data exchange. The snippet illustrates typical steps: creating codetext, configuring colors, and saving the image.
// Prompt: Customize barcode foreground and background colors via ComplexBarcodeGenerator properties before generating the image.
// Tags: swissqr, complexbarcode, color, png, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a SwissQR barcode, customizes its colors,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a SwissQR codetext with required mandatory fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Define the output file path for the generated barcode image
        string outputPath = "SwissQR.png";

        // Create a ComplexBarcodeGenerator instance using the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color to yellow
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}
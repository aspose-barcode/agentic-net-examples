// Title: Generate Swiss QR Code with custom foreground and background colors
// Description: Demonstrates how to set barcode and background colors using ComplexBarcodeGenerator before saving the image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings. Typical use cases include creating payment QR codes with customized visual appearance for branding or readability. Developers often need to adjust colors, sizes, and formats when integrating barcode generation into applications.
// Prompt: Customize barcode foreground and background colors via ComplexBarcodeGenerator properties before generating the image.
// Tags: swissqr, complexbarcode, color, foreground, background, png, aspnet, aspnetcore, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR Code with custom foreground and background colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a Swiss QR Code, sets custom colors, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR Code data (codetext) with creditor and payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator using the Swiss QR Code codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set custom foreground (barcode) color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set custom background color for the entire image
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Define output file path in the temporary folder
            string outputPath = Path.Combine(Path.GetTempPath(), "complex_barcode.png");

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}
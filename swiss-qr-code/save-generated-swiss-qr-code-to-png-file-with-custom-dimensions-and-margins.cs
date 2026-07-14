// Title: Generate Swiss QR Code with custom size and margins
// Description: Demonstrates creating a Swiss QR Bill barcode, customizing its image dimensions and padding, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator with SwissQRCodetext. Developers often need to generate QR codes for payment bills, adjust image size, margins, and colors before exporting to common image formats like PNG.
// Prompt: Save the generated Swiss QR Code to a PNG file with custom dimensions and margins.
// Tags: barcode symbology, generation, png, swissqr, complexbarcodegenerator, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR Bill barcode with custom dimensions and margins, then saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the QR bill data, configures the generator, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR bill data
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create generator for the complex Swiss QR code
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set custom image dimensions (points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Set custom margins (padding) around the barcode (points)
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set background and bar colors
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated Swiss QR code as PNG
            const string outputPath = "SwissQR.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Swiss QR Code saved to '{outputPath}'.");
        }
    }
}
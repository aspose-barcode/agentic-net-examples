// Title: Generate Swiss QR Bill Barcode with Custom DPI and Size
// Description: Demonstrates configuring ComplexBarcodeGenerator to create a 300 dpi PNG image with specific dimensions for printing a Swiss QR Bill.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use ComplexBarcodeGenerator and SwissQRCodetext to produce high‑resolution, printable barcodes. It highlights setting image resolution, custom dimensions, and colors—common tasks when integrating QR‑based payment codes into invoices or documents. Developers often need these settings to meet printing standards and branding requirements.
// Prompt: Configure ComplexBarcodeGenerator to produce a 300 dpi PNG image with custom dimensions for printing.
// Tags: swissqr, qr, barcode, generation, png, resolution, dimensions, complexbarcodegenerator, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss QR Bill barcode with custom DPI and image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the Swiss QR codetext, configures the generator, and saves a PNG image.
    /// </summary>
    static void Main()
    {
        // Prepare SwissQR codetext with mandatory fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator using the codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set image resolution to 300 dpi for high‑quality printing
            generator.Parameters.Resolution = 300f;

            // Define custom image dimensions in points (1 point = 1/72 inch)
            generator.Parameters.ImageWidth.Point = 600f;   // approx 8.33 in
            generator.Parameters.ImageHeight.Point = 400f;  // approx 5.55 in

            // Optional: set background and barcode colors
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG file
            generator.Save("SwissQR.png");
        }

        Console.WriteLine("Barcode image generated: SwissQR.png");
    }
}
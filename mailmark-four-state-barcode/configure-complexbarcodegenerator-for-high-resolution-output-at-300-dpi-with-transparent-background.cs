// Title: Generate Swiss QR Bill barcode with high‑resolution transparent PNG
// Description: Demonstrates creating a Swiss QR Bill barcode using Aspose.BarCode, configuring 300 DPI resolution and a transparent background, then saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Bill. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and generation parameters to control image resolution, colors, and output formats—common tasks for developers integrating payment QR codes into applications.
// Prompt: Configure ComplexBarcodeGenerator for high‑resolution output at 300 DPI with transparent background.
// Tags: swissqr, barcode, high resolution, transparent background, png, aspose.barcode, complexbarcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR Bill barcode with high resolution and transparent background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds Swiss QR codetext, configures generator, and saves PNG image.
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

        // Create ComplexBarcodeGenerator with the codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set high resolution (300 DPI)
            generator.Parameters.Resolution = 300f;

            // Set transparent background
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode image as PNG
            generator.Save("SwissQR.png");
        }

        // Inform the user that generation succeeded
        Console.WriteLine("Barcode generated successfully.");
    }
}
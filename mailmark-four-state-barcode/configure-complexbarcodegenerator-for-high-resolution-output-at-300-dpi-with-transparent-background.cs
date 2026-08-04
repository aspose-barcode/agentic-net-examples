// Title: Generate High‑Resolution Swiss QR Complex Barcode with Transparent Background
// Description: Demonstrates creating a Swiss QR bill complex barcode, configuring the generator for 300 DPI resolution and a transparent background, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with SwissQRCodetext to produce payment‑oriented QR codes. Developers often need high‑resolution, transparent images for printing invoices or embedding in UI designs, and this pattern illustrates the typical API workflow.
// Prompt: Configure ComplexBarcodeGenerator for high‑resolution output at 300 DPI with transparent background.
// Tags: swissqr, complexbarcode, generation, png, aspose.barcode.complexbarcode, aspose.barcode.generation, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss QR bill complex barcode with high resolution
/// and a transparent background, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the Swiss QR codetext, configures the generator,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a Swiss QR codetext (complex barcode data) with creditor and payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator using the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure the generator for high‑resolution output (300 DPI)
            generator.Parameters.Resolution = 300f;

            // Set the background to be fully transparent
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Define the output file path
            string outputPath = "complex_highres.png";

            // Ensure the target directory exists before saving
            string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated successfully.");
    }
}
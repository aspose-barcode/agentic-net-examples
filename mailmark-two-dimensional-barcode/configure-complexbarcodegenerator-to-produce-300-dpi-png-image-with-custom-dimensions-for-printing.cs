// Title: Generate Swiss QR Code as 300 dpi PNG with Custom Dimensions
// Description: Demonstrates creating a Swiss QR (QR‑Bill) barcode using Aspose.BarCode, configuring 300 dpi resolution and custom image size, then saving it as a PNG for printing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with SwissQRCodetext to produce payment‑oriented QR codes. Developers commonly need to adjust resolution and image dimensions for high‑quality print output, making this pattern useful for invoicing, billing, and other financial document workflows.
// Prompt: Configure ComplexBarcodeGenerator to produce a 300 dpi PNG image with custom dimensions for printing.
// Tags: swissqr, generation, png, complexbarcodegenerator, swissqrcodetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Swiss QR (QR‑Bill) barcode,
/// sets a 300 dpi resolution and custom image dimensions,
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the QR‑Bill data,
    /// configures the generator, and writes the PNG image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR codetext (complex barcode data) with creditor information and payment details.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator using the prepared Swiss QR codetext.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set the image resolution to 300 dots per inch for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Define custom image dimensions in points (e.g., 600 × 400 points).
            generator.Parameters.ImageWidth.Point = 600f;
            generator.Parameters.ImageHeight.Point = 400f;

            // Save the generated barcode as a PNG file.
            generator.Save("SwissQR_300dpi.png");
        }

        // Inform the user that the image has been created.
        Console.WriteLine("Barcode image generated: SwissQR_300dpi.png");
    }
}
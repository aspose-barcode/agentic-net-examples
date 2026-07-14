// Title: Export Swiss QR Code to JPEG with Adjustable Quality
// Description: Demonstrates generating a Swiss QR Code and exporting it as a JPEG image suitable for web usage.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and SwissQRCodetext to create Swiss QR payment codes, then saves the result as a JPEG. Developers working with payment QR codes, image export, or web‑optimized barcode rendering commonly use these APIs.
// Prompt: Export the generated Swiss QR Code as a JPEG image with adjustable quality settings for web usage.
// Tags: swiss qr, barcode generation, jpeg export, quality settings, aspose.barcode, complexbarcodegenerator, swissqrcodetext

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Swiss QR Code and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts an optional JPEG quality argument (0‑100) and creates the barcode image.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can specify JPEG quality.</param>
    static void Main(string[] args)
    {
        // Default JPEG quality (placeholder – actual quality is controlled by resolution/anti‑alias settings).
        int jpegQuality = 90;

        // Parse optional quality argument if provided.
        if (args.Length > 0 && int.TryParse(args[0], out int q) && q >= 0 && q <= 100)
        {
            jpegQuality = q;
        }

        // --------------------------------------------------------------------
        // Prepare Swiss QR Code data (creditor, account, amount, version, etc.).
        // --------------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // --------------------------------------------------------------------
        // Generate the Swiss QR barcode using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set image resolution and anti‑aliasing for better web quality.
            generator.Parameters.Resolution = 300f;
            generator.Parameters.UseAntiAlias = true;

            // Define output file path.
            string outputPath = "SwissQR.jpeg";

            // Save the barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);

            // Inform the user about the saved file and the quality placeholder.
            Console.WriteLine($"Swiss QR Code saved to '{outputPath}' (JPEG quality placeholder: {jpegQuality}).");
        }
    }
}
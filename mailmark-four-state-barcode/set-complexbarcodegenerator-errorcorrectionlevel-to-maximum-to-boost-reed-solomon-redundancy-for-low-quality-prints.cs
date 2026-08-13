// Title: Generate Swiss QR Code with Maximum Reed‑Solomon Error Correction
// Description: Demonstrates how to create a Swiss QR bill barcode and set the QR error correction level to the highest (Level H) to improve readability on low‑quality prints.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows usage of ComplexBarcodeGenerator, SwissQRCodetext, and QR parameters to produce a Swiss QR bill image. Developers working with payment QR codes often need to adjust error correction levels, colors, and output formats, making this a common pattern in financial and invoicing applications.
// Prompt: Set ComplexBarcodeGenerator ErrorCorrectionLevel to maximum to boost Reed‑Solomon redundancy for low‑quality prints.
// Tags: swiss qr, error correction, png, complexbarcodegenerator, swissqrcodetext, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Swiss QR bill barcode with maximum error correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the Swiss QR codetext, configures the generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR codetext with required fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator for the Swiss QR codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set maximum Reed‑Solomon error correction (Level H) for QR part
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set colors for better visibility
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image as PNG
            string outputPath = "SwissQR_MaxError.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}
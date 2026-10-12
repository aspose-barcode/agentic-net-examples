// Title: Generate Swiss QR Code with High Error Correction Level
// Description: Demonstrates how to create a Swiss QR Code (QR‑Bill) and set its QR error correction level to High, then save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator and SwissQRCodetext classes. It shows how to configure QR‑Bill data, adjust QR error correction levels, and export the result to common image formats. Developers working with payment QR codes, especially Swiss QR‑Bills, often need to control error correction to ensure readability under distortion.
// Prompt: Set a specific QR error correction level for Swiss QR Code generation to ensure readability under distortion.
// Tags: barcode, swiss qr code, error correction, png, complexbarcodegenerator, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss QR Code (QR‑Bill) with a high QR error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the QR‑Bill data, sets the error correction level,
    /// and saves the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Define and ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR_Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the Swiss QR Code payload (QR‑Bill) with creditor and payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Initialize the complex barcode generator using the Swiss QR payload
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure the QR error correction level to High (Level H) for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the full path for the output PNG file
            string outputPath = Path.Combine(outputDir, "SwissQR_HighErrorCorrection.png");

            // Render and save the barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
        }
    }
}
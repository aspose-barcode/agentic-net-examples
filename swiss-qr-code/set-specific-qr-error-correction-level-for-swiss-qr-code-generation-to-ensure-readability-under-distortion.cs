// Title: Generate Swiss QR Code with High Error Correction Level
// Description: Demonstrates creating a Swiss QR Bill using Aspose.BarCode and setting the QR error correction level to Level H to improve readability under distortion.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with SwissQRCodetext to produce a Swiss QR Bill. Typical use cases include generating payment QR codes for Swiss banking applications, where developers need to control QR error correction levels to ensure reliable scanning even when the image is distorted or partially obscured.
// Prompt: Set a specific QR error correction level for Swiss QR Code generation to ensure readability under distortion.
// Tags: swiss qr, qr error correction, barcode generation, aspose.barcode, complexbarcode, png output, payment

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Swiss QR Bill and configures a high QR error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Swiss QR Code with Level H error correction and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR Code data structure with creditor and payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator using the Swiss QR data
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure the QR part to use the highest error correction level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Determine a temporary file path for the output PNG image
            string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");

            // Render and save the barcode image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
        }
    }
}
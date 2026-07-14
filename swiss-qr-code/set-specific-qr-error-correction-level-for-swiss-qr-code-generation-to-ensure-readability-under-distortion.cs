// Title: Swiss QR Code Generation with High Error Correction
// Description: Demonstrates generating a Swiss QR Bill and setting QR error correction level H to improve readability under distortion.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator and SwissQRCodetext for creating Swiss QR Codes. Developers commonly need to generate QR codes for payment bills, adjust error correction levels, and export images in various formats. The example highlights key API classes and typical steps for QR code customization.
// Prompt: Set a specific QR error correction level for Swiss QR Code generation to ensure readability under distortion.
// Tags: qr code, swiss qr, error correction, barcode generation, aspose.barcode, png

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Swiss QR Bill with a high error correction level (Level H) and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the Swiss QR codetext, configures error correction, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create Swiss QR codetext and populate mandatory bill fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Initialize ComplexBarcodeGenerator with the codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set high error correction level (Level H) for better readability under distortion
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define output file path
            string outputPath = "SwissQR.png";

            // Save the generated Swiss QR Code image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the image was saved
            Console.WriteLine($"Swiss QR Code saved to {outputPath}");
        }
    }
}
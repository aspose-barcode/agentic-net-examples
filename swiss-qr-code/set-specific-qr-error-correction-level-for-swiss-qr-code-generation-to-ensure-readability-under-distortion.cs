// Title: Generate Swiss QR Bill with High Error Correction Level
// Description: Demonstrates creating a Swiss QR Bill barcode and setting a high QR error correction level to improve readability under distortion.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcodes such as Swiss QR Bills. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and QR error correction settings. Developers often need to generate compliant QR bills with specific error correction levels for reliable scanning in challenging conditions.
// Prompt: Set a specific QR error correction level for Swiss QR Code generation to ensure readability under distortion.
// Tags: swiss qr, qr error correction, barcode generation, aspnet, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Swiss QR Bill barcode with a high error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates Swiss QR data, configures error correction, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR bill data (mandatory fields)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create ComplexBarcodeGenerator for Swiss QR
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set high error correction level to improve readability under distortion
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Write the PNG bytes to a file
                File.WriteAllBytes("SwissQR.png", ms.ToArray());
            }
        }

        Console.WriteLine("Swiss QR code generated with high error correction level: SwissQR.png");
    }
}
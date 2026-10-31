// Title: Generate Swiss QR Code with High Error Correction Level
// Description: Demonstrates creating a Swiss QR Bill and encoding it as a QR Code with error correction level H to improve readability under distortion.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator and SwissQRCodetext classes. It shows how to configure QR error correction levels, a common requirement for payment QR codes and other high-reliability scanning scenarios. Developers often need to generate QR codes with specific error correction to ensure successful reads even when the image is damaged or distorted.
/// Prompt: Set a specific QR error correction level for Swiss QR Code generation to ensure readability under distortion.
/// Tags: barcode, swissqr, qrcode, error-correction, aspnet, aspose.barcode, png, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss QR Bill and saves it as a PNG image
/// with QR error correction level H to enhance readability under distortion.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize Swiss QR Bill data
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator for the Swiss QR data
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set QR error correction level to High (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode to a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Write the PNG bytes to a file
                File.WriteAllBytes("SwissQR.png", ms.ToArray());
                Console.WriteLine("Swiss QR Code saved as SwissQR.png with error correction Level H.");
            }
        }
    }
}
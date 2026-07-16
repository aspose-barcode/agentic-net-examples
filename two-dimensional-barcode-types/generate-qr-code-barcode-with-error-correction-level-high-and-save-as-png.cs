// Title: Generate QR Code with High Error Correction and Save as PNG
// Description: Demonstrates creating a QR Code barcode with high error correction level (Level H) and saving it as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows setting QR-specific parameters such as error correction level, a common requirement for developers needing robust QR codes for marketing, product tracking, or data encoding where damage tolerance is critical. Typical use cases include generating QR codes for URLs, contact info, or inventory data in automated workflows.
// Prompt: Generate a QR Code barcode with error correction level high and save as PNG.
// Tags: qr code, error correction, png, generation, aspose.barcode, barcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode with high error correction
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Configure the QR Code to use the highest error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the generated barcode to a PNG file named "qr.png".
            generator.Save("qr.png");
        }
    }
}
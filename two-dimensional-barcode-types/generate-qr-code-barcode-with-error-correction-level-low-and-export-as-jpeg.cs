// Title: Generate QR Code with Low Error Correction and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with low error correction level and exporting it as a JPEG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to encode data into a QR Code, configure its error correction level, and save the result in a common image format. Developers working with barcode creation often need to customize symbology settings and output formats for integration into web or desktop applications.
// Prompt: Generate a QR Code barcode with error correction level low and export as JPEG.
// Tags: qr code, barcode generation, error correction, jpeg, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with low error correction level and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary directory, generates the QR Code, and saves it.
    /// </summary>
    static void Main()
    {
        // Define output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "QrCodeExample");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the output JPEG file
        string outputPath = Path.Combine(outputDir, "qr_low.jpg");

        // Initialize the barcode generator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose QR"))
        {
            // Set the QR Code error correction level to low (Level L)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;
            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine("QR Code saved to: " + outputPath);
    }
}
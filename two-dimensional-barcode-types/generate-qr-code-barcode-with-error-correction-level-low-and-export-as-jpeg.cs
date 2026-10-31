// Title: Generate QR Code with Low Error Correction and Save as JPEG
// Description: Demonstrates creating a QR Code barcode with low error correction level and exporting it as a JPEG image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows setting QR error correction level, specifying output format, and saving the image. Developers working with barcode creation, especially QR codes, often need to adjust error correction and export to common image formats.
// Prompt: Generate a QR Code barcode with error correction level low and export as JPEG.
// Tags: qr code, error correction, jpeg, barcode generation, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with low error correction and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output directory inside the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full path for the JPEG file
        string outputPath = Path.Combine(outputDir, "qr_low.jpg");

        // Initialize the barcode generator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code error correction level to low (Level L)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;
            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code with low error correction saved to: {outputPath}");
    }
}
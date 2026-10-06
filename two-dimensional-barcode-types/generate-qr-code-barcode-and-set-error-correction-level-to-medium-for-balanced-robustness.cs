// Title: Generate QR Code with Medium Error Correction Level
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, setting the error correction level to medium (Level M) for a balance between data capacity and robustness, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel classes to configure QR Code parameters such as error correction. Developers commonly use these APIs to create QR codes for URLs, contact info, or product data, adjusting error correction to meet scanning reliability requirements.
// Prompt: Generate QR Code barcode and set error correction level to medium for balanced robustness.
// Tags: qr code, barcode generation, error correction, medium level, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with medium error correction level and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the PNG image.
        string outputPath = Path.Combine(outputDir, "QrCode_M.png");

        // Initialize the barcode generator for QR Code symbology with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the QR Code error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}
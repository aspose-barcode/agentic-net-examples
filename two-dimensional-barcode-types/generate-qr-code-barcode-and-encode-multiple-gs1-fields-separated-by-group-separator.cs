// Title: Generate GS1 QR Code with Multiple Application Identifiers
// Description: Demonstrates how to create a GS1 QR Code barcode containing several GS1 Application Identifier fields and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code symbology and GS1 data encoding. It showcases the use of BarcodeGenerator, EncodeTypes, and QR error correction settings to produce GS1‑compliant QR codes. Developers often need to embed product information such as GTIN, serial numbers, and batch numbers in a single QR code for scanning in supply‑chain applications.
// Prompt: Generate QR Code barcode and encode multiple GS1 fields separated by group separator.
// Tags: qr code, gs1, barcode, generation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program demonstrating generation of a GS1 QR Code with multiple Application Identifier fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a unique output file path in the system temporary folder
        string outputFile = Path.Combine(Path.GetTempPath(), "Gs1Qr_" + Guid.NewGuid().ToString("N") + ".png");
        string outputDir = Path.GetDirectoryName(outputFile);

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // GS1 QR code data containing multiple Application Identifiers (AI)
        // (01) – GTIN, (21) – Serial Number, (30) – Quantity
        string codeText = "(01)00123456789012(21)ABC123(30)9876";

        // Initialize the barcode generator for GS1 QR symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1QR, codeText))
        {
            // Set the module (pixel) size of the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Optional: configure error correction level (Level M provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG image
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine("GS1 QR code generated at: " + outputFile);
    }
}
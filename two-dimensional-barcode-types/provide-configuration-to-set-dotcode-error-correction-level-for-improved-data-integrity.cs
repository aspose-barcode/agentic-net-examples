// Title: Demonstrate DotCode barcode generation and error‑correction handling
// Description: Shows how to generate a DotCode barcode (error correction is automatic) and a QR Code barcode with an explicit error‑correction level.
// Category-Description: This example is part of the Aspose.BarCode barcode generation collection, focusing on creating DotCode and QR Code symbols using the BarcodeGenerator class. It highlights key API classes such as EncodeTypes, BarcodeGenerator, and BarCodeImageFormat, typical use cases like setting layout parameters and error‑correction levels, and common developer needs for saving barcodes as images.
// Prompt: Provide configuration to set DotCode error correction level for improved data integrity.
// Tags: dotcode, barcode, error correction, qrcode, generation, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample DotCode and QR Code barcodes, demonstrating error‑correction handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, generates barcodes, and saves them as PNG images.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Generate a DotCode barcode.
        // Note: DotCode error correction is applied automatically and cannot be set via the API.
        // ------------------------------------------------------------
        using (var dotCodeGenerator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            // Optional: adjust layout if desired
            dotCodeGenerator.Parameters.Barcode.DotCode.Columns = 18; // set number of columns
            dotCodeGenerator.Parameters.Barcode.DotCode.Rows = -1;   // rows auto‑calculated

            // Save the DotCode image
            string dotCodePath = Path.Combine(outputFolder, "DotCode.png");
            dotCodeGenerator.Save(dotCodePath, BarCodeImageFormat.Png);
            Console.WriteLine($"DotCode barcode saved to: {dotCodePath}");
        }

        // ------------------------------------------------------------
        // Generate a QR Code barcode with explicit high error correction level (for comparison).
        // ------------------------------------------------------------
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR"))
        {
            // Set error correction level to High (approximately 30% data recovery)
            qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the QR Code image
            string qrPath = Path.Combine(outputFolder, "QR_HighErrorCorrection.png");
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
            Console.WriteLine($"QR barcode with high error correction saved to: {qrPath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}
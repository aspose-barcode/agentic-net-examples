// Title: Demonstrate DotCode generation and QR Code error correction configuration
// Description: Shows how to generate a DotCode barcode (where error correction is automatic) and how to configure the error correction level for a QR Code using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with different EncodeTypes. It covers creating barcodes, adjusting parameters such as module size and QR error correction levels, and saving images. Developers working with barcode creation, especially those needing to control data integrity via error correction, will find these patterns useful.
// Prompt: Provide configuration to set DotCode error correction level for improved data integrity.
// Tags: dotcode, qrcode, error-correction, barcode-generation, aspose.barcode, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point for the barcode generation example.
/// Demonstrates DotCode creation (with automatic error correction) and QR Code error correction configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DotCode barcode and a QR Code with high error correction, then saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "DotCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Example 1: Generate a DotCode barcode (error correction is automatic and not configurable)
        // ------------------------------------------------------------
        using (var dotGen = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            // Optional: set the size of each module (pixel dimension)
            dotGen.Parameters.Barcode.XDimension.Pixels = 10;

            // Define the output file path for the DotCode image
            string dotPath = Path.Combine(outputFolder, "DotCode.png");

            // Save the generated DotCode barcode as a PNG image
            dotGen.Save(dotPath, BarCodeImageFormat.Png);
            Console.WriteLine("DotCode barcode saved to: " + dotPath);
        }

        // ------------------------------------------------------------
        // Example 2: Generate a QR Code barcode with configurable error correction level
        // ------------------------------------------------------------
        using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR"))
        {
            // Set error correction level to High (Level H) for maximum data integrity
            qrGen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the output file path for the QR Code image
            string qrPath = Path.Combine(outputFolder, "QrCode_HighErrorCorrection.png");

            // Save the QR Code barcode as a PNG image
            qrGen.Save(qrPath, BarCodeImageFormat.Png);
            Console.WriteLine("QR Code with high error correction saved to: " + qrPath);
        }

        Console.WriteLine("Processing completed.");
    }
}
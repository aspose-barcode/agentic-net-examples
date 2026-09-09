// Title: Multi-Decode Barcode Generation and Recognition for UPC-A, UPC-E, and EAN-8
// Description: Demonstrates generating PNG images for UPC-A, UPC-E, and EAN-8 barcodes and reading them using a MultiDecodeType configuration.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with MultiDecodeType for decoding multiple symbologies in a single pass. Developers often need to batch‑process retail barcodes such as UPC‑A, UPC‑E, and EAN‑8, and this pattern provides a concise solution.
// Prompt: Configure MultyDecodeType to include UPC-A, UPC-E, and EAN-8 for comprehensive retail barcode scanning.
// Tags: barcode symbology, multi-decode, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading UPC-A, UPC-E, and EAN-8 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images, configures MultiDecodeType, reads the barcodes, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data for each barcode type (encode type, text, output file name)
        var samples = new (BaseEncodeType encodeType, string codeText, string fileName)[]
        {
            (EncodeTypes.UPCA, "012345678905", "upca.png"),
            (EncodeTypes.UPCE, "01234565", "upce.png"),
            (EncodeTypes.EAN8, "12345670", "ean8.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Configure MultiDecodeType to include UPC-A, UPC-E, and EAN-8 symbologies
        var multiDecode = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);

        // Read each generated barcode using the configured MultiDecodeType
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, multiDecode))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)} => Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}
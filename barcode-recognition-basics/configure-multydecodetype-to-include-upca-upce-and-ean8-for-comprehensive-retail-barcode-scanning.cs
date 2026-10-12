// Title: Multi-Decode Barcode Generation and Recognition for UPC-A, UPC-E, and EAN-8
// Description: Demonstrates generating PNG images for UPC-A, UPC-E, and EAN-8 barcodes and recognizing them using MultiDecodeType.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcode images, BarCodeReader with MultiDecodeType to decode multiple symbologies in a single pass, and common retail scanning scenarios. Developers often need to generate barcode assets and then validate them across various formats such as UPC-A, UPC-E, and EAN-8.
// Prompt: Configure MultyDecodeType to include UPC-A, UPC-E, and EAN-8 for comprehensive retail barcode scanning.
// Tags: upc-a, upc-e, ean-8, multidecode, barcode-generation, barcode-recognition, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample UPC-A, UPC-E, and EAN-8 barcodes, saves them as PNG files,
/// and then reads them back using a MultiDecodeType that includes all three symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, decodes them,
    /// and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for storing generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data for each barcode symbology
        var samples = new (BaseEncodeType encodeType, string codeText, string fileName)[]
        {
            (EncodeTypes.UPCA, "012345678905", "upca.png"),
            (EncodeTypes.UPCE, "0123456", "upce.png"),
            (EncodeTypes.EAN8, "12345670", "ean8.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                // Set X-dimension (module width) to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
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
                    Console.WriteLine($"{Path.GetFileName(filePath)} - Detected: {result.CodeTypeName} => {result.CodeText}");
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
            // Ignore any errors during cleanup
        }
    }
}
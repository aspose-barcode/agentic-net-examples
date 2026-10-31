// Title: Multi-Decode Barcode Generation and Recognition for UPC-A, UPC-E, and EAN-8
// Description: Demonstrates generating barcode images for UPC-A, UPC-E, and EAN-8, then configuring MultiDecodeType to read them in a single pass.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and MultiDecodeType to handle multiple symbologies simultaneously. Typical scenarios include retail scanning systems that must recognize various product barcode formats in one operation.
// Prompt: Configure MultyDecodeType to include UPC-A, UPC-E, and EAN-8 for comprehensive retail barcode scanning.
// Tags: barcode symbology, generation, recognition, multidecode, upc-a, upc-e, ean-8, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates UPC-A, UPC-E, and EAN-8 barcodes, then reads them using a MultiDecodeType configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, configures multi‑decode,
    /// reads each image, and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode type, data, and output file name for each symbology
        var barcodeInfos = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.UPCA, "012345678905", "upca.png"),
            (EncodeTypes.UPCE, "01234565", "upce.png"),
            (EncodeTypes.EAN8, "12345670", "ean8.png")
        };

        // -----------------------------------------------------------------
        // Generate barcode images and save them to the temporary folder
        // -----------------------------------------------------------------
        foreach (var info in barcodeInfos)
        {
            using (var generator = new BarcodeGenerator(info.encodeType, info.codeText))
            {
                string path = Path.Combine(tempFolder, info.fileName);
                generator.Save(path, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated {info.encodeType} barcode at {path}");
            }
        }

        // ---------------------------------------------------------------
        // Configure MultiDecodeType to recognize UPC-A, UPC-E, and EAN-8
        // ---------------------------------------------------------------
        var multiDecode = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);

        // ---------------------------------------------------------------
        // Read each generated barcode using the configured MultiDecodeType
        // ---------------------------------------------------------------
        foreach (var info in barcodeInfos)
        {
            string imagePath = Path.Combine(tempFolder, info.fileName);
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            using (var reader = new BarCodeReader(imagePath, multiDecode))
            {
                Console.WriteLine($"Reading {info.fileName}:");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // ---------------------------------------------------------------
        // Clean up temporary files and folder
        // ---------------------------------------------------------------
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
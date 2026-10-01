// Title: Configure MultiDecodeType for UPC-A, UPC-E, and EAN-8 Barcode Scanning
// Description: Demonstrates generating UPC-A, UPC-E, and EAN-8 barcodes, then decoding them using a custom MultiDecodeType array.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with a specified BaseDecodeType[] to limit decoding to particular symbologies. Developers often need to generate retail barcodes and then scan them efficiently, selecting only the required decode types to improve performance and accuracy.
// Prompt: Configure MultyDecodeType to include UPC-A, UPC-E, and EAN-8 for comprehensive retail barcode scanning.
// Tags: barcode symbology, generation, recognition, upc-a, upc-e, ean-8, multidecodetype, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates UPC-A, UPC-E, and EAN-8 barcodes,
/// then decodes them using a specified set of decode types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode images, decodes them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode specifications: type, text, and output file name
        var barcodeInfos = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.UPCA, "012345678905", "upca.png"),   // UPC-A (12 digits)
            (EncodeTypes.UPCE, "012345", "upce.png"),         // UPC-E (6 digits)
            (EncodeTypes.EAN8, "12345670", "ean8.png")        // EAN-8 (8 digits)
        };

        // Generate barcode images for each specification
        foreach (var info in barcodeInfos)
        {
            string filePath = Path.Combine(tempFolder, info.fileName);
            using (var generator = new BarcodeGenerator(info.type, info.text))
            {
                // Optional visual settings
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated {info.type} barcode at: {filePath}");
            }
        }

        // Prepare the decode types to include UPC-A, UPC-E, and EAN-8
        BaseDecodeType[] decodeTypes = new BaseDecodeType[]
        {
            DecodeType.UPCA,
            DecodeType.UPCE,
            DecodeType.EAN8
        };

        // Read and decode each barcode using the specified decode types
        foreach (var info in barcodeInfos)
        {
            string filePath = Path.Combine(tempFolder, info.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, decodeTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {info.fileName}");
                    Console.WriteLine($"  Detected Type : {result.CodeType}");
                    Console.WriteLine($"  Code Text     : {result.CodeText}");
                }
            }
        }

        // Cleanup: delete temporary files and folder
        try
        {
            foreach (var info in barcodeInfos)
            {
                string filePath = Path.Combine(tempFolder, info.fileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}
// Title: Multi-Decode UPC-A, UPC-E, and EAN-8 Barcode Scanning Example
// Description: Demonstrates generating UPC-A, UPC-E, and EAN-8 barcodes and configuring a BarCodeReader to decode them in a single pass.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of MultiDecodeType with BarCodeReader to detect multiple symbologies simultaneously. It highlights key classes such as BarcodeGenerator, BarCodeReader, MultiDecodeType, and DecodeType, which developers commonly use for retail barcode scanning, inventory management, and point‑of‑sale applications.
// Prompt: Configure MultyDecodeType to include UPC-A, UPC-E, and EAN-8 for comprehensive retail barcode scanning.
// Tags: barcode symbology, decoding, console output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample UPC-A, UPC-E, and EAN-8 barcodes and reads them using a multi‑decode configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, configures a BarCodeReader with MultiDecodeType,
    /// and outputs detected barcode information to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a folder for sample barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate sample UPC-A barcode if it does not already exist
        string upcAPath = Path.Combine(folderPath, "upca.png");
        if (!File.Exists(upcAPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
            {
                generator.Save(upcAPath);
            }
        }

        // Generate sample UPC-E barcode if it does not already exist
        string upcEPath = Path.Combine(folderPath, "upce.png");
        if (!File.Exists(upcEPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCE, "01234565"))
            {
                generator.Save(upcEPath);
            }
        }

        // Generate sample EAN-8 barcode if it does not already exist
        string ean8Path = Path.Combine(folderPath, "ean8.png");
        if (!File.Exists(ean8Path))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN8, "96385074"))
            {
                generator.Save(ean8Path);
            }
        }

        // Configure a BarCodeReader to detect UPC-A, UPC-E, and EAN-8 in a single pass
        using (var reader = new BarCodeReader())
        {
            // MultiDecodeType includes the three desired symbologies
            var multiDecode = new MultiDecodeType(DecodeType.UPCA, DecodeType.UPCE, DecodeType.EAN8);
            reader.BarCodeReadType = multiDecode;

            // Process each generated image
            string[] imageFiles = new[] { upcAPath, upcEPath, ean8Path };
            foreach (string imageFile in imageFiles)
            {
                if (!File.Exists(imageFile))
                {
                    Console.WriteLine($"File not found: {imageFile}");
                    continue;
                }

                // Load the image into the reader
                reader.SetBarCodeImage(imageFile);

                // Read and display all detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Image: {Path.GetFileName(imageFile)}");
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine();
                }
            }
        }
    }
}
// Title: Detect EAN13 barcodes using BarCodeReader
// Description: Demonstrates generating an EAN13 barcode image and reading it back with DecodeType set to EAN13, ensuring only European Article Number symbology is detected.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader with specific DecodeType filtering. Developers often need to generate barcodes for product labeling and then validate or extract them from images, focusing on particular symbologies such as EAN13 for retail applications.
// Prompt: Use BarCodeReader with DecodeType set to EAN13 to exclusively detect European Article Number barcodes.
// Tags: ean13, barcode, generation, recognition, decode, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an EAN13 barcode image and reading it using <see cref="BarCodeReader"/> with <see cref="DecodeType.EAN13"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an EAN13 barcode, saves it, and reads it back exclusively as EAN13.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "ean13.png";

        // Create an EAN13 barcode with a valid 13‑digit value (including checksum) and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image was successfully created before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not found.");
            return;
        }

        // Initialize a reader that is configured to decode only EAN13 symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Iterate through all detected barcodes (expected to be a single EAN13 entry).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}
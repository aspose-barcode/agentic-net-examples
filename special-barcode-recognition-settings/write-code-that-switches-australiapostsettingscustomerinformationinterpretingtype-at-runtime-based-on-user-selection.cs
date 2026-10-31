// Title: Dynamic Switching of AustraliaPost CustomerInformationInterpretingType
// Description: Demonstrates how to generate and read Australia Post barcodes while changing the CustomerInformationInterpretingType at runtime.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and the AustraliaPost settings to handle different interpreting tables (C‑Table, N‑Table, Other). Developers often need to switch interpreting types based on user input or business rules, and this snippet provides a clear pattern for doing so.
// Prompt: Write code that switches AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on user selection.
// Tags: barcode, australia post, customer information, interpreting type, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates dynamic switching of AustraliaPost <c>CustomerInformationInterpretingType</c> during barcode generation and recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates barcodes for each <c>CustomerInformationInterpretingType</c>, saves them, and reads them back using the matching interpreting setting.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample code text without customer information (valid for all interpreting types)
        string codeText = "6201234567";

        // Define the interpreting types to demonstrate
        CustomerInformationInterpretingType[] types = new CustomerInformationInterpretingType[]
        {
            CustomerInformationInterpretingType.CTable,
            CustomerInformationInterpretingType.NTable,
            CustomerInformationInterpretingType.Other
        };

        // Iterate over each interpreting type, generate and read the barcode
        foreach (CustomerInformationInterpretingType type in types)
        {
            // Build the file path for the current barcode image
            string filePath = Path.Combine(outputDir, $"{type}.png");

            // Generate barcode with the specific encoding table
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;
                generator.Parameters.Barcode.AustralianPost.EncodingTable = type;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify that the image file was created before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode image for {type}");
                continue;
            }

            // Read the barcode using a matching interpreting type
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
            {
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = type;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Interpreting Type: {type}");
                    Console.WriteLine($"  Code Type : {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text : {result.CodeText}");
                }
            }
        }

        // Output the location of generated barcode images
        Console.WriteLine($"Barcodes generated in: {outputDir}");
    }
}
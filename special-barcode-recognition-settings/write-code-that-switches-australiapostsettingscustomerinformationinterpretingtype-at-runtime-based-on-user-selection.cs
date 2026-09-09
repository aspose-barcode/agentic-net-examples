// Title: Dynamic Australia Post Customer Information Interpreting Type Example
// Description: Demonstrates how to set the AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on a command‑line argument, then generate and read a barcode using that setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It shows how to work with EncodeTypes.AustraliaPost, configure the AustralianPost.EncodingTable, and adjust the CustomerInformationInterpretingType for both encoding and decoding. Developers often need to switch interpreting modes (CTable, DTable, etc.) dynamically depending on business rules or user input.
// Prompt: Write code that switches AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on user selection.
// Tags: barcode symbology, australia post, interpreting type, runtime selection, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates runtime selection of Australia Post customer information interpreting type,
/// barcode generation, and subsequent recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses an optional command‑line argument to select the interpreting type,
    /// generates an Australia Post barcode, saves it, and reads it back using the same setting.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a CustomerInformationInterpretingType value.</param>
    static void Main(string[] args)
    {
        // Determine interpreting type from first argument or default to CTable
        CustomerInformationInterpretingType interpretingType = CustomerInformationInterpretingType.CTable;
        if (args.Length > 0)
        {
            if (Enum.TryParse<CustomerInformationInterpretingType>(args[0], true, out var parsed))
            {
                interpretingType = parsed;
            }
            else
            {
                Console.WriteLine($"Invalid interpreting type '{args[0]}', using default CTable.");
            }
        }

        // Prepare a unique temporary output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "AustraliaPost.png");

        // Sample code text (FCC 59 + 8‑digit DPID, no customer info) – valid for all interpreting types
        string codeText = "5901234567";

        // Generate barcode with the selected interpreting type
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = interpretingType;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");
        Console.WriteLine($"Interpreting type used for generation: {interpretingType}");

        // Read barcode using the same interpreting type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}
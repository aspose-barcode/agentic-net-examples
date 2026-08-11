// Title: Dynamic AustraliaPost Customer Information Interpreting Type
// Description: Demonstrates how to switch the AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on a command‑line argument and generate/recognize the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It shows usage of BarcodeGenerator, BarCodeReader, and the AustraliaPost settings such as EncodingTable and CustomerInformationInterpretingType. Developers often need to create or read Australia Post barcodes with different customer information tables (CTable, NTable, Other) depending on business rules.
// Prompt: Write code that switches AustraliaPostSettings.CustomerInformationInterpretingType at runtime based on user selection.
// Tags: barcode symbology, australia post, customer information, interpreting type, runtime selection, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates runtime selection of Australia Post customer information interpreting type,
/// barcode generation, and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses a command‑line argument to choose the interpreting type,
    /// builds a valid Australia Post codetext, and calls the generation/reading routine.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument selects the interpreting type.</param>
    static void Main(string[] args)
    {
        // Determine interpreting type from command‑line argument or default to CTable
        string typeArg = args.Length > 0 ? args[0] : "CTable";
        CustomerInformationInterpretingType interpretingType = typeArg switch
        {
            "CTable" => CustomerInformationInterpretingType.CTable,
            "NTable" => CustomerInformationInterpretingType.NTable,
            "Other" => CustomerInformationInterpretingType.Other,
            _ => CustomerInformationInterpretingType.CTable
        };

        // Build a valid AustraliaPost codetext for the selected type
        // FCC 59 allows up to 5 CTable chars or 10 NTable digits or 4 symbols (0‑3) for Other.
        string fcc = "59";
        string dpid = "01234567"; // 8‑digit DPID
        string customerInfo = interpretingType switch
        {
            CustomerInformationInterpretingType.CTable => "ABCD",   // letters allowed, <=5 chars
            CustomerInformationInterpretingType.NTable => "1234",   // digits only
            CustomerInformationInterpretingType.Other => "0123",   // symbols 0‑3 only
            _ => ""
        };
        string codeText = fcc + dpid + customerInfo;

        string outputPath = "AustraliaPostBarcode.png";

        GenerateAndReadBarcode(codeText, interpretingType, outputPath);
    }

    static void GenerateAndReadBarcode(string codeText, CustomerInformationInterpretingType type, string outputPath)
    {
        // Generate barcode image with the specified interpreting type
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.AustralianPost.EncodingTable = type;

            using (MemoryStream ms = new MemoryStream())
            {
                // Save barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                // Write the image to a file for visual verification (optional)
                File.WriteAllBytes(outputPath, ms.ToArray());

                // Reset stream position for reading
                ms.Position = 0;

                // Recognize the barcode using the same interpreting type
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = type;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Interpreting Type: {type}");
                        Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}
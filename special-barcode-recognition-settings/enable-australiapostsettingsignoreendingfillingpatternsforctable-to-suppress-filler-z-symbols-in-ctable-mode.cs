// Title: Suppress filler symbols in Australia Post CTable barcode decoding
// Description: Demonstrates how to generate an Australia Post barcode using CTable encoding and decode it while ignoring trailing filler "z" symbols.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Australia Post barcodes and BarCodeReader for decoding them, highlighting settings such as AustralianPost.EncodingTable and IgnoreEndingFillingPatternsForCTable. Developers working with postal symbologies often need to control filler characters during decoding, making this pattern useful for accurate data extraction.
// Prompt: Enable AustraliaPostSettings.IgnoreEndingFillingPatternsForCTable to suppress filler "z" symbols in CTable mode.
// Tags: australia post, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates an Australia Post barcode in CTable mode,
/// saves it as PNG, and reads it while suppressing trailing filler symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and decodes it with filler suppression.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo");
        Directory.CreateDirectory(outputDir);

        // Define the full file path for the PNG barcode image.
        string barcodePath = Path.Combine(outputDir, "AustraliaPostCTable.png");

        // Generate an Australia Post barcode using CTable encoding.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567ASPOSE"))
        {
            // Set visual parameters: module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Choose CTable as the customer information interpreting type.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode image as PNG.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a reader to decode the saved barcode.
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Ensure the reader interprets the customer information using CTable.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Suppress trailing filler "z" symbols in the decoded result.
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

            // Iterate through all decoded results and output the code text.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }
    }
}
// Title: Suppress filler symbols in Australia Post CTable barcode reading
// Description: Demonstrates generating an Australia Post barcode using CTable encoding and reading it with and without ignoring ending filler patterns.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and specific AustraliaPostSettings to control decoding behavior. Developers working with postal barcodes often need to customize encoding tables and handle filler characters, making this pattern common in logistics and mailing applications.
// Prompt: Enable AustraliaPostSettings.IgnoreEndingFillingPatternsForCTable to suppress filler "z" symbols in CTable mode.
// Tags: australia post, barcode generation, barcode reading, png, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates an Australia Post barcode using CTable encoding, then reads it twice:
/// once with default settings (including filler symbols) and once with
/// IgnoreEndingFillingPatternsForCTable enabled to suppress the filler "z" symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, two read operations,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "AustraliaPostCTable.png");

        // ------------------------------------------------------------
        // Generate an Australia Post barcode with CTable encoding
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567ASPOSE"))
        {
            // Set visual appearance
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Use CTable encoding for the customer information part
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine();

        // ------------------------------------------------------------
        // Read the barcode without ignoring filler patterns
        // ------------------------------------------------------------
        Console.WriteLine("Reading without IgnoreEndingFillingPatternsForCTable:");
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Ensure the reader expects CTable encoding
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Iterate through all detected barcodes (should be one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
            }
        }

        Console.WriteLine();

        // ------------------------------------------------------------
        // Read the barcode with IgnoreEndingFillingPatternsForCTable enabled
        // ------------------------------------------------------------
        Console.WriteLine("Reading with IgnoreEndingFillingPatternsForCTable = true:");
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Set CTable decoding mode
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Suppress trailing filler "z" symbols in the decoded text
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("CodeText: " + result.CodeText);
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}
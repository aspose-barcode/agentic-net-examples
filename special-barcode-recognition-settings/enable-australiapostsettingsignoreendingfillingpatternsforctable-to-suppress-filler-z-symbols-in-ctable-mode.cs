// Title: Suppress filler symbols in Australia Post CTable barcode decoding
// Description: Demonstrates how to enable IgnoreEndingFillingPatternsForCTable to remove trailing "z" filler symbols when decoding Australia Post barcodes in CTable mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It shows usage of BarcodeGenerator, BarCodeReader, and related settings such as AustralianPostEncodingTable and IgnoreEndingFillingPatternsForCTable. Developers often need to generate barcodes and accurately decode them while handling filler patterns, especially in logistics and mailing applications.
// Prompt: Enable AustraliaPostSettings.IgnoreEndingFillingPatternsForCTable to suppress filler "z" symbols in CTable mode.
// Tags: australia post, ctable, ignore ending filling patterns, barcode generation, barcode recognition, aspnet.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an Australia Post barcode in CTable mode and decoding it while suppressing filler "z" symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and reads it back with specific settings.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Australia Post with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678AB"))
        {
            // Configure the generator to use the CTable interpreting type
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the generated barcode image to a file
            generator.Save("AustraliaPost.png");

            // Generate the barcode image in memory for immediate decoding
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader for the generated image, targeting Australia Post symbology
                using (BarCodeReader reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                {
                    // Set the reader to interpret customer information using CTable
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Enable suppression of ending filler patterns ("z") in CTable mode
                    reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Iterate through all detected barcodes and output their details
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}
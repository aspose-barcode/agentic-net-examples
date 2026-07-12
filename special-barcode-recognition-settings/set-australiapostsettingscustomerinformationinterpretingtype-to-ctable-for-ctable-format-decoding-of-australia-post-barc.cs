// Title: Decode Australia Post barcode using CTable format
// Description: Demonstrates setting CustomerInformationInterpretingType to CTable for decoding Australia Post barcodes and prints the decoded values.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, focusing on Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings to generate and decode barcodes, a common task for developers handling postal services integration.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to CTable for CTable format decoding of Australia Post barcodes.
// Tags: barcode symbology, australia post, decoding, ctable, aspose.barcode, generation, recognition

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating generation and CTable decoding of an Australia Post barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, decodes it using CTable interpreting type, and writes results to console.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode text (postal code + customer info)
        const string codeText = "5912345678AB";

        // Initialize a barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the generator to use CTable encoding for the customer information segment
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image in memory
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Create a barcode reader for the generated image, specifying Australia Post decoding
                using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
                {
                    // Set the reader to interpret the customer information using CTable format
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                    // Iterate through all decoded barcode results
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("BarCode Type: " + result.CodeType);
                        Console.WriteLine("BarCode CodeText: " + result.CodeText);
                    }
                }
            }
        }
    }
}
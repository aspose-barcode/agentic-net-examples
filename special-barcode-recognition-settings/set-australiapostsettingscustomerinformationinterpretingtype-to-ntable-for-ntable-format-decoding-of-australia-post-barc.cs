// Title: Decode Australia Post barcode using NTable format
// Description: Demonstrates setting CustomerInformationInterpretingType to NTable for both generation and decoding of Australia Post barcodes.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to configure the AustraliaPostSettings.CustomerInformationInterpretingType property for NTable format, a common requirement when working with Australia Post barcodes that include customer information. Developers often need to generate barcodes with specific encoding tables and then decode them accurately using matching settings.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to NTable for NTable format decoding of Australia Post barcodes.
// Tags: barcode symbology, australia post, ntable, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an Australia Post barcode using the NTable encoding
/// and then decodes it with the same NTable interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, verifies its creation,
    /// and reads the barcode back using NTable decoding settings.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode text (FCC 59, DPID 8 digits, no customer info)
        string codeText = "5980123456";

        // Output image path
        string imagePath = "AustraliaPost_NTable.png";

        // Generate the barcode with NTable encoding table
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the encoding table to NTable for generation
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Save the barcode image to the specified path
            generator.Save(imagePath);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read and decode the barcode, setting the decoding interpreting type to NTable
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Apply NTable interpreting type for decoding
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

            // Iterate through detected barcodes and output their details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeType}");
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }
    }
}
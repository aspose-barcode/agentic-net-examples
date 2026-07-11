// Title: Custom decoding of Australia Post barcodes using Other interpreting type
// Description: Demonstrates how to generate and read an Australia Post barcode with CustomerInformationInterpretingType set to Other, allowing custom handling of the customer information segment.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and AustraliaPostSettings to control encoding and decoding of Australia Post barcodes. Developers often need to customize how the customer information part of the barcode is interpreted, especially when integrating with proprietary systems.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to Other for custom decoding of Australia Post barcodes.
// Tags: barcode symbology, australia post, encoding, decoding, png, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates an Australia Post barcode with custom customer information interpretation
/// and then reads it back using the same custom settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it as PNG,
    /// verifies the file, and reads the barcode using the Other interpreting type.
    /// </summary>
    static void Main()
    {
        // Sample barcode text (customer information part can be empty for Other interpreting type)
        const string codeText = "59123456780123012301230123";
        const string imagePath = "AustraliaPost.png";

        // Generate Australia Post barcode with CustomerInformationInterpretingType set to Other
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Configure the generator to treat the customer information segment as 'Other' (no built‑in interpretation)
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.Other;

            // Save the generated barcode image in PNG format
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Initialize a reader for Australia Post barcodes
        using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Apply the same 'Other' interpreting type for decoding the customer information segment
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;

            // Iterate through all recognized barcodes (should be one in this case)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeType}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}
// Title: Australia Post barcode generation and NTable decoding example
// Description: Demonstrates how to generate an Australia Post barcode using the NTable encoding table and then decode it with NTable customer information interpretation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator and BarCodeReader classes. It illustrates typical use cases such as creating barcodes for postal services and decoding them with specific settings, which developers often need when integrating mailing solutions.
/// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to NTable for NTable format decoding of Australia Post barcodes.
/// Tags: barcode symbology, australia post, encoding, decoding, png, barcodegenerator, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Program demonstrating generation and recognition of an Australia Post barcode with NTable settings.
/// </summary>
class Program
{
    /// <summary>
    /// Generates an Australia Post barcode with NTable encoding, saves it as PNG, and then reads it back using NTable decoding.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string imagePath = "australia_post.png";

        // Ensure a clean start by deleting any existing file with the same name
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // -------------------- Barcode Generation --------------------
        // Create a generator for an Australia Post barcode with the sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678"))
        {
            // Configure the generator to use the NTable encoding (digits only)
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // -------------------- Barcode Recognition --------------------
        // Initialize a reader for the saved image, specifying the Australia Post decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Set the decoder to interpret customer information using the NTable format
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

            // Iterate through all detected barcodes and output their details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeType}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}
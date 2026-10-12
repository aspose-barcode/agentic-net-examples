// Title: Custom Australia Post barcode decoding with Aspose.BarCode
// Description: Shows how to generate an Australia Post barcode, assign a custom customer information decoder, and read the barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It demonstrates using BarcodeGenerator, BarCodeReader, and AustraliaPostSettings to customize decoding logic. Developers working with postal services often need to interpret custom customer information fields, and this snippet illustrates the typical workflow for such scenarios.
// Prompt: Instantiate AustraliaPostSettings and assign it to BarCodeReader.RecognitionSettings for custom Australia Post decoding.
// Tags: australia post, barcode, custom decoder, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Custom decoder for Australia Post customer information fields.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Decodes the raw customer information field.
    /// </summary>
    /// <param name="customerInformationField">The raw field extracted from the barcode.</param>
    /// <returns>A simple prefixed string representing the decoded value.</returns>
    public string Decode(string customerInformationField)
    {
        // Simple example: return the raw field prefixed
        return $"Decoded:{customerInformationField}";
    }
}

/// <summary>
/// Demonstrates custom decoding of Australia Post barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a sample Australia Post barcode, reads it with a custom customer information decoder, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "AustraliaPost.png");

        // Generate a sample Australia Post barcode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Use N-table for customer information encoding
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

            // Save the barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with a custom customer information decoder
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Configure recognition settings for Australia Post
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyDecoder();

            // Perform the reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}
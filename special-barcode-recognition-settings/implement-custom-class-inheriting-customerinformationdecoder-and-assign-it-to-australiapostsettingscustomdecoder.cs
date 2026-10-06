// Title: Custom Australia Post barcode decoder example
// Description: Demonstrates how to implement a custom CustomerInformationDecoder for Australia Post barcodes and use it during barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It shows the use of BarcodeGenerator, BarCodeReader, and the AustraliaPostSettings classes, which developers commonly employ to create, read, and interpret Australia Post barcodes with custom decoding logic.
// Prompt: Implement a custom class inheriting CustomerInformationDecoder and assign it to AustraliaPostSettings.CustomDecoder.
// Tags: australia post, barcode, custom decoder, generation, recognition, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder that inherits from <see cref="AustraliaPostCustomerInformationDecoder"/>.
/// This simple implementation returns the raw customer information field unchanged.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Decodes the supplied customer information field.
    /// </summary>
    /// <param name="customerInformationField">The raw customer information string extracted from the barcode.</param>
    /// <returns>The decoded (or in this case, unchanged) customer information.</returns>
    public string Decode(string customerInformationField)
    {
        // Simple example: return the field unchanged
        return customerInformationField;
    }
}

class Program
{
    /// <summary>
    /// Entry point of the example. Generates an Australia Post barcode, reads it back using a custom decoder,
    /// and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare temporary output directory and file path for the barcode image
        // -----------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "AustraliaPost.png");

        // -----------------------------------------------------------------
        // Generate a sample Australia Post barcode with specific settings
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // -----------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -----------------------------------------------------------------
        // Read the barcode using a custom customer information decoder
        // -----------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Match the interpreting type used during generation
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Assign the custom decoder (property name is CustomerInformationDecoder)
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyDecoder();

            // Iterate through all detected barcodes (should be one in this example)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Manually invoke the custom decoder if additional processing is required
                string decodedInfo = ((AustraliaPostCustomerInformationDecoder)reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder)
                                    .Decode(result.CodeText);
                Console.WriteLine($"Custom Decoded Info: {decodedInfo}");
            }
        }

        // -----------------------------------------------------------------
        // Optional cleanup of temporary files and directories
        // -----------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}
// Title: Generate MaxiCode (Mode 2) with Complex Primary Data
// Description: Demonstrates building a MaxiCode Mode 2 codetext using the MaxiCodeCodetextMode2 helper, then generating and decoding the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and BarCodeReader to create and read MaxiCode symbols, a common requirement for shipping and logistics applications where detailed routing information is encoded. Developers often need to construct complex codetext structures, render them to images, and verify correctness via decoding.
// Prompt: Use the MaxiCodeCodetextMode2 helper to build complex primary data and generate the barcode image.
// Tags: maxicode, mode2, complex barcode, generation, decoding, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a MaxiCode (Mode 2) barcode with complex primary data,
/// saving it as an image, and decoding it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the codetext, generates the barcode,
    /// saves it to a PNG file, and then reads the file to verify the encoded data.
    /// </summary>
    static void Main()
    {
        // Build MaxiCode codetext for Mode 2 with a standard second message
        var maxiCodeCodetext = new MaxiCodeCodetextMode2();
        maxiCodeCodetext.PostalCode = "524032140";   // 9‑digit US postal code
        maxiCodeCodetext.CountryCode = 56;          // Numeric country code
        maxiCodeCodetext.ServiceCategory = 999;     // Example service category

        // Create and assign the optional standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage();
        secondMessage.Message = "Test message";
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate and save the MaxiCode image to a PNG file
        string outputPath = "maxicode.png";
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath);
        }

        // Verify that the image was created and read it back for validation
        if (File.Exists(outputPath))
        {
            // Initialize a reader for MaxiCode symbols
            using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
            {
                // Iterate through all detected barcodes (should be one)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Decode the complex codetext from the raw CodeText
                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.MaxiCodeMode,
                        result.CodeText);

                    // Cast to the specific Mode 2 type to access its properties
                    if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                    {
                        Console.WriteLine($"PostalCode: {decodedMode2.PostalCode}");
                        Console.WriteLine($"CountryCode: {decodedMode2.CountryCode}");
                        Console.WriteLine($"ServiceCategory: {decodedMode2.ServiceCategory}");

                        // Output the optional second message if present
                        if (decodedMode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                        {
                            Console.WriteLine($"Second Message: {stdMsg.Message}");
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate the MaxiCode image.");
        }
    }
}
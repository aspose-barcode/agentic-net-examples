// Title: Encode and Verify MaxiCode Mode 2 Postal Code
// Description: Demonstrates encoding a numeric postal code into the primary message of a MaxiCode Mode 2 barcode and validates the result by decoding the generated image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and BarCodeReader to create and read MaxiCode symbols. Developers working with high‑density 2‑D barcodes such as MaxiCode can use these APIs to embed structured data (e.g., postal codes) and verify encoding accuracy, a common requirement in logistics and shipping applications.
// Prompt: Encode a numeric postal code in the primary message of a MaxiCode Mode 2 and verify decoding accuracy.
// Tags: maxicode, mode2, barcode, encoding, decoding, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode containing a numeric postal code,
/// saves it as a PNG image, and then reads the image back to verify that the encoded data
/// matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates decoding.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode_mode2.png";

        // Build the MaxiCode Mode 2 codetext with required fields.
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "123456789",   // 9‑digit numeric postal code (primary message)
            CountryCode = 840,          // Numeric ISO country code for USA
            ServiceCategory = 999       // Example service category identifier
        };

        // Optional: add a standard secondary message to the MaxiCode.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample secondary data"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the MaxiCode image using the ComplexBarcodeGenerator.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            complexGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read and decode the generated barcode image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Decode the raw codetext according to the detected MaxiCode mode.
                var decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                    result.Extended.MaxiCode.MaxiCodeMode,
                    result.CodeText);

                // Check if the decoded data is of type MaxiCode Mode 2.
                if (decodedCodetext is MaxiCodeCodetextMode2 decodedMode2)
                {
                    Console.WriteLine($"Decoded PostalCode: {decodedMode2.PostalCode}");
                    Console.WriteLine($"Original PostalCode: {maxiCodeCodetext.PostalCode}");
                    bool match = decodedMode2.PostalCode == maxiCodeCodetext.PostalCode;
                    Console.WriteLine($"Match: {match}");
                }
                else
                {
                    Console.WriteLine("Decoded codetext is not MaxiCode Mode 2.");
                }
            }
        }
    }
}
// Title: Set ECIEncoding to UTF-8 for MaxiCode and verify embedded character set
// Description: Demonstrates how to configure a MaxiCode barcode to use UTF‑8 ECI encoding and validates that the correct character set identifier is embedded by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on ECI (Extended Channel Interpretation) handling for Unicode data. It showcases the BarcodeGenerator and BarCodeReader classes, typical for scenarios where international characters must be encoded in 2D barcodes such as MaxiCode. Developers often need to set ECIEncoding to ensure proper decoding across platforms.
// Prompt: Set ECIEncoding to UTF‑8 for MaxiCode and verify the correct character set identifier is embedded.
// Tags: maxicode, eci, utf-8, barcode generation, barcode recognition, aspnet.barcode, encoding

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a MaxiCode barcode with UTF‑8 ECI encoding, saves it, and verifies the encoding by reading the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode_utf8.png";

        // Sample text containing Unicode characters to be encoded in the barcode.
        string sampleText = "犬Right狗";

        // Create a MaxiCode barcode generator with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, sampleText))
        {
            // Set the ECI (Extended Channel Interpretation) encoding to UTF‑8.
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a barcode reader for MaxiCode to read the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
        {
            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Ensure at least one barcode was detected.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode result.
            foreach (var result in results)
            {
                // Output the decoded text and verify it matches the original sample text.
                // This confirms that the UTF‑8 ECI identifier was correctly embedded.
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Match Original: {result.CodeText == sampleText}");
            }
        }
    }
}
// Title: Decode MaxiCode barcode with retry mechanism
// Description: Demonstrates generating a MaxiCode barcode in memory and attempting to decode it up to three times, handling failures gracefully.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of ComplexBarcodeGenerator, BarCodeReader, and related classes for MaxiCode symbology. Developers often need to generate complex barcodes, read them from streams, and implement retry logic for unreliable scans. The snippet illustrates typical workflow and error handling for MaxiCode decoding.
// Prompt: Implement a retry mechanism that attempts to decode a MaxiCode barcode up to three times on failure.
// Tags: maxicode, barcode generation, barcode recognition, retry, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and decoding it with retry logic.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode in memory, then attempts to decode it up to three times.
    /// </summary>
    static void Main()
    {
        // Create a sample MaxiCode barcode (Mode 2) in memory
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Add a second message to the barcode
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        maxiCode.SecondMessage = secondMessage;

        // Generate the barcode image and store it in a memory stream
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        using (var imageStream = new MemoryStream())
        {
            // Save the barcode image to the memory stream in PNG format
            generator.Save(imageStream, BarCodeImageFormat.Png);

            // Reset stream position to the beginning for reading
            imageStream.Position = 0;

            const int maxAttempts = 3; // Maximum number of decode attempts
            bool decoded = false;      // Flag indicating successful decode

            // Retry loop: attempt to read and decode the barcode up to maxAttempts times
            for (int attempt = 1; attempt <= maxAttempts && !decoded; attempt++)
            {
                // Ensure the stream is positioned at the start before each read
                imageStream.Position = 0;

                // Initialize the barcode reader for MaxiCode symbology
                using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
                {
                    // Use high-quality settings to improve detection reliability
                    reader.QualitySettings = QualitySettings.MaxQuality;

                    // Read all barcodes found in the stream
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                        continue; // Proceed to next attempt
                    }

                    // Process each detected barcode
                    foreach (var result in results)
                    {
                        // Attempt to decode the MaxiCode codetext using the structured reader
                        var decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                            result.Extended.MaxiCode.MaxiCodeMode,
                            result.CodeText);

                        if (decodedCodetext != null)
                        {
                            // Decoding succeeded – output the extracted information
                            Console.WriteLine($"Attempt {attempt}: Decoding succeeded.");
                            var structured = (MaxiCodeStructuredCodetext)decodedCodetext;
                            Console.WriteLine($"Postal Code: {structured.PostalCode}");
                            Console.WriteLine($"Country Code: {structured.CountryCode}");
                            Console.WriteLine($"Service Category: {structured.ServiceCategory}");

                            // If the barcode is Mode 2, display the second message
                            if (decodedCodetext is MaxiCodeCodetextMode2 mode2 &&
                                mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                            {
                                Console.WriteLine($"Message: {stdMsg.Message}");
                            }

                            decoded = true; // Mark as successfully decoded
                            break;          // Exit the foreach loop
                        }
                        else
                        {
                            Console.WriteLine($"Attempt {attempt}: Decoding failed for detected barcode.");
                        }
                    }

                    // If not decoded and more attempts remain, indicate a retry
                    if (!decoded && attempt < maxAttempts)
                    {
                        Console.WriteLine($"Retrying... (attempt {attempt + 1} of {maxAttempts})");
                    }
                }
            }

            // Final outcome after all attempts
            if (!decoded)
            {
                Console.WriteLine("Failed to decode the MaxiCode barcode after maximum attempts.");
            }
        }
    }
}
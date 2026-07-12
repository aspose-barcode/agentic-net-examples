// Title: Decode Dutch KIX barcode from byte array
// Description: Demonstrates generating a Dutch KIX barcode, storing it in a byte array, and decoding it while handling format exceptions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a Dutch KIX symbology image and BarCodeReader to decode it from a memory stream. Developers working with postal code barcodes often need to generate barcodes programmatically and later validate or extract the encoded data, making this pattern common for batch processing and automated verification scenarios.
// Prompt: Decode a Dutch KIX barcode from a byte array and handle potential format exceptions.
// Tags: dutchkix, barcode, decode, png, barcodereader, barcodegenerator, exception-handling

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Dutch KIX barcode, stores it in a byte array,
/// and then decodes it while handling possible format exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, stores, and decodes a Dutch KIX barcode.
    /// </summary>
    static void Main()
    {
        // Sample data to encode – Dutch KIX requires numeric postal code format.
        string sampleCode = "12345678";

        // Generate a Dutch KIX barcode image and store it in a memory stream.
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, sampleCode))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                barcodeBytes = ms.ToArray(); // Convert the stream to a byte array.
            }
        }

        // Decode the barcode from the byte array.
        try
        {
            using (var imageStream = new MemoryStream(barcodeBytes))
            {
                // Initialize the reader for Dutch KIX symbology.
                using (var reader = new BarCodeReader(imageStream, DecodeType.DutchKIX))
                {
                    // Optionally set a quality preset (default is NormalQuality).
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Perform the recognition.
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No Dutch KIX barcode detected.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            // result.CodeText will be null if decoding failed; check for that.
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                Console.WriteLine($"Decoded Dutch KIX CodeText: {result.CodeText}");
                            }
                            else
                            {
                                Console.WriteLine("Barcode detected but CodeText could not be read.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle possible format or processing exceptions.
            Console.WriteLine($"Error during barcode decoding: {ex.Message}");
        }
    }
}
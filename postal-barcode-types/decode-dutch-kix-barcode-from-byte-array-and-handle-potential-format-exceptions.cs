// Title: Decode Dutch KIX barcode from byte array
// Description: Demonstrates generating a Dutch KIX barcode, converting it to a PNG byte array, and decoding it back while handling format errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image, retrieve the image as a byte array, and then use BarCodeReader with DecodeType to read the barcode. Typical use cases include processing scanned images stored in memory, validating barcode data, and handling exceptions when the format is invalid. Developers working with barcode automation often need these APIs to integrate barcode handling into backend services.
// Prompt: Decode a Dutch KIX barcode from a byte array and handle potential format exceptions.
// Tags: dutch kix, barcode generation, barcode decoding, byte array, exception handling, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Dutch KIX barcode, extracts its image as a byte array,
/// and then decodes the barcode from that byte array while handling possible format exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, conversion to byte array,
    /// and subsequent decoding with error handling.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a Dutch KIX barcode and obtain its image as a byte array
        // ------------------------------------------------------------
        byte[] barcodeBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, "123456ASPOSE"))
        {
            // Set the X-dimension (module width) to 4 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the memory stream to a byte array for later decoding
                barcodeBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // Decode the barcode from the byte array
        // ------------------------------------------------------------
        try
        {
            // Load the byte array into a memory stream for the reader
            using (var ms = new MemoryStream(barcodeBytes))
            {
                // Initialize the barcode reader for Dutch KIX symbology
                using (var reader = new BarCodeReader(ms, DecodeType.DutchKIX))
                {
                    // Iterate through all detected barcodes (should be one in this case)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine($"Code Type: {result.CodeType}");
                        Console.WriteLine($"Code Type Name: {result.CodeTypeName}");
                    }
                }
            }
        }
        // Handle format-related errors (e.g., invalid barcode data)
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Format error: {ex.Message}");
        }
        // Handle any other unexpected errors
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
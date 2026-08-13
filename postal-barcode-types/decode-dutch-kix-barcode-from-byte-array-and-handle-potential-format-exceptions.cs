// Title: Decode Dutch KIX barcode from byte array with exception handling
// Description: Demonstrates generating a Dutch KIX barcode, converting it to a PNG byte array, and decoding it while handling format exceptions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and handling BarCodeException. Developers often need to generate barcodes in memory, transmit them as byte arrays, and reliably decode them in various applications such as inventory systems or document processing.
// Prompt: Decode a Dutch KIX barcode from a byte array and handle potential format exceptions.
// Tags: barcode, dutch kix, decode, byte array, exception handling, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of generating and decoding a Dutch KIX barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Dutch KIX barcode, obtains its PNG byte array,
    /// and decodes the barcode while handling possible exceptions.
    /// </summary>
    static void Main()
    {
        // Sample code text for Dutch KIX barcode (example value)
        const string sampleCodeText = "123456789";

        // Generate a Dutch KIX barcode image and obtain its byte array
        byte[] barcodeBytes = GenerateDutchKixBarcode(sampleCodeText);

        // Decode the barcode from the byte array
        DecodeDutchKixFromBytes(barcodeBytes);
    }

    /// <summary>
    /// Generates a Dutch KIX barcode image and returns the image bytes in PNG format.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    static byte[] GenerateDutchKixBarcode(string codeText)
    {
        // Use a memory stream to hold the generated image
        using (var ms = new MemoryStream())
        {
            // Initialize the generator with Dutch KIX symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the image bytes from the memory stream
            return ms.ToArray();
        }
    }

    /// <summary>
    /// Decodes a Dutch KIX barcode from a byte array and prints the result.
    /// Handles both barcode-specific and general exceptions.
    /// </summary>
    /// <param name="imageBytes">Byte array containing the barcode image.</param>
    static void DecodeDutchKixFromBytes(byte[] imageBytes)
    {
        // Validate input
        if (imageBytes == null || imageBytes.Length == 0)
        {
            Console.WriteLine("No image data provided.");
            return;
        }

        // Create a memory stream from the byte array for decoding
        using (var ms = new MemoryStream(imageBytes))
        {
            try
            {
                // Initialize the reader for Dutch KIX decode type
                using (var reader = new BarCodeReader(ms, DecodeType.DutchKIX))
                {
                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                    }
                }
            }
            catch (BarCodeException ex)
            {
                // Handle format or recognition errors specific to Aspose.BarCode
                Console.WriteLine($"BarCodeException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
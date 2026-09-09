// Title: Decode OneCode barcode from a memory stream and handle unsupported recognition
// Description: This example generates a OneCode barcode, stores it in a memory stream, and attempts to decode it, illustrating how to manage cases where the symbology is not supported and how to log any decoding errors.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to create a barcode image, saves it to a stream, and employs BarCodeReader for decoding. Developers working with barcode creation, image handling, and error reporting commonly use these APIs to integrate barcode functionality into .NET applications.
// Prompt: Decode a OneCode barcode from a network response stream and log any decoding errors.
// Tags: onecode, barcode, generation, recognition, decoding, error-logging, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a OneCode barcode, saving it to a memory stream,
/// and attempting to decode it while handling unsupported recognition and logging errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, prepares the stream,
    /// decodes the barcode, and outputs results or errors to the console.
    /// </summary>
    static void Main()
    {
        // Generate a OneCode barcode image into a memory stream
        MemoryStream barcodeStream;
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, "1234567890"))
            {
                // Set the X-dimension (module width) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Initialize the memory stream that will hold the barcode image
                barcodeStream = new MemoryStream();

                // Save the generated barcode as PNG into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during barcode generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
            return;
        }

        // Reset the stream position to the beginning for reading
        barcodeStream.Position = 0;

        // Decode the OneCode barcode from the stream
        try
        {
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.OneCode))
            {
                // Attempt to read barcodes from the stream
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    // OneCode recognition is currently unsupported; zero results are expected.
                    Console.WriteLine("No barcode detected (expected — OneCode recognition unsupported).");
                }
                else
                {
                    // Output details for each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"CodeType: {result.CodeType}");
                        Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                        Console.WriteLine($"Confidence: {result.Confidence}");
                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during decoding
            Console.WriteLine($"Decoding error: {ex.Message}");
        }
        finally
        {
            // Ensure the memory stream is properly disposed
            barcodeStream.Dispose();
        }
    }
}
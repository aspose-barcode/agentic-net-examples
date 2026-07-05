// Title: Barcode Generation and Recognition with Timeout Handling
// Description: Demonstrates generating a Code128 barcode, reading it from a memory stream, and handling a RecognitionAbortedException when the read operation times out.
// Prompt: Catch RecognitionAbortedException to handle cases where barcode detection is interrupted by timeout or abort.
// Tags: barcode, code128, generation, recognition, timeout, exception handling, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a barcode, attempts to read it, and gracefully handles a timeout abort.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it, and catches RecognitionAbortedException.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        const string codeText = "1234567890";

        // Use a memory stream to hold the generated barcode image.
        using (var generationStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(generationStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            generationStream.Position = 0;

            // Initialize a barcode reader for the generated image, specifying the expected symbology.
            using (var reader = new BarCodeReader(generationStream, DecodeType.Code128))
            {
                // Set an extremely low timeout (1 ms) to force a RecognitionAbortedException.
                reader.Timeout = 1; // milliseconds

                try
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Detected Text: {result.CodeText}");
                    }
                }
                catch (RecognitionAbortedException ex)
                {
                    // Handle the case where recognition was aborted due to timeout or manual abort.
                    Console.WriteLine($"Recognition aborted: {ex.Message}");
                }
            }
        }
    }
}
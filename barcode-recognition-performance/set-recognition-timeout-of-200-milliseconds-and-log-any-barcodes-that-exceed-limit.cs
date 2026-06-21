using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode image and attempts to read it.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output path for the generated barcode image.
        const string imagePath = "sample.png";

        // -------------------------------------------------
        // Generate a sample barcode image (Code128, data "1234567890").
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode to the specified file.
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Create a barcode reader, configure a timeout, and read the image.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set the maximum time (in milliseconds) the reader will spend on recognition.
            reader.Timeout = 200; // milliseconds

            try
            {
                // Attempt to read all barcodes from the image.
                var results = reader.ReadBarCodes();

                // Iterate through each detected barcode and output its type and text.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Log a message when recognition exceeds the configured timeout.
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms (limit: 200 ms)");
            }
        }
    }
}
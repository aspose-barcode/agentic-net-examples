// Title: Barcode generation and recognition from a TIFF memory stream
// Description: Demonstrates creating a Code128 barcode, storing it as a TIFF in a memory stream, then reading and outputting all detected barcode values.
// Prompt: Pass a memory stream containing TIFF data to BarCodeReader and extract all detected barcode values.
// Tags: barcode, tiff, memorystream, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as a TIFF image in a memory stream,
/// and then reads the barcode back using <see cref="BarCodeReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, writes it to a memory stream,
    /// and extracts all detected barcode values from the stream.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated TIFF image.
        using (var memoryStream = new MemoryStream())
        {
            // Generate a Code128 barcode with the text "Sample123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the barcode image into the memory stream in TIFF format.
                generator.Save(memoryStream, BarCodeImageFormat.Tiff);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Initialize a BarCodeReader to process the TIFF image from the memory stream.
            using (var reader = new BarCodeReader(memoryStream))
            {
                // Instruct the reader to detect all supported barcode types.
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                // Perform the barcode detection and retrieve the results.
                var results = reader.ReadBarCodes();

                // Iterate through each detected barcode and output its text value.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected barcode: {result.CodeText}");
                }
            }
        }
    }
}
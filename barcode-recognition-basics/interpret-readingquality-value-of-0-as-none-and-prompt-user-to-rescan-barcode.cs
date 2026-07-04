// Title: Barcode Reading Quality Evaluation
// Description: Demonstrates generating a Code128 barcode, reading it, and interpreting a ReadingQuality of 0 as none, prompting the user to rescan.
// Prompt: Interpret a ReadingQuality value of 0 as none and prompt the user to rescan the barcode.
// Tags: barcode symbology, generation, recognition, readingquality, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a barcode, reads it, and checks the reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it from memory,
    /// and reports the reading quality, prompting a rescan when quality is none.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode with the value "12345" and keep it in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Store the generated barcode image in a memory stream.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for reading.
                memoryStream.Position = 0;

                // Create a reader that can decode all supported barcode types from the stream.
                using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Retrieve the reading quality metric.
                        double quality = result.ReadingQuality;

                        // If quality is zero, treat it as "none" and ask for a rescan.
                        if (quality == 0)
                        {
                            Console.WriteLine("Reading quality is none. Please rescan the barcode.");
                        }
                        else
                        {
                            Console.WriteLine($"Reading quality: {quality}");
                        }
                    }
                }
            }
        }
    }
}
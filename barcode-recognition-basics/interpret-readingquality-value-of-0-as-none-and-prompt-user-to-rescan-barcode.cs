// Title: Demonstrate barcode generation, reading, and handling zero ReadingQuality
// Description: This example generates a Code128 barcode, reads it, and treats a ReadingQuality of 0 as no quality, prompting a rescan.
// Category-Description: Shows basic Aspose.BarCode operations such as barcode generation with BarcodeGenerator, image saving, and barcode recognition using BarCodeReader. Useful for developers needing to validate scan quality and handle low-quality reads in scanning applications. Covers common use cases like automated scanning, quality assessment, and error handling.
// Prompt: Interpret a ReadingQuality value of 0 as none and prompt the user to rescan the barcode.
// Tags: code128, barcode generation, barcode recognition, readingquality, quality assessment, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a Code128 barcode, reads it, and checks the reading quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it, and outputs quality information.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        const string sampleText = "12345";

        // Create a barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
        {
            // Store the generated barcode image in a memory stream.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                memoryStream.Position = 0;

                // Initialize a barcode reader for Code128 from the memory stream.
                using (var reader = new BarCodeReader(memoryStream, DecodeType.Code128))
                {
                    // Iterate through all detected barcodes.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Retrieve the reading quality metric.
                        double quality = result.ReadingQuality;

                        // If quality is zero, treat it as none and suggest a rescan.
                        if (quality == 0.0)
                        {
                            Console.WriteLine("Reading quality is none. Please rescan the barcode.");
                        }
                        else
                        {
                            // Otherwise, display the quality and decoded text.
                            Console.WriteLine($"Reading quality: {quality}");
                            Console.WriteLine($"Decoded text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}
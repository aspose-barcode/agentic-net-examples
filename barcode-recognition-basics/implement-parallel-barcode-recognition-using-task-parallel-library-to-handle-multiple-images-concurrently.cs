using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code128 barcodes, storing them in memory,
/// and recognizing them concurrently using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, processes them in parallel, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // Define a list of sample barcode texts to encode.
        List<string> codeTexts = new List<string>
        {
            "Sample1",
            "Sample2",
            "Sample3",
            "Sample4",
            "Sample5"
        };

        // Generate barcode images for each text and store them in memory streams.
        List<MemoryStream> imageStreams = new List<MemoryStream>();
        foreach (string text in codeTexts)
        {
            // Create a new memory stream for the current barcode image.
            MemoryStream ms = new MemoryStream();

            // Use BarcodeGenerator to create a Code128 barcode.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Save the generated barcode as a PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for subsequent reading.
            ms.Position = 0;
            imageStreams.Add(ms);
        }

        // Process each barcode image concurrently using the Task Parallel Library (TPL).
        List<Task> tasks = new List<Task>();
        foreach (MemoryStream stream in imageStreams)
        {
            // Capture the current stream in a local variable for the task closure.
            Task task = Task.Run(() =>
            {
                // Create a BarCodeReader to decode all supported barcode types from the stream.
                using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes and output their type and text.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }
                }

                // Dispose the memory stream after processing to free resources.
                stream.Dispose();
            });

            tasks.Add(task);
        }

        // Wait for all recognition tasks to complete before proceeding.
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine("Parallel barcode recognition completed.");
    }
}
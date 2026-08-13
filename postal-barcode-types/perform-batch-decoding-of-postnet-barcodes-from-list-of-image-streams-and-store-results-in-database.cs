// Title: Batch decode Postnet barcodes from memory streams
// Description: Demonstrates generating Postnet barcode images, decoding them in a batch, and persisting the results to a JSON file (as a stand‑in for a database).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Postnet symbology, BarCodeReader for batch decoding, and common .NET I/O classes for handling image streams. Developers working with bulk barcode processing, automated scanning, or data import pipelines often need to generate, read, and store barcode information efficiently.
// Prompt: Perform batch decoding of Postnet barcodes from a list of image streams and store results in a database.
// Tags: postnet, barcode, decoding, batch, json, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace PostnetBatchDecode
{
    /// <summary>
    /// Simple DTO to hold decoding results for each processed image.
    /// </summary>
    public class DecodeRecord
    {
        public int Index { get; set; }
        public string CodeText { get; set; }
        public string CodeTypeName { get; set; }
    }

    /// <summary>
    /// Demonstrates batch generation and decoding of Postnet barcodes, then stores the results.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates sample Postnet barcodes, decodes them, and writes results to a JSON file.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Define a set of sample Postnet code texts.
            var sampleCodes = new List<string> { "12345", "67890", "24680", "13579", "11223" };

            // Generate barcode images and keep them as memory streams.
            var imageStreams = new List<MemoryStream>();
            foreach (var code in sampleCodes)
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
                {
                    var ms = new MemoryStream();
                    // Save the barcode image to the memory stream in PNG format.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset for reading.
                    imageStreams.Add(ms);
                }
            }

            // Prepare a list to collect decoding results.
            var results = new List<DecodeRecord>();

            // Decode each image stream using the Postnet decode type.
            BaseDecodeType postnetDecode = DecodeType.Postnet;
            int index = 0;
            foreach (var stream in imageStreams)
            {
                // Ensure the stream is positioned at the beginning.
                stream.Position = 0;
                using (var reader = new BarCodeReader(stream, postnetDecode))
                {
                    var barCodes = reader.ReadBarCodes();
                    foreach (var result in barCodes)
                    {
                        results.Add(new DecodeRecord
                        {
                            Index = index,
                            CodeText = result.CodeText,
                            CodeTypeName = result.CodeTypeName
                        });
                        Console.WriteLine($"Image {index}: Type={result.CodeTypeName}, Text={result.CodeText}");
                    }
                }
                index++;
            }

            // Store results in a JSON file (simulating a database).
            string jsonPath = "postnet_results.json";
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(results, jsonOptions);
            File.WriteAllText(jsonPath, json);
            Console.WriteLine($"Decoding results saved to {jsonPath}");

            // Cleanup memory streams.
            foreach (var ms in imageStreams)
            {
                ms.Dispose();
            }

            // Note: In a real scenario, you would insert 'results' into a database
            // using an appropriate data access library (e.g., SQLite, SQL Server, etc.).
        }
    }
}
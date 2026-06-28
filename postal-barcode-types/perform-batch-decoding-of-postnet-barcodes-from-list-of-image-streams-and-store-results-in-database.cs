using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace PostnetBatchDecoding
{
    // Simple DTO to hold decoding results
    public class DecodedResult
    {
        public string CodeText { get; set; }
        public string CodeTypeName { get; set; }
    }

    /// <summary>
    /// Demonstrates generating Postnet barcodes, decoding them, and saving results as JSON.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Generates barcode images from sample ZIP codes, decodes them, and writes results to a JSON file.
        /// </summary>
        static void Main()
        {
            // Sample Postnet code texts (ZIP codes) to be encoded into barcodes
            string[] sampleCodes = new string[]
            {
                "12345",
                "67890",
                "24680",
                "13579",
                "11223"
            };

            // Generate barcode images and keep them as memory streams for later decoding
            var imageStreams = new List<MemoryStream>();
            foreach (var code in sampleCodes)
            {
                // Create a barcode generator for the current code using Postnet symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
                {
                    // Save the generated barcode to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position to the beginning

                        // Store a copy of the stream for later decoding (avoid disposing the original)
                        var copy = new MemoryStream(ms.ToArray());
                        imageStreams.Add(copy);
                    }
                }
            }

            // Decode each image stream using Postnet symbology
            var decodedResults = new List<DecodedResult>();
            foreach (var stream in imageStreams)
            {
                // Initialize a barcode reader for the current stream
                using (var reader = new BarCodeReader(stream, DecodeType.Postnet))
                {
                    // Read all barcodes found in the stream
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Add the decoded information to the results list
                        decodedResults.Add(new DecodedResult
                        {
                            CodeText = result.CodeText,
                            CodeTypeName = result.CodeTypeName
                        });
                    }
                }
            }

            // Serialize the decoding results to JSON and write to a file (simulating a database)
            string outputPath = "decoded_results.json";
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(decodedResults, jsonOptions);
            File.WriteAllText(outputPath, json);

            // Inform the user about the operation outcome
            Console.WriteLine($"Decoded {decodedResults.Count} Postnet barcodes. Results saved to '{outputPath}'.");
        }
    }
}
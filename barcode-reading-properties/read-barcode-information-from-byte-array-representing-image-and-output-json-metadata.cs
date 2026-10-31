// Title: Read barcode from image byte array and output JSON metadata
// Description: Demonstrates generating a QR barcode, reading it from an in‑memory byte array, and serializing the detected barcode information to JSON.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image, BarCodeReader to decode barcodes from a stream, and System.Text.Json to produce structured JSON output. Developers working with barcode imaging often need to process images without persisting them to disk, extract metadata such as code text, type, and region, and integrate the results into web services or data pipelines.
// Prompt: Read barcode information from a byte array representing an image and output JSON metadata.
// Tags: barcode, qr, read, bytearray, json, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR barcode, reads it from a byte array,
/// and prints the extracted barcode metadata as formatted JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a QR barcode and capture its image in a byte array.
        // ------------------------------------------------------------
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the X-dimension (module size) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Convert the stream to a byte array.
            }
        }

        // ------------------------------------------------------------
        // 2. Decode the barcode directly from the byte array.
        // ------------------------------------------------------------
        using (var readStream = new MemoryStream(imageBytes))
        {
            // Configure the reader to attempt decoding all supported barcode types.
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

            using (var reader = new BarCodeReader(readStream, decodeType))
            {
                // Read all barcodes found in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Prepare a collection to hold metadata for each detected barcode.
                var metadataList = new List<object>();

                foreach (var result in results)
                {
                    // Extract the bounding rectangle of the barcode region.
                    var rect = result.Region.Rectangle;

                    // Capture the corner points of the region for detailed geometry.
                    var points = new List<object>();
                    foreach (var pt in result.Region.Points)
                    {
                        points.Add(new { X = pt.X, Y = pt.Y });
                    }

                    // Build an anonymous object containing the barcode data and region info.
                    var meta = new
                    {
                        CodeText = result.CodeText,
                        CodeTypeName = result.CodeTypeName,
                        Region = new
                        {
                            X = rect.X,
                            Y = rect.Y,
                            Width = rect.Width,
                            Height = rect.Height,
                            Angle = result.Region.Angle,
                            Points = points
                        }
                    };

                    metadataList.Add(meta);
                }

                // ------------------------------------------------------------
                // 3. Serialize the metadata collection to indented JSON and output.
                // ------------------------------------------------------------
                string json = JsonSerializer.Serialize(
                    metadataList,
                    new JsonSerializerOptions { WriteIndented = true });

                Console.WriteLine(json);
            }
        }
    }
}
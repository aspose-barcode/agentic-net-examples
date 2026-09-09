// Title: Read barcode from image byte array and output JSON metadata
// Description: This example generates a QR barcode, converts it to a byte array, reads the barcode from the image, and serializes the detection metadata to JSON.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create a barcode image, work with image data in memory, use BarCodeReader to detect all supported symbologies, and extract region, angle, and point information. Developers often need these steps for automated scanning, image processing pipelines, or integrating barcode data into web services.
// Prompt: Read barcode information from a byte array representing an image and output JSON metadata.
// Tags: barcode, qr, read, json, aspose.barcode, generation, recognition, bytearray

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates reading barcode information from an in‑memory image and outputting JSON metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads it from a byte array, and prints JSON metadata to the console.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and obtain its image as a byte array.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the X dimension (module size) of the QR code.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Create a bitmap from the byte array for recognition.
                using (var bitmap = new Bitmap(new MemoryStream(imageBytes)))
                {
                    // Initialize a reader that can decode all supported barcode types.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes found in the image.
                        BarCodeResult[] results = reader.ReadBarCodes();
                        var metadataList = new List<object>();

                        // Process each detection result.
                        foreach (var result in results)
                        {
                            var rect = result.Region.Rectangle;
                            var points = result.Region.Points
                                .Select(p => new { X = p.X, Y = p.Y })
                                .ToArray();

                            // Build an anonymous object containing relevant metadata.
                            var metadata = new
                            {
                                CodeText = result.CodeText,
                                CodeTypeName = result.CodeTypeName,
                                Region = new
                                {
                                    X = rect.X,
                                    Y = rect.Y,
                                    Width = rect.Width,
                                    Height = rect.Height
                                },
                                Angle = result.Region.Angle,
                                Points = points
                            };

                            metadataList.Add(metadata);
                        }

                        // Serialize the metadata collection to formatted JSON.
                        string json = JsonSerializer.Serialize(metadataList, new JsonSerializerOptions { WriteIndented = true });
                        Console.WriteLine(json);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, reading it, and outputting the results as JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates a barcode image in memory, reads it, and prints detection results in JSON format.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET Core integration cannot be demonstrated in this console snippet.
        // The core barcode reading logic is shown below, and the results are printed as JSON.

        // Generate a sample barcode image in memory.
        using (var imageStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                imageStream.Position = 0;
            }

            // Load the image from the memory stream into a Bitmap (Aspose.Drawing).
            using (var bitmap = new Bitmap(imageStream))
            {
                // Initialize a barcode reader that detects all supported barcode types.
                using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                {
                    // Use the highest quality preset to improve detection accuracy.
                    reader.QualitySettings = QualitySettings.MaxQuality;

                    // Read all barcodes present in the image.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Prepare a list of simple objects for JSON serialization.
                    var output = new List<object>();
                    foreach (var result in results)
                    {
                        // Create an anonymous object containing relevant barcode information.
                        var info = new
                        {
                            CodeText = result.CodeText,
                            Confidence = result.Confidence.ToString(),
                            ReadingQuality = result.ReadingQuality,
                            Angle = result.Region.Angle
                        };
                        output.Add(info);
                    }

                    // Serialize the results to formatted JSON and write to the console.
                    string json = JsonSerializer.Serialize(
                        output,
                        new JsonSerializerOptions { WriteIndented = true });

                    Console.WriteLine(json);
                }
            }
        }
    }
}
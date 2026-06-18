using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, reading its metadata, and outputting the information as JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO for JSON output representing barcode metadata.
    /// </summary>
    private class BarcodeInfo
    {
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public string Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public double Angle { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads its properties, and prints them as formatted JSON.
    /// </summary>
    static void Main()
    {
        // Step 1: Generate a sample barcode and obtain its image bytes.
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the stream contents to a byte array.
                imageBytes = ms.ToArray();
            }
        }

        // Step 2: Read barcode information from the byte array.
        var barcodeInfos = new List<BarcodeInfo>();
        using (var imageStream = new MemoryStream(imageBytes))
        {
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Iterate over each detected barcode in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract the bounding rectangle of the barcode region.
                    var region = result.Region.Rectangle;

                    // Populate a DTO with the relevant metadata.
                    var info = new BarcodeInfo
                    {
                        CodeTypeName = result.CodeTypeName,
                        CodeText = result.CodeText,
                        Confidence = result.Confidence.ToString(),
                        ReadingQuality = result.ReadingQuality,
                        Angle = result.Region.Angle,
                        X = region.X,
                        Y = region.Y,
                        Width = region.Width,
                        Height = region.Height
                    };

                    // Add the DTO to the collection.
                    barcodeInfos.Add(info);
                }
            }
        }

        // Step 3: Serialize the collected metadata to formatted JSON and output it.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(barcodeInfos, jsonOptions);
        Console.WriteLine(json);
    }
}
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image and store it in a byte array
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Read barcode information from the byte array
                using (var imageStream = new MemoryStream(imageBytes))
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    var metadata = new List<object>();
                    foreach (BarCodeResult result in results)
                    {
                        var region = result.Region;
                        var rect = region.Rectangle;
                        var item = new
                        {
                            CodeText = result.CodeText,
                            CodeType = result.CodeTypeName,
                            Confidence = result.Confidence.ToString(),
                            ReadingQuality = result.ReadingQuality,
                            Angle = region.Angle,
                            Bounds = new
                            {
                                X = rect.X,
                                Y = rect.Y,
                                Width = rect.Width,
                                Height = rect.Height
                            }
                        };
                        metadata.Add(item);
                    }

                    string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(json);
                }
            }
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample barcode image in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate bitmap
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Read the barcode from the generated bitmap
                using (var reader = new BarCodeReader())
                {
                    // Set the image for recognition
                    reader.SetBarCodeImage(barcodeImage);
                    // Detect all supported barcode types
                    reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                    // Collect results
                    var results = new List<object>();
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        var rect = result.Region.Rectangle;
                        var info = new
                        {
                            Type = result.CodeTypeName,
                            Text = result.CodeText,
                            Angle = result.Region.Angle,
                            Bounds = new
                            {
                                X = rect.X,
                                Y = rect.Y,
                                Width = rect.Width,
                                Height = rect.Height
                            }
                        };
                        results.Add(info);
                    }

                    // Serialize to JSON
                    string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText("barcodeInfo.json", json);
                }
            }
        }
    }
}
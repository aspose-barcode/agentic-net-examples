using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Export generator settings to XML (in memory)
            using (var xmlStream = new MemoryStream())
            {
                if (!generator.ExportToXml(xmlStream))
                {
                    Console.WriteLine("Failed to export generator settings to XML.");
                    return;
                }
                xmlStream.Position = 0;

                // Generate barcode image (PNG) in memory
                using (var imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0;

                    // Import BarCodeReader from the exported XML
                    using (var reader = BarCodeReader.ImportFromXml(xmlStream))
                    {
                        // Assign the generated image to the reader
                        reader.SetBarCodeImage(imageStream);

                        // Read barcodes from the image
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // Convert results to a JSON-friendly structure
                        var jsonItems = new List<object>();
                        foreach (var result in results)
                        {
                            var rect = result.Region.Rectangle;
                            jsonItems.Add(new
                            {
                                result.CodeText,
                                result.CodeTypeName,
                                Confidence = result.Confidence.ToString(),
                                Region = new
                                {
                                    rect.X,
                                    rect.Y,
                                    rect.Width,
                                    rect.Height,
                                    result.Region.Angle
                                }
                            });
                        }

                        // Serialize to JSON and output
                        string json = JsonSerializer.Serialize(jsonItems, new JsonSerializerOptions { WriteIndented = true });
                        Console.WriteLine(json);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeJsonExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator and set basic properties
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                // Export generator settings to XML (in memory)
                using (var xmlStream = new MemoryStream())
                {
                    generator.ExportToXml(xmlStream);
                    xmlStream.Position = 0;

                    // Import a new generator instance from the XML stream
                    using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                    {
                        // Generate barcode image as PNG into a memory stream
                        using (var imageStream = new MemoryStream())
                        {
                            importedGenerator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0;

                            // Read the barcode from the image stream
                            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                            {
                                // Perform recognition
                                BarCodeResult[] results = reader.ReadBarCodes();

                                // Convert results to JSON
                                string json = ConvertResultsToJson(results);
                                Console.WriteLine(json);
                            }
                        }
                    }
                }
            }
        }

        // Converts an array of BarCodeResult objects into a JSON string
        private static string ConvertResultsToJson(BarCodeResult[] results)
        {
            var list = new List<object>();

            foreach (var result in results)
            {
                var region = result.Region.Rectangle;
                var item = new
                {
                    CodeTypeName = result.CodeTypeName,
                    CodeText = result.CodeText,
                    // Include 1D specific extended data if available
                    Value = result.Extended?.OneD?.Value,
                    CheckSum = result.Extended?.OneD?.CheckSum,
                    Region = new
                    {
                        X = region.X,
                        Y = region.Y,
                        Width = region.Width,
                        Height = region.Height,
                        Angle = result.Region.Angle
                    },
                    Confidence = result.Confidence,
                    ReadingQuality = result.ReadingQuality
                };
                list.Add(item);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(list, options);
        }
    }
}
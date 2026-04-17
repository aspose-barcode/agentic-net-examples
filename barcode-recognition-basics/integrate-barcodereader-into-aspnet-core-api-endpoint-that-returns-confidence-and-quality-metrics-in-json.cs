using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

namespace AsposeBarcodeDemo
{
    // DTO for JSON response
    public class BarcodeInfo
    {
        public string TypeName { get; set; }
        public string CodeText { get; set; }
        public string Confidence { get; set; }
        public double ReadingQuality { get; set; }
    }

    class Program
    {
        // Simulated ASP.NET Core API endpoint
        static string GetBarcodeMetrics()
        {
            // Generate a sample barcode image in memory
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    memoryStream.Position = 0;

                    // Read the barcode and collect confidence/quality data
                    using (var reader = new BarCodeReader(memoryStream, DecodeType.AllSupportedTypes))
                    {
                        var results = new List<BarcodeInfo>();
                        foreach (var result in reader.ReadBarCodes())
                        {
                            results.Add(new BarcodeInfo
                            {
                                TypeName = result.CodeTypeName,
                                CodeText = result.CodeText,
                                Confidence = result.Confidence.ToString(),
                                ReadingQuality = result.ReadingQuality
                            });
                        }

                        // Serialize the result list to JSON
                        return JsonSerializer.Serialize(results);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // Simulate a request to the API endpoint
            string jsonResponse = GetBarcodeMetrics();
            Console.WriteLine(jsonResponse);
        }
    }
}
using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a barcode image in memory
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Prepare the stream for reading
            imageStream.Position = 0;

            // Read the barcode and obtain confidence and quality metrics
            using (var reader = new BarCodeReader(imageStream, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                var output = new System.Collections.Generic.List<object>();

                foreach (var result in results)
                {
                    output.Add(new
                    {
                        CodeType = result.CodeTypeName,
                        CodeText = result.CodeText,
                        Confidence = result.Confidence.ToString(),
                        ReadingQuality = result.ReadingQuality
                    });
                }

                // Serialize the results to JSON and write to console
                string json = JsonSerializer.Serialize(output, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(json);
            }
        }
    }
}
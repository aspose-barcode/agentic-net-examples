using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static async Task Main(string[] args)
    {
        // Sample payloads simulating incoming web requests
        var requests = new List<(string CodeText, BaseEncodeType Type)>
        {
            ("1234567890", EncodeTypes.Code128),
            ("9876543210", EncodeTypes.EAN13),
            ("HELLO", EncodeTypes.QR),
            ("A1B2C3D4", EncodeTypes.DataMatrix),
            ("CODE-XYZ", EncodeTypes.Code39)
        };

        var tasks = new List<Task<(int Index, byte[] ImageData)>>();

        for (int i = 0; i < requests.Count; i++)
        {
            int index = i; // capture loop variable
            var (codeText, type) = requests[i];
            tasks.Add(Task.Run(async () =>
            {
                var imageData = await GenerateBarcodeAsync(codeText, type);
                return (Index: index, ImageData: imageData);
            }));
        }

        var results = await Task.WhenAll(tasks);

        foreach (var result in results)
        {
            string fileName = $"barcode_{result.Index}.png";
            await File.WriteAllBytesAsync(fileName, result.ImageData);
            Console.WriteLine($"Saved {fileName}");
        }
    }

    private static Task<byte[]> GenerateBarcodeAsync(string codeText, BaseEncodeType type)
    {
        return Task.Run(() =>
        {
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Example of setting a generation parameter
                generator.Parameters.Resolution = 300; // DPI

                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    return memoryStream.ToArray();
                }
            }
        });
    }
}
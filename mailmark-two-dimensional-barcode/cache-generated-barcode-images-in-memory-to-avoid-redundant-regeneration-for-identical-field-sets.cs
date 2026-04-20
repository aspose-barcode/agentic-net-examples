using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class BarcodeCache
{
    private static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>();

    public static byte[] GetOrCreate(BaseEncodeType type, string codeText)
    {
        string key = $"{type}:{codeText}";
        if (_cache.TryGetValue(key, out byte[] cachedData))
        {
            Console.WriteLine($"Cache hit for [{key}]");
            return cachedData;
        }

        Console.WriteLine($"Generating barcode for [{key}]");
        using (BarcodeGenerator generator = new BarcodeGenerator(type, codeText))
        {
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] data = ms.ToArray();
                    _cache[key] = data;
                    return data;
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        var requests = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Code128, "ABC123"), // duplicate
            (EncodeTypes.QR, "https://example.com"), // duplicate
            (EncodeTypes.EAN13, "1234567890128")
        };

        int index = 1;
        foreach (var (type, text) in requests)
        {
            byte[] imageData = BarcodeCache.GetOrCreate(type, text);
            string fileName = $"barcode_{index}_{type}.png";
            File.WriteAllBytes(fileName, imageData);
            Console.WriteLine($"Saved {fileName}");
            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}
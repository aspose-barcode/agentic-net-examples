using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace HanXinBarcodeCacheDemo
{
    // Simple in‑memory cache for barcode images keyed by the encoded text.
    internal static class BarcodeCache
    {
        // Stores PNG image bytes for each unique code text.
        private static readonly Dictionary<string, byte[]> _cache = new Dictionary<string, byte[]>(StringComparer.Ordinal);

        // Returns cached image bytes or generates a new barcode, caches it and returns the bytes.
        public static byte[] GetOrAdd(string codeText)
        {
            if (codeText == null) throw new ArgumentNullException(nameof(codeText));

            if (_cache.TryGetValue(codeText, out var cachedBytes))
            {
                return cachedBytes;
            }

            // Generate Han Xin barcode image.
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Optional: set image size or other parameters here if needed.
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    using (var ms = new MemoryStream())
                    {
                        // Save as PNG into memory.
                        bitmap.Save(ms, ImageFormat.Png);
                        var imageBytes = ms.ToArray();
                        _cache[codeText] = imageBytes;
                        return imageBytes;
                    }
                }
            }
        }
    }

    internal class Program
    {
        // Sanitizes a string to be safe for file names.
        private static string SanitizeFileName(string text)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                text = text.Replace(c, '_');
            }
            return text;
        }

        static void Main()
        {
            // Sample texts to encode.
            var samples = new[]
            {
                "1234567890ABCDEFGabcdefg,Han Xin Code",
                "ΑΒΓΔΕ",
                "abcd АБВ ıntəˈnæʃənəl テスト 안녕하세요 테스트 테스트",
                "https://www.example.com/search=test",
                @"\gb180302b:漄\gb180304b:㐁\region1:全\region2:螅\numeric:123\text:qwe\unicode:ıntəˈnæʃənəl"
            };

            // Ensure output directory exists.
            var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Process each sample, using the cache.
            for (int i = 0; i < samples.Length; i++)
            {
                var text = samples[i];
                var imageBytes = BarcodeCache.GetOrAdd(text);

                // Write PNG file.
                var safeName = SanitizeFileName(text);
                var filePath = Path.Combine(outputDir, $"barcode_{i + 1}_{safeName}.png");
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(imageBytes, 0, imageBytes.Length);
                }
            }

            // Program completes without waiting for user input.
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    // In‑memory cache: key = symbology name + code text, value = PNG bytes
    private static readonly Dictionary<string, byte[]> _barcodeCache = new Dictionary<string, byte[]>();

    static void Main()
    {
        // Ensure output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define a small set of DataBar symbologies
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded,
            EncodeTypes.DatabarExpandedStacked
        };

        foreach (BaseEncodeType type in dataBarTypes)
        {
            // Choose appropriate code text per symbology
            string codeText = type == EncodeTypes.DatabarLimited
                ? "(01)08888888888888"
                : "(01)12345678901231";

            string cacheKey = $"{type.TypeName}_{codeText}";
            byte[] imageBytes;

            if (_barcodeCache.TryGetValue(cacheKey, out imageBytes))
            {
                // Cached image found – write it to file
                string cachedPath = Path.Combine(outputDir, $"{cacheKey}_cached.png");
                File.WriteAllBytes(cachedPath, imageBytes);
                Console.WriteLine($"Cache hit: {cachedPath}");
                continue;
            }

            // Cache miss – generate barcode
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Use interpolation auto‑size mode for consistent dimensions
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;
                generator.Parameters.Resolution = 96;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Generate image into a memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    imageBytes = ms.ToArray();

                    // Store in cache
                    _barcodeCache[cacheKey] = imageBytes;

                    // Save generated image to file
                    string filePath = Path.Combine(outputDir, $"{cacheKey}.png");
                    File.WriteAllBytes(filePath, imageBytes);
                    Console.WriteLine($"Generated: {filePath}");
                }
            }
        }

        // Demonstrate cache reuse by requesting the first barcode again
        BaseEncodeType repeatType = EncodeTypes.DatabarOmniDirectional;
        string repeatCode = "(01)12345678901231";
        string repeatKey = $"{repeatType.TypeName}_{repeatCode}";
        if (_barcodeCache.TryGetValue(repeatKey, out byte[] repeatBytes))
        {
            string repeatPath = Path.Combine(outputDir, $"{repeatKey}_second.png");
            File.WriteAllBytes(repeatPath, repeatBytes);
            Console.WriteLine($"Reused cache for second file: {repeatPath}");
        }
    }
}
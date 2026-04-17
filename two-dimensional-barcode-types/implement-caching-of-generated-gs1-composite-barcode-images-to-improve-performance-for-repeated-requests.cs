using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // In‑memory cache: key = codetext, value = PNG image bytes
        var cache = new Dictionary<string, byte[]>();

        // Sample requests (some duplicates to demonstrate caching)
        var requests = new[]
        {
            "(01)03212345678906|(21)A1B2C3D4E5F6G7H8",
            "(01)03212345678906|(21)A1B2C3D4E5F6G7H8", // duplicate
            "(01)12345678901231|(10)ABC123"
        };

        for (int i = 0; i < requests.Length; i++)
        {
            string codeText = requests[i];
            byte[] imageBytes;

            // Try to get the image from cache
            if (!cache.TryGetValue(codeText, out imageBytes))
            {
                // Not cached – generate the GS1 Composite barcode
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
                {
                    // Configure linear and 2D components
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                    // Set dimensions
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                    // Save to a memory stream in PNG format
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        imageBytes = ms.ToArray();
                        // Store in cache for future requests
                        cache[codeText] = imageBytes;
                    }
                }
            }

            // Write the image to a file for demonstration purposes
            string fileName = $"barcode_{i}.png";
            File.WriteAllBytes(fileName, imageBytes);
            Console.WriteLine($"Saved {fileName} (cached: {cache.ContainsKey(codeText)})");
        }
    }
}
// Title: Barcode Image Caching with Aspose.BarCode
// Description: Demonstrates caching of generated PNG barcode images in memory to prevent redundant regeneration for identical barcode parameters.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, BarcodeCacheKey, and a simple in‑memory Dictionary cache. Developers often need to generate many barcodes with repeated settings, and caching improves performance and reduces CPU load. The pattern is useful for web services, batch processing, or any application that repeatedly renders the same barcodes.
// Prompt: Cache generated barcode images in memory to avoid redundant regeneration for identical field sets.
// Tags: barcode, symbology, caching, png, aspose.barcode, generation, memory cache

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeCacheDemo
{
    /// <summary>
    /// Represents a unique set of barcode generation parameters.
    /// For simplicity only symbology type and code text are considered.
    /// Extend this class with additional properties (e.g., XDimension, colors) as needed.
    /// </summary>
    class BarcodeCacheKey : IEquatable<BarcodeCacheKey>
    {
        public BaseEncodeType EncodeType { get; }
        public string CodeText { get; }

        public BarcodeCacheKey(BaseEncodeType encodeType, string codeText)
        {
            EncodeType = encodeType;
            CodeText = codeText ?? string.Empty;
        }

        public bool Equals(BarcodeCacheKey other)
        {
            if (other is null) return false;
            return EncodeType.Equals(other.EncodeType) && CodeText == other.CodeText;
        }

        public override bool Equals(object obj) => Equals(obj as BarcodeCacheKey);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + EncodeType.GetHashCode();
                hash = hash * 31 + CodeText.GetHashCode();
                return hash;
            }
        }
    }

    /// <summary>
    /// Simple in‑memory cache for barcode PNG bytes keyed by <see cref="BarcodeCacheKey"/>.
    /// </summary>
    class BarcodeCache
    {
        // Cache stores the generated PNG bytes keyed by barcode parameters.
        private readonly Dictionary<BarcodeCacheKey, byte[]> _cache = new Dictionary<BarcodeCacheKey, byte[]>();

        /// <summary>
        /// Returns PNG image bytes for the requested barcode, using the cache when possible.
        /// </summary>
        /// <param name="encodeType">The barcode symbology.</param>
        /// <param name="codeText">The data to encode.</param>
        /// <returns>Byte array containing the PNG image.</returns>
        public byte[] GetBarcodeImage(BaseEncodeType encodeType, string codeText)
        {
            var key = new BarcodeCacheKey(encodeType, codeText);
            if (_cache.TryGetValue(key, out var imageBytes))
            {
                // Cache hit – return existing image.
                Console.WriteLine($"Cache hit for type {encodeType} and text \"{codeText}\".");
                return imageBytes;
            }

            // Cache miss – generate a new barcode image.
            Console.WriteLine($"Generating barcode for type {encodeType} and text \"{codeText}\".");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting some common parameters.
                generator.Parameters.Barcode.XDimension.Point = 2f;               // module size
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Resolution = 300;                           // DPI

                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    imageBytes = ms.ToArray();
                }
            }

            // Store the generated image in the cache for future reuse.
            _cache[key] = imageBytes;
            return imageBytes;
        }
    }

    /// <summary>
    /// Demonstrates barcode generation with caching and saves the resulting PNG files.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that demonstrates barcode caching and saves PNG files.
        /// </summary>
        static void Main()
        {
            var cache = new BarcodeCache();

            // Sample data: some barcodes are duplicated to demonstrate caching.
            var samples = new (BaseEncodeType type, string text)[]
            {
                (EncodeTypes.Code128, "ABC123"),
                (EncodeTypes.QR, "https://example.com"),
                (EncodeTypes.Code128, "ABC123"), // duplicate
                (EncodeTypes.DataMatrix, "DataMatrixSample"),
                (EncodeTypes.QR, "https://example.com") // duplicate
            };

            // Generate images and write them to files.
            for (int i = 0; i < samples.Length; i++)
            {
                var (type, text) = samples[i];
                byte[] pngBytes = cache.GetBarcodeImage(type, text);

                string fileName = $"barcode_{i + 1}.png";
                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(pngBytes, 0, pngBytes.Length);
                }

                Console.WriteLine($"Saved {fileName}");
            }

            // Program ends – no waiting for user input.
        }
    }
}
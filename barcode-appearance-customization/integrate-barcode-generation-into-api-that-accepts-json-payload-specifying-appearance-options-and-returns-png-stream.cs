using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeApiDemo
{
    // Model representing the JSON payload
    public class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public int? BarColorArgb { get; set; }          // Optional ARGB color for bars
        public int? BackgroundColorArgb { get; set; }   // Optional ARGB color for background
        public float? BarHeight { get; set; }           // Optional bar height in points
        public float? PaddingLeft { get; set; }         // Optional padding values in points
        public float? PaddingTop { get; set; }
        public float? PaddingRight { get; set; }
        public float? PaddingBottom { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Load JSON payload (from file "input.json" or create a sample if missing)
            const string inputFile = "input.json";
            if (!File.Exists(inputFile))
            {
                var sample = new BarcodeRequest
                {
                    Symbology = "Code128",
                    CodeText = "123ABC",
                    BarColorArgb = unchecked((int)0xFF000000), // Black
                    BackgroundColorArgb = unchecked((int)0xFFFFFFFF), // White
                    BarHeight = 50f,
                    PaddingLeft = 5f,
                    PaddingTop = 5f,
                    PaddingRight = 5f,
                    PaddingBottom = 5f
                };
                var json = JsonSerializer.Serialize(sample, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(inputFile, json);
                Console.WriteLine($"Sample input file created at '{inputFile}'. Edit it and rerun the program.");
                return;
            }

            string jsonPayload = File.ReadAllText(inputFile);
            BarcodeRequest request;
            try
            {
                request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
                if (request == null)
                    throw new ArgumentException("Deserialized request is null.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse JSON payload: {ex.Message}");
                return;
            }

            // Resolve symbology to EncodeTypes member
            BaseEncodeType encodeType = ResolveEncodeType(request.Symbology);

            // Create generator and apply settings
            using (var generator = new BarcodeGenerator(encodeType))
            {
                generator.CodeText = request.CodeText ?? string.Empty;

                // Bar color
                if (request.BarColorArgb.HasValue)
                {
                    generator.Parameters.Barcode.BarColor = Color.FromArgb(request.BarColorArgb.Value);
                }

                // Background color
                if (request.BackgroundColorArgb.HasValue)
                {
                    generator.Parameters.BackColor = Color.FromArgb(request.BackgroundColorArgb.Value);
                }

                // Bar height
                if (request.BarHeight.HasValue)
                {
                    generator.Parameters.Barcode.BarHeight.Point = request.BarHeight.Value;
                }

                // Padding
                if (request.PaddingLeft.HasValue)
                    generator.Parameters.Barcode.Padding.Left.Point = request.PaddingLeft.Value;
                if (request.PaddingTop.HasValue)
                    generator.Parameters.Barcode.Padding.Top.Point = request.PaddingTop.Value;
                if (request.PaddingRight.HasValue)
                    generator.Parameters.Barcode.Padding.Right.Point = request.PaddingRight.Value;
                if (request.PaddingBottom.HasValue)
                    generator.Parameters.Barcode.Padding.Bottom.Point = request.PaddingBottom.Value;

                // Generate PNG into a memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    byte[] pngBytes = ms.ToArray();

                    // Write PNG bytes to a file (simulating API response stream)
                    const string outputFile = "output.png";
                    File.WriteAllBytes(outputFile, pngBytes);
                    Console.WriteLine($"Barcode image saved to '{outputFile}'.");
                }
            }
        }

        // Maps a string name to a known EncodeTypes member; throws if unsupported.
        private static BaseEncodeType ResolveEncodeType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Symbology name is required.");

            // Normalise for case‑insensitive comparison
            string key = name.Trim().ToLowerInvariant();

            return key switch
            {
                "code128" => EncodeTypes.Code128,
                "code39" => EncodeTypes.Code39,
                "code39fullascii" => EncodeTypes.Code39FullASCII,
                "ean13" => EncodeTypes.EAN13,
                "ean8" => EncodeTypes.EAN8,
                "qr" => EncodeTypes.QR,
                "datamatrix" => EncodeTypes.DataMatrix,
                "pdf417" => EncodeTypes.Pdf417,
                // Add more mappings as needed
                _ => throw new ArgumentException($"Unsupported symbology: {name}")
            };
        }
    }
}
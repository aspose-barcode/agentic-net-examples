using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

namespace SyntheticFncBarcodes
{
    class Program
    {
        static void Main()
        {
            // Directory to store generated images
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Define barcode data with embedded FNC characters (FNC1, FNC2, FNC3)
            // In Code128, FNC1 = 0xF1, FNC2 = 0xF2, FNC3 = 0xF3
            var fncData = new (string fileName, byte[] bytes)[]
            {
                ("code128_fnc1.png", new byte[] { 0xF1, (byte)'A', (byte)'B', (byte)'C' }),
                ("code128_fnc2.png", new byte[] { 0xF2, (byte)'1', (byte)'2', (byte)'3' }),
                ("code128_fnc3.png", new byte[] { 0xF3, (byte)'X', (byte)'Y', (byte)'Z' })
            };

            // Generate barcode images
            foreach (var (fileName, bytes) in fncData)
            {
                string filePath = Path.Combine(outputDir, fileName);
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    // Set the raw byte sequence containing the FNC character
                    generator.SetCodeText(bytes);
                    // Optional: set image size for consistency
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                    // Save the barcode image
                    generator.Save(filePath);
                }
                Console.WriteLine($"Generated: {filePath}");
            }

            // Demonstrate StripFNC behavior during reading
            foreach (var (fileName, _) in fncData)
            {
                string filePath = Path.Combine(outputDir, fileName);
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Read without stripping FNC characters
                using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
                {
                    reader.BarcodeSettings.StripFNC = false;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"[StripFNC=False] {Path.GetFileName(filePath)} => {result.CodeText}");
                    }
                }

                // Read with stripping FNC characters
                using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
                {
                    reader.BarcodeSettings.StripFNC = true;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"[StripFNC=True]  {Path.GetFileName(filePath)} => {result.CodeText}");
                    }
                }
            }
        }
    }
}
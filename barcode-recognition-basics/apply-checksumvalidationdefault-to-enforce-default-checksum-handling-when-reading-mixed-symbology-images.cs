using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace MixedSymbologyChecksumDemo
{
    class Program
    {
        static void Main()
        {
            // Paths for temporary barcode images
            string code128Path = "code128.png";
            string ean13Path = "ean13.png";
            string mixedPath = "mixed.png";

            // Generate a Code128 barcode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                generator.Save(code128Path);
            }

            // Generate an EAN13 barcode (includes checksum)
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                generator.Save(ean13Path);
            }

            // Combine the two barcodes into a single image
            using (var bmp1 = new Bitmap(code128Path))
            using (var bmp2 = new Bitmap(ean13Path))
            {
                int width = bmp1.Width + bmp2.Width;
                int height = Math.Max(bmp1.Height, bmp2.Height);
                using (var combined = new Bitmap(width, height))
                {
                    using (var graphics = Graphics.FromImage(combined))
                    {
                        graphics.Clear(Color.White);
                        graphics.DrawImage(bmp1, 0, 0);
                        graphics.DrawImage(bmp2, bmp1.Width, 0);
                    }
                    combined.Save(mixedPath);
                }
            }

            // Read all barcodes from the combined image using default checksum validation
            using (var reader = new BarCodeReader(mixedPath, DecodeType.AllSupportedTypes))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"  Value: {result.Extended.OneD.Value}, Checksum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
        }
    }
}
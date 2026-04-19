using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare three barcode images
        string[] texts = { "ABC123", "DEF456", "GHI789" };
        Bitmap[] barcodes = new Bitmap[texts.Length];

        for (int i = 0; i < texts.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, texts[i]))
            {
                // Generate bitmap for each barcode
                barcodes[i] = generator.GenerateBarCodeImage();
            }
        }

        // Combine barcodes into one image
        int totalWidth = 0;
        int maxHeight = 0;
        foreach (var bmp in barcodes)
        {
            totalWidth += bmp.Width;
            if (bmp.Height > maxHeight) maxHeight = bmp.Height;
        }

        using (var combined = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                int offsetX = 0;
                foreach (var bmp in barcodes)
                {
                    graphics.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                    offsetX += bmp.Width;
                }
            }

            string combinedPath = "combined.png";
            combined.Save(combinedPath, ImageFormat.Png);
        }

        // Ensure the combined image exists before reading
        string imagePath = "combined.png";
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // Read barcodes from the combined image, limiting to three results
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            int count = 0;
            foreach (var result in reader.ReadBarCodes())
            {
                if (count >= 3) break;
                Console.WriteLine($"Detected Barcode {count + 1}: Type={result.CodeTypeName}, Text={result.CodeText}");
                count++;
            }
        }

        // Cleanup generated bitmaps
        foreach (var bmp in barcodes)
        {
            bmp.Dispose();
        }
    }
}
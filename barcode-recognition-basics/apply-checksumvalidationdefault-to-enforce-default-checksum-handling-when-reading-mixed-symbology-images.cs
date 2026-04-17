using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for individual barcode images
        string eanPath = Path.Combine(outputDir, "ean13.png");
        string code39Path = Path.Combine(outputDir, "code39.png");
        string mixedPath = Path.Combine(outputDir, "mixed.png");

        // Generate EAN13 barcode (valid checksum)
        using (BarcodeGenerator eanGen = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            eanGen.Save(eanPath);
        }

        // Generate Code39 barcode
        using (BarcodeGenerator code39Gen = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            code39Gen.Save(code39Path);
        }

        // Combine both barcodes into one image (side by side)
        if (File.Exists(eanPath) && File.Exists(code39Path))
        {
            using (Image eanImg = Image.FromFile(eanPath))
            using (Image code39Img = Image.FromFile(code39Path))
            {
                int width = eanImg.Width + code39Img.Width;
                int height = Math.Max(eanImg.Height, code39Img.Height);
                using (Bitmap combined = new Bitmap(width, height))
                using (Graphics g = Graphics.FromImage(combined))
                {
                    g.Clear(Color.White);
                    g.DrawImage(eanImg, 0, 0, eanImg.Width, eanImg.Height);
                    g.DrawImage(code39Img, eanImg.Width, 0, code39Img.Width, code39Img.Height);
                    combined.Save(mixedPath);
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate individual barcode images.");
            return;
        }

        // Verify combined image exists
        if (!File.Exists(mixedPath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // Read barcodes from the combined image with default checksum validation
        using (BarCodeReader reader = new BarCodeReader(mixedPath, DecodeType.EAN13, DecodeType.Code39))
        {
            // Apply default checksum handling
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    // For 1D barcodes, also display checksum if available
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Value (without checksum): {result.Extended.OneD.Value}");
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
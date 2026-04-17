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
        // Folder for temporary images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Base QR code image
        string basePath = Path.Combine(tempDir, "qr_normal.png");
        GenerateQrCode("https://example.com", basePath);

        // Dimmed version (simulating low lighting)
        string dimPath = Path.Combine(tempDir, "qr_dim.png");
        CreateAdjustedImage(basePath, dimPath, Color.FromArgb(128, Color.Black));

        // Brightened version (simulating strong lighting)
        string brightPath = Path.Combine(tempDir, "qr_bright.png");
        CreateAdjustedImage(basePath, brightPath, Color.FromArgb(128, Color.White));

        // Analyze each image
        AnalyzeImage("Normal Lighting", basePath);
        AnalyzeImage("Low Lighting (Dim)", dimPath);
        AnalyzeImage("Strong Lighting (Bright)", brightPath);
    }

    // Generates a QR code with default settings
    private static void GenerateQrCode(string text, string filePath)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Optional: set higher error correction to survive lighting changes
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(filePath);
        }
    }

    // Creates a new image by overlaying a semi‑transparent color to simulate lighting
    private static void CreateAdjustedImage(string sourcePath, string destPath, Color overlayColor)
    {
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine($"Source image not found: {sourcePath}");
            return;
        }

        using (Bitmap bitmap = (Bitmap)Image.FromFile(sourcePath))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (SolidBrush brush = new SolidBrush(overlayColor))
                {
                    graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
                }
            }

            bitmap.Save(destPath, ImageFormat.Png);
        }
    }

    // Reads a QR code from an image and prints its ReadingQuality
    private static void AnalyzeImage(string description, string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image not found: {imagePath}");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{description}:");
                Console.WriteLine($"  Code Text       : {result.CodeText}");
                Console.WriteLine($"  Reading Quality : {result.ReadingQuality}%");
                Console.WriteLine($"  Confidence      : {result.Confidence}");
            }
        }
    }
}
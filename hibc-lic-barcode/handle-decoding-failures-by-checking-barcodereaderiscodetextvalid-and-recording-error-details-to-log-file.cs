using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for sample images and log file
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string validBarcodePath = Path.Combine(outputDir, "valid.png");
        string blankImagePath = Path.Combine(outputDir, "blank.png");
        string logFilePath = Path.Combine(outputDir, "decode_log.txt");

        // Ensure previous log is cleared
        if (File.Exists(logFilePath))
            File.Delete(logFilePath);

        // Generate a valid Code128 barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(validBarcodePath);
        }

        // Create a blank image (no barcode)
        using (var bitmap = new Bitmap(200, 200))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
            }
            bitmap.Save(blankImagePath, Aspose.Drawing.Imaging.ImageFormat.Png);
        }

        // Array of images to decode
        string[] imagesToDecode = new[] { validBarcodePath, blankImagePath };

        foreach (string imagePath in imagesToDecode)
        {
            // Verify the image file exists
            if (!File.Exists(imagePath))
            {
                LogError(logFilePath, $"File not found: {imagePath}");
                continue;
            }

            // Read barcodes from the image
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes().ToArray();

                if (results.Length == 0)
                {
                    LogError(logFilePath, $"No barcode detected in image: {imagePath}");
                    continue;
                }

                foreach (var result in results)
                {
                    // Validate decoded text
                    if (string.IsNullOrEmpty(result.CodeText))
                    {
                        LogError(logFilePath, $"Decoded barcode has empty CodeText in image: {imagePath}");
                    }
                    else
                    {
                        Console.WriteLine($"Decoded from '{Path.GetFileName(imagePath)}': {result.CodeText}");
                    }
                }
            }
        }

        Console.WriteLine("Decoding completed. Check log file for any errors.");
    }

    static void LogError(string logPath, string message)
    {
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
        File.AppendAllText(logPath, entry);
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a temporary directory for barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        Directory.CreateDirectory(tempDir);

        // Generate sample barcodes
        List<string> barcodeFiles = GenerateBarcodes(tempDir, 5);

        // Add a blank image (no barcode) to test false positives
        string blankImagePath = Path.Combine(tempDir, "blank.png");
        CreateBlankImage(blankImagePath, 200, 100);

        // Benchmark with MinimalXDimension = 0 and = 2
        int[] minimalValues = new int[] { 0, 2 };
        foreach (int minimal in minimalValues)
        {
            int falsePositives = 0;
            int truePositives = 0;

            // Test each barcode image
            foreach (string file in barcodeFiles)
            {
                string expectedText = Path.GetFileNameWithoutExtension(file);
                bool isCorrect = RecognizeAndValidate(file, expectedText, minimal);
                if (isCorrect)
                    truePositives++;
                else
                    falsePositives++;
            }

            // Test blank image for false positives
            bool blankDetected = RecognizeAndValidate(blankImagePath, null, minimal);
            if (blankDetected)
                falsePositives++;

            Console.WriteLine($"MinimalXDimension = {minimal} pixels:");
            Console.WriteLine($"  True Positives : {truePositives}");
            Console.WriteLine($"  False Positives: {falsePositives}");
            Console.WriteLine();
        }

        // Cleanup temporary files
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    // Generates a specified number of Code128 barcodes with simple numeric text
    static List<string> GenerateBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string codeText = $"12345{i}";
            string filePath = Path.Combine(folder, $"{codeText}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Ensure a consistent size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Save(filePath);
            }
            files.Add(filePath);
        }
        return files;
    }

    // Creates a plain white bitmap image
    static void CreateBlankImage(string path, int width, int height)
    {
        using (var bitmap = new Bitmap(width, height))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
            }
            bitmap.Save(path, Aspose.Drawing.Imaging.ImageFormat.Png);
        }
    }

    // Recognizes a barcode from an image and validates against expected text.
    // Returns true if a barcode is correctly recognized, false otherwise.
    static bool RecognizeAndValidate(string imagePath, string expectedText, int minimalXDimension)
    {
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image file not found: {imagePath}");

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure recognition to use MinimalXDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = minimalXDimension;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // If expectedText is null (blank image), any detection is a false positive
                if (expectedText == null)
                    return true;

                // Compare recognized text with expected
                if (result.CodeText == expectedText)
                    return true;
            }
        }
        return false;
    }
}
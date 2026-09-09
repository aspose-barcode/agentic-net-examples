// Title: Generate, Recognize, and Log Barcode Quality Metrics in a Temporary Folder
// Description: This example creates several barcode images, reads them back with Aspose.BarCode, and records detection quality metrics to a log file.
// Category-Description: Demonstrates core Aspose.BarCode operations—barcode generation (BarcodeGenerator), barcode recognition (BarCodeReader), and quality‑settings configuration. Typical for developers building scanning utilities, quality‑control pipelines, or services that need to validate barcode readability. The example shows how to produce PNG images, apply high‑quality recognition settings, and extract detailed region and quality information.
// Prompt: Develop a Windows service that monitors a folder, reads incoming barcode images, and logs quality metrics.
// Tags: barcode generation, barcode recognition, quality metrics, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating barcode images, recognizing them, and logging quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates sample barcodes, reads them back, and writes a quality‑metrics log.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the demo files
        string folder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        var filePaths = new List<string>();

        // Generate barcode images and collect their file paths
        foreach (var sample in samples)
        {
            string path = Path.Combine(folder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Set X‑dimension for better visual quality
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save as PNG
                generator.Save(path, BarCodeImageFormat.Png);
            }
            filePaths.Add(path);
        }

        // Path for the log file that will contain quality metrics
        string logPath = Path.Combine(folder, "log.txt");

        // Process each generated image: read, evaluate quality, and log results
        foreach (var file in filePaths)
        {
            if (!File.Exists(file))
            {
                File.AppendAllText(logPath, $"File not found: {file}{Environment.NewLine}");
                continue;
            }

            // Use all supported barcode types for recognition
            BaseDecodeType decode = DecodeType.AllSupportedTypes;
            using (var reader = new BarCodeReader(file, decode))
            {
                // Configure high‑quality recognition settings
                reader.QualitySettings = QualitySettings.HighQuality;
                reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

                BarCodeResult[] results;
                try
                {
                    // Attempt to read barcodes from the image
                    results = reader.ReadBarCodes();
                }
                catch (ArgumentException)
                {
                    // Log image‑loading failures (e.g., unsupported format)
                    File.AppendAllText(logPath, $"Failed to load image: {file}{Environment.NewLine}");
                    continue;
                }

                if (results.Length == 0)
                {
                    // No barcode detected – log the outcome
                    File.AppendAllText(logPath, $"No barcode detected in {Path.GetFileName(file)}{Environment.NewLine}");
                    continue;
                }

                // Log details for each detected barcode
                foreach (var result in results)
                {
                    var rect = result.Region.Rectangle;
                    string line = $"File: {Path.GetFileName(file)}, CodeText: {result.CodeText}, Symbology: {result.CodeTypeName}, Quality: {result.ReadingQuality}, Region: X={rect.X}, Y={rect.Y}, W={rect.Width}, H={rect.Height}, Angle={result.Region.Angle}";
                    Console.WriteLine(line);
                    File.AppendAllText(logPath, line + Environment.NewLine);
                }
            }
        }

        // Inform the user where the log file is located
        Console.WriteLine($"Log written to: {logPath}");
    }
}
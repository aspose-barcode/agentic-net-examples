// Title: Barcode Generation, Recognition, and Quality Logging Example
// Description: Demonstrates generating multiple barcode images, recognizing them, and logging quality metrics to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, showcasing the use of BarcodeGenerator for encoding, BarCodeReader for decoding, and QualitySettings for controlling recognition accuracy. Typical use cases include batch barcode creation, automated scanning pipelines, and quality analysis in manufacturing or logistics applications. Developers often need to generate barcodes, read them from images, and evaluate reading quality, which this sample illustrates.
// Prompt: Develop a Windows service that monitors a folder, reads incoming barcode images, and logs quality metrics.
// Tags: barcode generation, barcode recognition, quality settings, aspose.barcode, qr, code128, datamatrix, logging

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch barcode creation, recognition, and quality logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them back, and logs quality metrics.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a dedicated temporary folder for generated images and logs.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------
        // 2. Prepare sample data: a list of barcode symbologies and texts.
        // --------------------------------------------------------------
        var samples = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.QR, "SampleQR123"),
            (EncodeTypes.Code128, "CODE128ABC"),
            (EncodeTypes.DataMatrix, "DMATRIX")
        };

        var generatedFiles = new List<string>();

        // --------------------------------------------------------------
        // 3. Generate barcode images for each sample and store file paths.
        // --------------------------------------------------------------
        for (int i = 0; i < samples.Count; i++)
        {
            var (encode, text) = samples[i];
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");

            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Optional encoding parameters.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated barcode as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            generatedFiles.Add(filePath);
        }

        // --------------------------------------------------------------
        // 4. Initialize a log file to capture processing details.
        // --------------------------------------------------------------
        string logPath = Path.Combine(tempFolder, "log.txt");
        File.WriteAllText(logPath, $"Barcode processing started at {DateTime.Now}{Environment.NewLine}");

        // --------------------------------------------------------------
        // 5. Process each generated barcode image: read, evaluate quality, and log.
        // --------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                File.AppendAllText(logPath, $"File not found: {file}{Environment.NewLine}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, decodeType))
                {
                    // Configure high‑quality recognition settings.
                    reader.QualitySettings = QualitySettings.HighQuality;
                    reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
                    reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Normal;
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Perform barcode detection.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        File.AppendAllText(logPath, $"No barcode detected in file: {Path.GetFileName(file)}{Environment.NewLine}");
                        continue;
                    }

                    // Log each detected barcode with its quality metrics.
                    foreach (var result in results)
                    {
                        string codeText = result.CodeText ?? string.Empty;
                        string symbology = result.CodeTypeName ?? string.Empty;
                        double quality = result.ReadingQuality;
                        var rect = result.Region.Rectangle;
                        double angle = result.Region.Angle;

                        string entry = $"File: {Path.GetFileName(file)} | Text: {codeText} | Symbology: {symbology} | Quality: {quality:F2} | Region: X={rect.X}, Y={rect.Y}, W={rect.Width}, H={rect.Height} | Angle: {angle:F2}";
                        Console.WriteLine(entry);
                        File.AppendAllText(logPath, entry + Environment.NewLine);
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Specific handling for image loading failures.
                File.AppendAllText(logPath, $"Failed to load image {Path.GetFileName(file)}: {ex.Message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // General error handling for unexpected issues.
                File.AppendAllText(logPath, $"Unexpected error processing {Path.GetFileName(file)}: {ex.Message}{Environment.NewLine}");
            }
        }

        // --------------------------------------------------------------
        // 6. Indicate completion and provide the log file location.
        // --------------------------------------------------------------
        Console.WriteLine($"Processing completed. Log file: {logPath}");
    }
}
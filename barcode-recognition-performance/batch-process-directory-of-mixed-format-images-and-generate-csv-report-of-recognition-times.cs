// Title: Batch barcode image processing with CSV timing report
// Description: Demonstrates generating several barcode images, scanning them, measuring recognition time, and exporting results to a CSV file.
// Category-Description: Shows how to use Aspose.BarCode Generation and Recognition APIs for batch processing of mixed‑format barcode images. Typical use cases include performance benchmarking, bulk scanning, and automated reporting. Developers often work with BarcodeGenerator, BarCodeReader, and related parameter classes to create, read, and analyze barcodes in various image formats.
// Prompt: Batch process a directory of mixed‑format images and generate a CSV report of recognition times.
// Tags: barcode symbology, batch processing, csv output, performance measurement, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates sample barcode images, reads them back,
/// measures recognition time for each image, and writes a CSV report.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the batch processing run.
        // --------------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // --------------------------------------------------------------------
        // Generate sample barcode images of various symbologies.
        // --------------------------------------------------------------------
        var barcodeFiles = new System.Collections.Generic.List<string>();

        // Local helper to generate a barcode image and store its path.
        void GenerateBarcode(BaseEncodeType encodeType, string codeText, string fileName)
        {
            string filePath = Path.Combine(batchFolder, fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType))
            {
                generator.CodeText = codeText;
                // Optional visual settings for better readability.
                generator.Parameters.Barcode.XDimension.Point = 2.0f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Generate a set of barcodes covering common symbologies.
        GenerateBarcode(EncodeTypes.QR, "Sample QR", "qr.png");
        GenerateBarcode(EncodeTypes.Code128, "CODE128", "code128.png");
        GenerateBarcode(EncodeTypes.DataMatrix, "DM12345", "datamatrix.png");
        GenerateBarcode(EncodeTypes.Aztec, "AztecText", "aztec.png");
        GenerateBarcode(EncodeTypes.Pdf417, "Pdf417Sample", "pdf417.png");

        // --------------------------------------------------------------------
        // Prepare a CSV builder with a header row.
        // --------------------------------------------------------------------
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,RecognitionTimeMs,BarcodesFound");

        // --------------------------------------------------------------------
        // Process each generated image: read barcodes, measure time, and log results.
        // --------------------------------------------------------------------
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    int foundCount = results?.Length ?? 0;
                    string line = $"{Path.GetFileName(filePath)},{stopwatch.ElapsedMilliseconds},{foundCount}";
                    csvBuilder.AppendLine(line);

                    // Output processing details to the console for quick feedback.
                    Console.WriteLine($"Processed {Path.GetFileName(filePath)} - Time: {stopwatch.ElapsedMilliseconds} ms, Found: {foundCount}");
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Write the accumulated CSV report to the batch folder.
        // --------------------------------------------------------------------
        string reportPath = Path.Combine(batchFolder, "RecognitionReport.csv");
        File.WriteAllText(reportPath, csvBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"CSV report written to: {reportPath}");
    }
}
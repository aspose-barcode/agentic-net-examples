// Title: Batch barcode recognition with CSV timing report
// Description: Demonstrates processing a folder of mixed‑format barcode images, recognizing them, and creating a CSV file with recognition times.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to batch‑process multiple image formats. Developers often need to automate barcode scanning across large image sets and log performance metrics, which this snippet illustrates.
// Prompt: Batch process a directory of mixed‑format images and generate a CSV report of recognition times.
// Tags: barcode symbology, batch processing, csv report, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample barcodes, scans them, and writes a CSV report
/// containing file name, recognition time, number of barcodes found, and the first barcode text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the sample generation, processing, and reporting workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a folder for sample barcode images
        // --------------------------------------------------------------------
        string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(imagesFolder))
        {
            Directory.CreateDirectory(imagesFolder);
        }

        // --------------------------------------------------------------------
        // 2. Generate a few sample barcode images of different formats
        // --------------------------------------------------------------------
        GenerateSampleBarcodes(imagesFolder);

        // --------------------------------------------------------------------
        // 3. Prepare CSV report file and header line
        // --------------------------------------------------------------------
        string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "report.csv");
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("FileName,RecognitionTimeMs,BarcodesFound,FirstCodeText");

        // --------------------------------------------------------------------
        // 4. Process PNG, JPG and BMP files in the images folder
        // --------------------------------------------------------------------
        string[] patterns = new[] { "*.png", "*.jpg", "*.bmp" };
        foreach (string pattern in patterns)
        {
            string[] files = Directory.GetFiles(imagesFolder, pattern);
            foreach (string filePath in files)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // ------------------------------------------------------------
                // Measure recognition time for each image
                // ------------------------------------------------------------
                var stopwatch = Stopwatch.StartNew();
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    int count = results.Length;
                    string firstCode = count > 0 ? results[0].CodeText : string.Empty;
                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    // --------------------------------------------------------
                    // Append a line to the CSV report
                    // --------------------------------------------------------
                    string fileName = Path.GetFileName(filePath);
                    csvBuilder.AppendLine($"{fileName},{elapsedMs},{count},{firstCode}");
                }
            }
        }

        // --------------------------------------------------------------------
        // 5. Write CSV report to disk
        // --------------------------------------------------------------------
        File.WriteAllText(csvPath, csvBuilder.ToString());
        Console.WriteLine($"Report generated at: {csvPath}");
    }

    /// <summary>
    /// Generates a set of sample barcode images in the specified folder using various image formats.
    /// </summary>
    /// <param name="folder">The directory where barcode images will be saved.</param>
    private static void GenerateSampleBarcodes(string folder)
    {
        // Sample data for barcodes
        var samples = new[]
        {
            new { Text = "Sample001", Format = BarCodeImageFormat.Png,  FileName = "barcode1.png" },
            new { Text = "Sample002", Format = BarCodeImageFormat.Jpeg, FileName = "barcode2.jpg" },
            new { Text = "Sample003", Format = BarCodeImageFormat.Bmp,  FileName = "barcode3.bmp" },
            new { Text = "Sample004", Format = BarCodeImageFormat.Png,  FileName = "barcode4.png" },
            new { Text = "Sample005", Format = BarCodeImageFormat.Jpeg, FileName = "barcode5.jpg" }
        };

        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folder, sample.FileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sample.Text))
            {
                // Optional: set some basic parameters for better readability
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Save the generated barcode image in the specified format
                generator.Save(filePath, sample.Format);
            }
        }
    }
}
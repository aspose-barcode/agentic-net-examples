using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating sample barcodes, reading them concurrently,
/// and aggregating the detection results using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, reads them in parallel,
    /// and prints aggregated detection results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary directory for storing generated barcode images.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        Directory.CreateDirectory(tempDir);

        // ---------------------------------------------------------------
        // 2. Define sample barcodes to generate (type, text, output file).
        // ---------------------------------------------------------------
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Sample128", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // ---------------------------------------------------------------
        // 3. Generate each sample barcode image and save to the temp folder.
        // ---------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempDir, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                generator.Save(filePath);
            }
        }

        // ---------------------------------------------------------------
        // 4. Retrieve all generated PNG files for processing.
        // ---------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(tempDir, "*.png");

        // ---------------------------------------------------------------
        // 5. Configure Aspose.BarCode to utilize all available CPU cores.
        // ---------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // ---------------------------------------------------------------
        // 6. Prepare a thread‑safe collection to hold aggregated results.
        // ---------------------------------------------------------------
        var aggregatedResults = new ConcurrentBag<(string file, string type, string text)>();

        // ---------------------------------------------------------------
        // 7. Set parallel execution options (max degree of parallelism).
        // ---------------------------------------------------------------
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        // ---------------------------------------------------------------
        // 8. Process each image file concurrently:
        //    - Load the image.
        //    - Read all supported barcode types.
        //    - Store each detection result in the concurrent bag.
        // ---------------------------------------------------------------
        Parallel.ForEach(imageFiles, parallelOptions, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            using (var bitmap = new Bitmap(filePath))
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    aggregatedResults.Add((filePath, result.CodeTypeName, result.CodeText));
                }
            }
        });

        // ---------------------------------------------------------------
        // 9. Output the aggregated barcode detection results to the console.
        // ---------------------------------------------------------------
        Console.WriteLine("Aggregated barcode detection results:");
        foreach (var entry in aggregatedResults)
        {
            Console.WriteLine($"File: {Path.GetFileName(entry.file)} | Type: {entry.type} | Text: {entry.text}");
        }

        // ---------------------------------------------------------------
        // 10. Optional: clean up temporary files and directory.
        // ---------------------------------------------------------------
        // Directory.Delete(tempDir, true);
    }
}
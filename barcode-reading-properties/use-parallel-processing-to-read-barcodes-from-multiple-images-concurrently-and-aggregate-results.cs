// Title: Parallel barcode reading from multiple images
// Description: Demonstrates generating several barcode images, then reading them concurrently using Aspose.BarCode's parallel processing capabilities.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeReader with ProcessorSettings for multi‑core execution. It illustrates typical use cases such as batch processing of scanned documents, inventory scans, or bulk image analysis where developers need to decode many barcodes efficiently.
// Prompt: Use parallel processing to read barcodes from multiple images concurrently and aggregate results.
// Tags: barcode symbology, parallel processing, batch recognition, aspnet, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcode images, reading them in parallel, and aggregating the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, processes them concurrently,
    /// and outputs aggregated recognition results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for sample barcode images
        string folderPath = Path.Combine(Path.GetTempPath(), "AsposeBarcodesSample");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Define sample barcodes to generate (file name, symbology, encoded text)
        var samples = new (string FileName, BaseEncodeType EncodeType, string CodeText)[]
        {
            ("code128.png", EncodeTypes.Code128, "ABC123456"),
            ("qr.png", EncodeTypes.QR, "https://example.com"),
            ("ean13.png", EncodeTypes.EAN13, "5901234123457"),
            ("datamatrix.png", EncodeTypes.DataMatrix, "DataMatrixSample"),
            ("pdf417.png", EncodeTypes.Pdf417, "PDF417 Sample Text")
        };

        // Generate barcode images and save them to the temporary folder
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folderPath, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                // Optional: customize image size, colors, etc., here
                generator.Save(filePath);
            }
        }

        // Retrieve all generated PNG files
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found.");
            return;
        }

        // Configure the barcode reader to utilize all available CPU cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Thread‑safe collection for storing aggregated recognition results
        var aggregatedResults = new ConcurrentBag<string>();

        // Parallel processing: read barcodes from each image concurrently
        Parallel.ForEach(imageFiles, file =>
        {
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    string entry = $"{Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}";
                    aggregatedResults.Add(entry);
                }
            }
        });

        // Output the aggregated results to the console
        Console.WriteLine("Aggregated barcode recognition results:");
        foreach (var line in aggregatedResults)
        {
            Console.WriteLine(line);
        }

        // Clean up temporary files (optional)
        try
        {
            foreach (var file in imageFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(folderPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
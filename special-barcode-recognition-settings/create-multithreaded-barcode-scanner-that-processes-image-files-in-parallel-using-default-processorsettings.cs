// Title: Multithreaded barcode scanning using Aspose.BarCode default settings
// Description: Demonstrates generating sample Code128 barcodes, saving them as PNG files, and scanning them concurrently with Parallel.ForEach using the default ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with default ProcessorSettings for decoding. Typical use cases include batch processing of scanned documents, automated inventory systems, and high‑throughput barcode validation where developers need to read multiple images in parallel.
// Prompt: Create a multithreaded barcode scanner that processes image files in parallel using default ProcessorSettings.
// Tags: barcode, multithreading, parallel, code128, png, generation, recognition, aspnet, aspose.barcode, processorsettings

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcode images and scanning them in parallel using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, saves them as PNG files,
    /// and processes the images concurrently to read barcode data.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Prepare a folder for sample barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folderPath);

        // --------------------------------------------------------------------
        // Define sample texts to encode as Code128 barcodes
        // --------------------------------------------------------------------
        string[] sampleTexts = { "ABC123", "XYZ789", "123456", "HELLO", "WORLD" };

        // --------------------------------------------------------------------
        // Generate barcode images (PNG) using default generator settings
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Retrieve all PNG files in the folder for processing
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");

        // --------------------------------------------------------------------
        // Process images in parallel using default ProcessorSettings
        // --------------------------------------------------------------------
        Parallel.ForEach(imageFiles, file =>
        {
            // Verify the file still exists (it may have been removed concurrently)
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                return;
            }

            // Create a reader instance for each file
            using (var reader = new BarCodeReader())
            {
                // Use all supported decode types (default ProcessorSettings are applied automatically)
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                // Load the image for recognition
                reader.SetBarCodeImage(file);

                // Read and output all detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        });
    }
}
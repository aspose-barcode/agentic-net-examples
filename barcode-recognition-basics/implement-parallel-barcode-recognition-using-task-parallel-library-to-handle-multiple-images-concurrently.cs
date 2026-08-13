// Title: Parallel barcode recognition using TPL
// Description: Demonstrates generating sample barcodes and recognizing them concurrently across multiple images.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, showcasing how to use BarCodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings for high‑performance parallel recognition. Typical use cases include batch processing of scanned documents, inventory systems, and automated data entry where many images must be decoded quickly. Developers often need to leverage the Task Parallel Library together with Aspose.BarCode APIs to maximize CPU utilization.
// Prompt: Implement parallel barcode recognition using Task Parallel Library to handle multiple images concurrently.
// Tags: barcode, parallel, tpl, recognition, generation, aspnet, aspose.barcode, multithreading

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates several barcode images and then reads them in parallel
/// using the Task Parallel Library (TPL). Demonstrates high‑performance batch barcode
/// recognition with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, configures parallel
    /// processing, and reads all generated images concurrently.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Prepare a folder for sample barcode images
        // --------------------------------------------------------------------
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // --------------------------------------------------------------------
        // 2. Define sample barcodes to generate (type, text, file name)
        // --------------------------------------------------------------------
        var samples = new (BaseEncodeType type, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "Hello QR", "qr.png"),
            (EncodeTypes.DataMatrix, "DM123", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample", "pdf417.png"),
            (EncodeTypes.Aztec, "Aztec", "aztec.png")
        };

        // --------------------------------------------------------------------
        // 3. Generate barcode images and save them to the folder
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                // Optional: set a modest XDimension for better visibility
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(filePath);
            }
        }

        // --------------------------------------------------------------------
        // 4. Configure processor settings to use all available CPU cores
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // 5. Collect image file paths for later processing
        // --------------------------------------------------------------------
        var imageFiles = new List<string>();
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folder, sample.fileName);
            if (File.Exists(filePath))
            {
                imageFiles.Add(filePath);
            }
        }

        // --------------------------------------------------------------------
        // 6. Process images in parallel using TPL
        // --------------------------------------------------------------------
        var tasks = new List<Task>();
        foreach (string imagePath in imageFiles)
        {
            tasks.Add(Task.Run(() =>
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"{Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }));
        }

        // --------------------------------------------------------------------
        // 7. Wait for all recognition tasks to complete
        // --------------------------------------------------------------------
        Task.WaitAll(tasks.ToArray());

        // --------------------------------------------------------------------
        // 8. Cleanup (optional): delete the generated images
        // --------------------------------------------------------------------
        // foreach (var file in imageFiles) { File.Delete(file); }
    }
}
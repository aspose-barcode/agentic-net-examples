using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of various postal barcodes using Aspose.BarCode
/// and optional verification by reading a generated barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Represents a single barcode generation request.
    /// </summary>
    private class BarcodeTask
    {
        public BaseEncodeType Symbology { get; }
        public string CodeText { get; }
        public string FileName { get; }

        public BarcodeTask(BaseEncodeType symbology, string codeText, string fileName)
        {
            Symbology = symbology;
            CodeText = codeText;
            FileName = fileName;
        }
    }

    /// <summary>
    /// Entry point of the application. Generates a set of postal barcodes in parallel
    /// and optionally reads back one of them for verification.
    /// </summary>
    static void Main()
    {
        // Determine output folder for generated barcode images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not exist.
            Directory.CreateDirectory(outputFolder);
        }

        // Prepare a small batch of postal barcode tasks (safe sample size).
        var tasks = new List<BarcodeTask>
        {
            // Postnet (US postal barcode) – 5-digit ZIP code.
            new BarcodeTask(EncodeTypes.Postnet, "12345", "postnet.png"),
            // Planet (US postal barcode) – 5-digit ZIP code.
            new BarcodeTask(EncodeTypes.Planet, "54321", "planet.png"),
            // AustraliaPost – sample customer information.
            new BarcodeTask(EncodeTypes.AustraliaPost, "5912345678ABCde", "australiapost.png"),
            // OneCode (USPS) – numeric string (20 digits).
            new BarcodeTask(EncodeTypes.OneCode, "12345678901234567890", "onecode.png"),
            // RM4SCC (UK Royal Mail) – sample alphanumeric.
            new BarcodeTask(EncodeTypes.RM4SCC, "AB12C3", "rm4scc.png")
        };

        // Parallel generation – each iteration creates its own BarcodeGenerator (thread‑safe).
        Parallel.ForEach(tasks, task =>
        {
            // Build full path for the output image.
            string outputPath = Path.Combine(outputFolder, task.FileName);

            // Each thread gets its own generator instance.
            using (var generator = new BarcodeGenerator(task.Symbology, task.CodeText))
            {
                // Optional: set a higher resolution for better quality.
                generator.Parameters.Resolution = 300f;

                // Save the barcode image (PNG by default).
                generator.Save(outputPath);
                Console.WriteLine($"Generated {task.FileName}");
            }
        });

        // Demonstrate reading back one of the generated barcodes (optional verification).
        string samplePath = Path.Combine(outputFolder, "postnet.png");
        if (File.Exists(samplePath))
        {
            // Initialize a reader for the specific barcode type.
            using (var reader = new BarCodeReader(samplePath, DecodeType.Postnet))
            {
                // Iterate through all decoded results (normally one per image).
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Read from {Path.GetFileName(samplePath)}: {result.CodeText}");
                }
            }
        }

        Console.WriteLine("Barcode batch generation completed.");
    }
}
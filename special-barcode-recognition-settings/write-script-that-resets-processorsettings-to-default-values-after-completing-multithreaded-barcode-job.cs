// Title: Multithreaded barcode generation, recognition, and processor settings reset
// Description: Demonstrates generating barcode images, reading them using multithreaded processing, and resetting ProcessorSettings to defaults.
// Category-Description: This example belongs to the Aspose.BarCode multithreading and processor configuration category. It showcases the use of BarCodeGenerator for creating barcodes, BarCodeReader with ProcessorSettings for parallel recognition, and typical cleanup steps. Developers working with high‑throughput barcode scanning often need to adjust core usage and thread limits, then restore defaults after a job.
// Prompt: Write a script that resets ProcessorSettings to default values after completing a multithreaded barcode job.
// Tags: barcode generation, barcode recognition, multithreading, processorsettings, code128, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, multithreaded recognition, and resetting processor settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, reads them using all CPU cores, and then restores default ProcessorSettings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the job
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeJob_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        for (int i = 0; i < 5; i++)
        {
            string codeText = $"Sample{i}";
            string filePath = Path.Combine(tempFolder, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional appearance settings
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
                generator.Parameters.Border.Visible = true;
                generator.Parameters.Border.Width.Point = 1f;
                generator.Parameters.Border.Color = Aspose.Drawing.Color.Black;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Configure multithreaded recognition settings
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        Console.WriteLine("Starting multithreaded barcode reading...");

        // Read each generated barcode
        foreach (string file in Directory.GetFiles(tempFolder, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                Stopwatch watch = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                watch.Stop();

                Console.WriteLine($"File: {Path.GetFileName(file)} - Found {results.Length} barcode(s) in {watch.ElapsedMilliseconds} ms");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Reset ProcessorSettings to default values
        BarCodeReader.ProcessorSettings.UseAllCores = true; // default is true
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0; // default is 0 (no forced count)
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0; // default is 0

        Console.WriteLine("ProcessorSettings have been reset to default values.");

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – not critical for the example
        }
    }
}
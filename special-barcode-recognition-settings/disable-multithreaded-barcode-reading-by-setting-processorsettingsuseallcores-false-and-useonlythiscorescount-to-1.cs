// Title: Disable Multithreaded Barcode Reading Example
// Description: Demonstrates generating a Code128 barcode, configuring Aspose.BarCode to use a single CPU core for reading, and decoding the barcode.
// Category-Description: This example belongs to the Aspose.BarCode reading category, illustrating how to control processor usage via BarCodeReader.ProcessorSettings. It covers key classes such as BarcodeGenerator, BarCodeReader, and ProcessorSettings, which developers commonly use to optimize performance, limit resource consumption, or ensure deterministic single‑threaded execution in environments where multithreading is undesirable.
// Prompt: Disable multithreaded barcode reading by setting ProcessorSettings.UseAllCores false and UseOnlyThisCoresCount to 1.
// Tags: barcode symbology, reading, single-threaded, processor settings, aspose.barcode, code128, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates disabling multithreaded barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, configures single‑threaded reading,
    /// decodes the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and save it to a PNG file
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Disable multithreaded barcode reading (force single‑core usage)
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // --------------------------------------------------------------------
        // Read the barcode using the single‑threaded settings
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            Stopwatch watch = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            watch.Stop();

            Console.WriteLine($"Found {results.Length} barcode(s) in {watch.ElapsedMilliseconds} ms using single-threaded mode.");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}
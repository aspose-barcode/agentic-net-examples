// Title: Disable Multithreaded Barcode Reading Example
// Description: Demonstrates how to turn off multithreaded barcode recognition by configuring ProcessorSettings to use a single CPU core.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the use of BarCodeReader and its ProcessorSettings to control threading behavior. Developers often need to limit CPU usage for barcode recognition in constrained environments or to achieve deterministic performance. The key API classes include BarCodeReader, ProcessorSettings, and BarcodeGenerator, which are commonly used for generating and decoding barcodes in .NET applications.
// Prompt: Disable multithreaded barcode reading by setting ProcessorSettings.UseAllCores false and UseOnlyThisCoresCount to 1.
// Tags: barcode, code128, reading, multithreading, processorsettings, aspose.barcode, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, disables multithreaded reading,
/// and reads the barcode using a single CPU core.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, configures single‑threaded
    /// processing, reads the barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Disable multithreaded reading: configure ProcessorSettings to use only one core
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Read the barcode using the single‑thread settings
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            Stopwatch watch = Stopwatch.StartNew(); // Start timing the recognition
            BarCodeResult[] results = reader.ReadBarCodes();
            watch.Stop(); // Stop timing

            Console.WriteLine($"Barcodes read: {results.Length}, Recognition time: {watch.ElapsedMilliseconds} ms");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}
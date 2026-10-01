// Title: Code39 Barcode Generation and Recognition Speed Measurement
// Description: Demonstrates generating a Code39 barcode, saving it as a PNG file, and measuring the time taken for both generation and recognition.
// Category-Description: This example belongs to the Aspose.BarCode performance benchmarking category. It showcases the use of core API classes such as BarcodeGenerator for barcode creation and BarCodeReader for barcode detection. Developers often need to evaluate processing speed when selecting symbologies or optimizing workflows, making this pattern useful for performance testing and comparative analysis.
// Prompt: Configure the library to prioritize Code39 symbology and measure any change in overall processing speed.
// Tags: barcode symbology, code39, performance, generation, recognition, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code39 barcode, saving it, and measuring generation and recognition performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code39 barcode, measures generation and recognition times, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode content and output file path
        string codeText = "123ABC";
        string barcodePath = Path.Combine(tempFolder, "code39.png");

        // -------------------- Generation Phase --------------------
        // Measure the time required to generate and save the barcode image
        Stopwatch genWatch = new Stopwatch();
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            genWatch.Start();
            generator.Save(barcodePath, BarCodeImageFormat.Png);
            genWatch.Stop();
        }

        Console.WriteLine($"Barcode generation time: {genWatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // -------------------- Recognition Phase --------------------
        // Measure the time required to read and decode the saved barcode
        Stopwatch recWatch = new Stopwatch();
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            recWatch.Start();
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Recognized Code: {result.CodeText}");
            }
            recWatch.Stop();
        }

        Console.WriteLine($"Barcode recognition time: {recWatch.ElapsedMilliseconds} ms");

        // -------------------- Cleanup Phase --------------------
        // Delete the generated files and temporary directory, handling any errors gracefully
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }
}
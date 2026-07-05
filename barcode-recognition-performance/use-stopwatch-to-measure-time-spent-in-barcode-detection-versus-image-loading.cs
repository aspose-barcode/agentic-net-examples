// Title: Measure barcode detection vs image loading time
// Description: Demonstrates loading a barcode image, generating it if missing, and using Stopwatch to compare the time spent loading the image versus detecting the barcode.
// Prompt: Use a Stopwatch to measure time spent in barcode detection versus image loading.
// Tags: barcode symbology, detection, timing, stopwatch, aspose.barcode, image loading

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that measures and compares the time required to load a barcode image
/// and to detect the barcode within that image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode image if it does not exist,
    /// then measures image loading time and barcode detection time using Stopwatch.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Define the path for the sample barcode image
        // ------------------------------------------------------------
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample.png");

        // ------------------------------------------------------------
        // Ensure the barcode image exists; generate it if missing
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Create a Code128 barcode with sample data and save it as PNG
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Measure the time taken to load the image from disk into memory
        // ------------------------------------------------------------
        var loadTimer = Stopwatch.StartNew();
        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            using (var bitmap = new Bitmap(fs))
            {
                // Image is loaded; no additional processing required for timing
            }
        }
        loadTimer.Stop();
        Console.WriteLine($"Image loading time: {loadTimer.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Measure the time taken to detect barcodes in the loaded image
        // ------------------------------------------------------------
        var detectTimer = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
        detectTimer.Stop();
        Console.WriteLine($"Barcode detection time: {detectTimer.ElapsedMilliseconds} ms");
    }
}
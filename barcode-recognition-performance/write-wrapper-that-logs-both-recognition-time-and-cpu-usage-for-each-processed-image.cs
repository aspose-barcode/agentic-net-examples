using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image for the specified type and text, returning a MemoryStream with PNG data.
    /// </summary>
    /// <param name="type">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A MemoryStream positioned at the beginning containing the PNG image.</returns>
    static MemoryStream GenerateBarcodeImage(BaseEncodeType type, string codeText)
    {
        // Create a memory stream to hold the generated image.
        var ms = new MemoryStream();

        // Use BarcodeGenerator with default settings to create the barcode.
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Save the barcode as PNG into the memory stream.
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset stream position so it can be read from the start.
        ms.Position = 0;
        return ms;
    }

    /// <summary>
    /// Entry point of the application. Generates sample barcodes, recognizes them, and logs performance metrics.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample barcodes with their types and texts.
        var samples = new List<(BaseEncodeType Type, string Text)>
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixTest")
        };

        // Process each sample barcode.
        foreach (var sample in samples)
        {
            // Generate the barcode image and obtain a stream for recognition.
            using (var imageStream = GenerateBarcodeImage(sample.Type, sample.Text))
            {
                // Capture CPU time before recognition.
                Process process = Process.GetCurrentProcess();
                TimeSpan cpuBefore = process.TotalProcessorTime;

                // Start a stopwatch to measure wall‑clock time.
                var stopwatch = Stopwatch.StartNew();

                // Recognize the barcode from the image stream.
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output the detected barcode type and decoded text.
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                    }
                }

                // Stop timing measurements.
                stopwatch.Stop();
                TimeSpan cpuAfter = process.TotalProcessorTime;

                // Log the symbology and performance data.
                Console.WriteLine($"Barcode Symbology: {sample.Type.TypeName}");
                Console.WriteLine($"Recognition Time (ms): {stopwatch.ElapsedMilliseconds}");
                Console.WriteLine($"CPU Time Used (ms): {(cpuAfter - cpuBefore).TotalMilliseconds}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
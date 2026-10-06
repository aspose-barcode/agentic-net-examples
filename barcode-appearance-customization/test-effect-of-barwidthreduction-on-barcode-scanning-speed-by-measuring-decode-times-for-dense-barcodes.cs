// Title: BarWidthReduction Impact on Barcode Decode Speed
// Description: Demonstrates how adjusting BarWidthReduction influences the time required to decode dense barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes with specific visual parameters and BarCodeReader for decoding them. Developers often need to fine‑tune barcode appearance (e.g., BarWidthReduction) and assess its effect on scanning performance, especially for high‑density symbologies.
// Prompt: Test the effect of BarWidthReduction on barcode scanning speed by measuring decode times for dense barcodes.
// Tags: barcode, barwidthreduction, performance, generation, recognition, aspose.barcode, code128, datamatrix, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides an example that measures how BarWidthReduction affects barcode decoding time
/// for dense Code128 and DataMatrix barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes with different BarWidthReduction values,
    /// decodes them, and outputs the elapsed decode time for each case.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "BarWidthReductionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define symbologies and corresponding dense text payloads
        var symbologies = new (BaseEncodeType Encode, string Text)[]
        {
            (EncodeTypes.Code128, new string('A', 30)),      // 30 characters of 'A' for Code128
            (EncodeTypes.DataMatrix, new string('B', 50))   // 50 characters of 'B' for DataMatrix
        };

        // Bar width reduction values to test (in pixels)
        float[] reductions = { 0f, 4f };

        // Iterate over each symbology and reduction setting
        foreach (var (encode, text) in symbologies)
        {
            foreach (float reduction in reductions)
            {
                // Build file name and full path for the generated image
                string fileName = $"{encode}_{reduction}pix.png";
                string filePath = Path.Combine(tempDir, fileName);

                // Generate barcode with the specified BarWidthReduction
                using (var generator = new BarcodeGenerator(encode, text))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;               // Set base module width
                    generator.Parameters.Barcode.BarWidthReduction.Pixels = reduction; // Apply reduction
                    generator.Save(filePath, BarCodeImageFormat.Png);                  // Save as PNG
                }

                // Measure the time required to decode the generated barcode
                long elapsedMs;
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var sw = Stopwatch.StartNew();
                    var results = reader.ReadBarCodes(); // Perform decoding
                    sw.Stop();
                    elapsedMs = sw.ElapsedMilliseconds;

                    // Output decoding statistics
                    Console.WriteLine($"Symbology: {encode}, Reduction: {reduction} px, DecodeTime: {elapsedMs} ms, Detected: {results.Length}");
                }
            }
        }

        // Attempt to clean up the temporary directory; ignore any errors
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Cleanup errors are non‑critical for this example
        }
    }
}
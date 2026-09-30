// Title: Measure XML export performance for large barcode configurations
// Description: Demonstrates how to benchmark file‑based versus stream‑based XML export of barcode configurations using Aspose.BarCode. It creates several large barcode generators, exports them to XML, and reports elapsed time.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating the use of BarcodeGenerator, ExportToXml, and related parameter settings. Developers often need to compare file I/O and memory‑stream approaches when handling bulk barcode data, especially for large configurations, to choose the most efficient method for their applications. The snippet shows typical setup, export, and cleanup steps.
// Prompt: Measure performance differences between file‑based and stream‑based XML export for large barcode configurations.
// Tags: barcode, performance, xml export, file io, memory stream, code128, aspnet, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates measuring performance of file‑based vs stream‑based XML export for large barcode configurations using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the benchmark and outputs timing results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for file‑based XML exports
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlPerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode texts (large configurations) to increase export payload size
        List<string> codeTexts = new List<string>
        {
            new string('A', 200),
            new string('B', 250),
            new string('C', 300),
            new string('D', 350),
            new string('E', 400)
        };

        // ------------------------------
        // Measure file‑based export time
        // ------------------------------
        Stopwatch fileSw = Stopwatch.StartNew();
        for (int i = 0; i < codeTexts.Count; i++)
        {
            using (var generator = CreateGenerator(codeTexts[i]))
            {
                string filePath = Path.Combine(tempFolder, $"barcode_{i}.xml");
                generator.ExportToXml(filePath); // Export directly to a file
            }
        }
        fileSw.Stop();

        // -------------------------------
        // Measure stream‑based export time
        // -------------------------------
        Stopwatch streamSw = Stopwatch.StartNew();
        for (int i = 0; i < codeTexts.Count; i++)
        {
            using (var generator = CreateGenerator(codeTexts[i]))
            {
                using (var ms = new MemoryStream())
                {
                    generator.ExportToXml(ms); // Export to an in‑memory stream
                    // Position reset not required for timing; kept for completeness
                }
            }
        }
        streamSw.Stop();

        // Output benchmark results
        Console.WriteLine($"File‑based XML export time for {codeTexts.Count} items: {fileSw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Stream‑based XML export time for {codeTexts.Count} items: {streamSw.ElapsedMilliseconds} ms");

        // Clean up temporary files and folder
        try
        {
            foreach (var file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect benchmark result
        }
    }

    // Helper to create a barcode generator with a relatively complex configuration
    private static BarcodeGenerator CreateGenerator(string codeText)
    {
        var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText);

        // Example of setting various parameters to increase configuration size
        generator.Parameters.Barcode.XDimension.Point = 2f;
        generator.Parameters.Barcode.BarHeight.Point = 30f;
        generator.Parameters.Barcode.Padding.Left.Point = 5f;
        generator.Parameters.Barcode.Padding.Top.Point = 5f;
        generator.Parameters.Barcode.Padding.Right.Point = 5f;
        generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
        generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
        generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
        generator.Parameters.Barcode.FilledBars = false;
        generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

        return generator;
    }
}
// Title: XML Export Performance Comparison for Barcode Configurations
// Description: Demonstrates measuring and comparing the time taken to export a large barcode configuration to XML using file‑based and stream‑based approaches.
// Prompt: Measure performance differences between file‑based and stream‑based XML export for large barcode configurations.
// Tags: barcode symbology, export, xml, performance, file, stream, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that benchmarks file‑based vs stream‑based XML export of a barcode configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode generator with a complex configuration, exports it to XML via file and memory stream,
    /// measures execution time for each method, and outputs the results.
    /// </summary>
    static void Main(string[] args)
    {
        // Initialize a barcode generator with a relatively complex configuration
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample1234567890"))
        {
            // Configure various parameters to simulate a large configuration
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 300;
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // ------------------------------
            // Measure file‑based XML export
            // ------------------------------
            var stopwatch = Stopwatch.StartNew();
            bool fileExportSuccess = generator.ExportToXml("barcode_config.xml");
            stopwatch.Stop();
            long fileExportMs = stopwatch.ElapsedMilliseconds;

            // -------------------------------
            // Measure stream‑based XML export
            // -------------------------------
            bool streamExportSuccess;
            long streamExportMs;
            using (var memoryStream = new MemoryStream())
            {
                stopwatch.Restart();
                streamExportSuccess = generator.ExportToXml(memoryStream);
                stopwatch.Stop();
                streamExportMs = stopwatch.ElapsedMilliseconds;

                // Reset stream position and import back to verify correctness
                memoryStream.Position = 0;
                var importedGenerator = BarcodeGenerator.ImportFromXml(memoryStream);
                // Dispose imported generator (it implements IDisposable)
                importedGenerator.Dispose();
            }

            // Output the benchmark results
            Console.WriteLine($"File export:   {(fileExportSuccess ? "Success" : "Failed")} in {fileExportMs} ms");
            Console.WriteLine($"Stream export: {(streamExportSuccess ? "Success" : "Failed")} in {streamExportMs} ms");
        }
    }
}
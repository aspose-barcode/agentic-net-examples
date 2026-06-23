using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates performance measurement of Aspose.BarCode ExportToXml using file path and stream overloads.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcode samples and measures the time taken
    /// to export them to XML via file path and memory stream overloads.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Number of barcode samples (small safe size for the runner)
        const int sampleCount = 5;

        // Temporary directory for XML files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExport");
        Directory.CreateDirectory(tempDir);

        // ------------------------------------------------------------
        // Measure ExportToXml using the file‑path overload
        // ------------------------------------------------------------
        Stopwatch fileTimer = Stopwatch.StartNew();
        for (int i = 0; i < sampleCount; i++)
        {
            // Create a barcode generator for each sample
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Build the full path for the XML file
                string xmlPath = Path.Combine(tempDir, $"barcode{i}.xml");
                // Export the barcode definition to the specified file
                generator.ExportToXml(xmlPath);
            }
        }
        fileTimer.Stop();

        // ------------------------------------------------------------
        // Measure ExportToXml using the stream overload
        // ------------------------------------------------------------
        Stopwatch streamTimer = Stopwatch.StartNew();
        for (int i = 0; i < sampleCount; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Export to a memory stream (no file system I/O)
                using (var ms = new MemoryStream())
                {
                    generator.ExportToXml(ms);
                }
            }
        }
        streamTimer.Stop();

        // Output timing results
        Console.WriteLine($"Export to file path total time: {fileTimer.ElapsedMilliseconds} ms");
        Console.WriteLine($"Export to stream total time: {streamTimer.ElapsedMilliseconds} ms");

        // ------------------------------------------------------------
        // Cleanup temporary files and directory
        // ------------------------------------------------------------
        try
        {
            // Delete each generated XML file
            foreach (var file in Directory.GetFiles(tempDir, "*.xml"))
                File.Delete(file);

            // Remove the temporary directory itself
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup is best effort.
        }
    }
}

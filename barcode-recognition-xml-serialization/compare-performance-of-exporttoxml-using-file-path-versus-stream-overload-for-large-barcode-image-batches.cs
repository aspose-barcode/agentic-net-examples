// Title: ExportToXml Performance Comparison: File Path vs Stream
// Description: Demonstrates measuring the execution time of Aspose.BarCode's ExportToXml method when writing to a file versus a memory stream for a batch of barcode images.
// Prompt: Compare performance of ExportToXml using file path versus stream overload for large barcode image batches.
// Tags: code128, export, xml, performance, aspose.barcode, stream, file

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple benchmark that compares the time required to export barcode data to XML
/// using the file‑path overload versus the stream overload of <c>BarcodeGenerator.ExportToXml</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a small set of barcodes, exports each to XML
    /// using both overloads, records the elapsed time, and prints a side‑by‑side comparison.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for XML files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExportDemo");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Sample barcode texts (small batch for safe execution)
        List<string> sampleTexts = new List<string>
        {
            "ABC123456",
            "9876543210",
            "TestCode128",
            "12345ABCDE",
            "ZXCVBNM123"
        };

        // Store timing results for each overload
        List<TimeSpan> fileTimes = new List<TimeSpan>();
        List<TimeSpan> streamTimes = new List<TimeSpan>();

        // Iterate over each barcode text
        for (int i = 0; i < sampleTexts.Count; i++)
        {
            string codeText = sampleTexts[i];

            // Create a BarcodeGenerator instance for the current text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Export to XML file and measure time
                string xmlFilePath = Path.Combine(tempDir, $"barcode_{i}.xml");
                Stopwatch swFile = Stopwatch.StartNew();
                bool fileResult = generator.ExportToXml(xmlFilePath);
                swFile.Stop();
                fileTimes.Add(swFile.Elapsed);

                // Export to XML stream and measure time
                using (MemoryStream ms = new MemoryStream())
                {
                    Stopwatch swStream = Stopwatch.StartNew();
                    bool streamResult = generator.ExportToXml(ms);
                    swStream.Stop();
                    streamTimes.Add(swStream.Elapsed);
                }

                // Optional: verify export success (not required for timing)
                if (!fileResult)
                {
                    Console.WriteLine($"Export to file failed for index {i}.");
                }
            }
        }

        // Output timing comparison
        Console.WriteLine("Performance comparison of ExportToXml (file path vs stream):");
        for (int i = 0; i < sampleTexts.Count; i++)
        {
            Console.WriteLine($"Item {i + 1}: File = {fileTimes[i].TotalMilliseconds} ms, Stream = {streamTimes[i].TotalMilliseconds} ms");
        }

        // Clean up temporary XML files
        try
        {
            foreach (string file in Directory.GetFiles(tempDir, "*.xml"))
            {
                File.Delete(file);
            }
            Directory.Delete(tempDir);
        }
        catch
        {
            // If cleanup fails, ignore – not critical for the demo
        }
    }
}
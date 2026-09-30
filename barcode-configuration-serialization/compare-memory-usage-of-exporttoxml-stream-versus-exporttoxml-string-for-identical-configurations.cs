// Title: Compare memory usage of ExportToXml with file path vs MemoryStream
// Description: Demonstrates how to measure and compare the memory consumption of Aspose.BarCode's ExportToXml method when exporting to a file path versus a MemoryStream, using identical barcode configurations.
// Category-Description: This example belongs to the Aspose.BarCode export operations category, illustrating the use of BarcodeGenerator and its ExportToXml API. It shows typical scenarios where developers need to persist barcode settings as XML, either to disk or in-memory, and how to assess memory impact. Common use cases include configuration backup, diagnostics, and integration with services that require XML payloads.
// Prompt: Compare memory usage of ExportToXml(Stream) versus ExportToXml(string) for identical configurations.
// Tags: barcode, export, xml, memory-usage, code128, aspose.barcode, stream, file

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring memory usage of ExportToXml using a file path and a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures a barcode generator, exports its configuration to XML via file and stream,
    /// and reports the memory consumption of each approach.
    /// </summary>
    static void Main()
    {
        // Prepare a barcode generator with a sample configuration
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "1234567890";

        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example configuration: set X dimension and bar color
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Ensure any previous allocations are collected before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // -------------------------------------------------
            // Export to XML using a file path (disk storage)
            // -------------------------------------------------
            string tempFilePath = Path.Combine(Path.GetTempPath(), "barcode_config_path.xml");
            // Remove the file if it already exists to ensure a clean test
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);

            long beforeFileExport = GC.GetTotalMemory(true);
            generator.ExportToXml(tempFilePath);
            long afterFileExport = GC.GetTotalMemory(true);
            long fileExportMemory = afterFileExport - beforeFileExport;

            // -------------------------------------------------
            // Export to XML using a MemoryStream (in‑memory)
            // -------------------------------------------------
            using (var memoryStream = new MemoryStream())
            {
                // Reset GC before measuring the stream export
                GC.Collect();
                GC.WaitForPendingFinalizers();

                long beforeStreamExport = GC.GetTotalMemory(true);
                generator.ExportToXml(memoryStream);
                long afterStreamExport = GC.GetTotalMemory(true);
                long streamExportMemory = afterStreamExport - beforeStreamExport;

                // Output the memory usage comparison
                Console.WriteLine($"Memory used by ExportToXml(string path): {fileExportMemory} bytes");
                Console.WriteLine($"Memory used by ExportToXml(Stream): {streamExportMemory} bytes");
                Console.WriteLine($"Difference (path - stream): {fileExportMemory - streamExportMemory} bytes");

                // Optional: verify that the XML was written to the stream
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream, leaveOpen: true))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine($"XML length from stream: {xmlContent.Length} characters");
                }
            }

            // Clean up the temporary file after the test
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
        }
    }
}
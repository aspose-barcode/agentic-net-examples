// Title: Compare memory usage of ExportToXml(Stream) vs ExportToXml(string)
// Description: Demonstrates how to measure and compare the memory consumption of Aspose.BarCode's ExportToXml method when using a Stream versus a file path.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category, illustrating the use of BarcodeGenerator and its ExportToXml API. Developers often need to persist barcode settings to XML for later reuse, and choosing between stream or file output can impact memory usage. The snippet shows typical patterns for measuring memory impact in .NET applications.
// Prompt: Compare memory usage of ExportToXml(Stream) versus ExportToXml(string) for identical configurations.
// Tags: barcode symbology, export, xml, memory usage, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that compares the memory usage of ExportToXml when writing to a <see cref="Stream"/>
/// versus writing directly to a file path, using identical barcode configurations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode generator, exports its configuration to XML
    /// using both a memory stream and a temporary file, and reports the memory consumption of each approach.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Adjust a non‑default parameter to ensure the configuration is not the default state.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // -------------------- Measure memory for ExportToXml(Stream) --------------------
            // Force a full garbage collection to get a clean baseline.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memoryBeforeStream = GC.GetTotalMemory(true);

            // Export the configuration to a memory stream.
            using (var memoryStream = new MemoryStream())
            {
                bool streamResult = generator.ExportToXml(memoryStream);
                Console.WriteLine($"ExportToXml(Stream) succeeded: {streamResult}");
            }

            // Capture memory after the stream export.
            long memoryAfterStream = GC.GetTotalMemory(true);
            long memoryUsedStream = memoryAfterStream - memoryBeforeStream;

            // -------------------- Measure memory for ExportToXml(string) --------------------
            // Force another garbage collection before the second measurement.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memoryBeforeFile = GC.GetTotalMemory(true);

            // Define a temporary file path for the XML output.
            string tempFilePath = Path.Combine(Path.GetTempPath(), "barcode_config.xml");

            // Export the configuration directly to a file.
            bool fileResult = generator.ExportToXml(tempFilePath);
            Console.WriteLine($"ExportToXml(string) succeeded: {fileResult}");

            // Capture memory after the file export.
            long memoryAfterFile = GC.GetTotalMemory(true);
            long memoryUsedFile = memoryAfterFile - memoryBeforeFile;

            // -------------------- Output comparison results --------------------
            Console.WriteLine($"Memory used by ExportToXml(Stream): {memoryUsedStream} bytes");
            Console.WriteLine($"Memory used by ExportToXml(string): {memoryUsedFile} bytes");

            // Clean up the temporary XML file.
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }
}
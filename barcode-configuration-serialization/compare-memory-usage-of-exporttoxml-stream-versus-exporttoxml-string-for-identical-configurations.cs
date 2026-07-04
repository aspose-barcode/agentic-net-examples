// Title: Memory Usage Comparison of ExportToXml Overloads
// Description: Demonstrates how to compare memory consumption when exporting barcode configuration to XML using a file path versus a stream.
// Prompt: Compare memory usage of ExportToXml(Stream) versus ExportToXml(string) for identical configurations.
// Tags: barcode, export, xml, memory, aspose.barcode, stream, file

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample program that measures and compares the memory allocation of
/// <c>BarcodeGenerator.ExportToXml</c> when using a file path versus a stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Configures a barcode generator, exports its configuration
    /// to XML using both overloads, and reports the memory used by each operation.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with a sample symbology and value.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply non‑default visual settings to make the configuration meaningful.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.Resolution = 150;

            // Force a clean GC state before the first measurement.
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // -------------------- ExportToXml(string) --------------------
            // Record memory before the export.
            long beforeString = GC.GetTotalMemory(true);
            // Export configuration to a physical XML file.
            bool resultString = generator.ExportToXml("barcode_config.xml");
            // Record memory after the export.
            long afterString = GC.GetTotalMemory(true);
            // Calculate the memory delta.
            long diffString = afterString - beforeString;

            // Output the result and memory usage for the string overload.
            Console.WriteLine($"ExportToXml(string) succeeded: {resultString}");
            Console.WriteLine($"Memory allocated (bytes) for ExportToXml(string): {diffString}");

            // -------------------- ExportToXml(Stream) --------------------
            // Use a memory stream to capture the XML output in memory.
            using (var memoryStream = new MemoryStream())
            {
                // Clean up before the second measurement.
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Record memory before the stream export.
                long beforeStream = GC.GetTotalMemory(true);
                // Export configuration to the provided stream.
                bool resultStream = generator.ExportToXml(memoryStream);
                // Record memory after the export.
                long afterStream = GC.GetTotalMemory(true);
                // Calculate the memory delta.
                long diffStream = afterStream - beforeStream;

                // Output the result and memory usage for the stream overload.
                Console.WriteLine($"ExportToXml(Stream) succeeded: {resultStream}");
                Console.WriteLine($"Memory allocated (bytes) for ExportToXml(Stream): {diffStream}");
            }

            // Clean up the temporary file created by ExportToXml(string).
            try
            {
                if (File.Exists("barcode_config.xml"))
                {
                    File.Delete("barcode_config.xml");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete temporary file: {ex.Message}");
            }
        }
    }
}
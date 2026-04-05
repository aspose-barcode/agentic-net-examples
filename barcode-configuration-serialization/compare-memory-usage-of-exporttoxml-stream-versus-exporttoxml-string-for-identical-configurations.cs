using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare a temporary file path for the string-based export
        string tempXmlPath = Path.Combine(Path.GetTempPath(), "barcode_config.xml");

        // Create a barcode generator with some configuration
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";
            // Example of setting an additional parameter
            generator.Parameters.Barcode.Code128.Code128EncodeMode = Code128EncodeMode.Auto;

            // Measure memory usage for ExportToXml(string)
            long beforeStringExport = GC.GetTotalMemory(true);
            bool stringExportResult = generator.ExportToXml(tempXmlPath);
            long afterStringExport = GC.GetTotalMemory(true);
            long stringExportMemory = afterStringExport - beforeStringExport;

            // Measure memory usage for ExportToXml(Stream)
            long beforeStreamExport = GC.GetTotalMemory(true);
            bool streamExportResult;
            using (var memoryStream = new MemoryStream())
            {
                streamExportResult = generator.ExportToXml(memoryStream);
                // Ensure the stream is flushed (not strictly necessary for memory measurement)
                memoryStream.Flush();
            }
            long afterStreamExport = GC.GetTotalMemory(true);
            long streamExportMemory = afterStreamExport - beforeStreamExport;

            // Output the results
            Console.WriteLine($"ExportToXml(string) succeeded: {stringExportResult}");
            Console.WriteLine($"Memory used (bytes) by ExportToXml(string): {stringExportMemory}");

            Console.WriteLine($"ExportToXml(Stream) succeeded: {streamExportResult}");
            Console.WriteLine($"Memory used (bytes) by ExportToXml(Stream): {streamExportMemory}");
        }

        // Clean up the temporary file
        if (File.Exists(tempXmlPath))
        {
            try { File.Delete(tempXmlPath); } catch { /* ignore cleanup errors */ }
        }
    }
}
// Title: Compare memory usage of ExportToXml with file path vs. stream
// Description: Demonstrates measuring memory consumption when exporting a barcode configuration to XML using a file path and a memory stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to export barcode settings to XML. It highlights typical scenarios where developers need to persist barcode configurations either to a file or an in‑memory stream and want to understand the memory impact of each approach. The example uses ExportToXml(string) and ExportToXml(Stream) methods, common in reporting, logging, or configuration‑export workflows.
// Prompt: Compare memory usage of ExportToXml(Stream) versus ExportToXml(string) for identical configurations.
// Tags: barcode, export, xml, memory, stream, file, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple console application that measures and compares the memory usage
/// of the <c>ExportToXml</c> method when writing to a file path versus a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes two export scenarios and reports the
    /// memory increase observed for each case.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the XML file output
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlMemTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string xmlFilePath = Path.Combine(tempFolder, "generator.xml");

        // ------------------------------------------------------------
        // Scenario 1: Export barcode configuration to an XML file
        // ------------------------------------------------------------
        using (var generator = CreateGenerator())
        {
            // Force a full garbage collection to get a clean baseline
            GC.Collect();
            GC.WaitForPendingFinalizers();

            long before = GC.GetTotalMemory(true); // Memory before export
            generator.ExportToXml(xmlFilePath);    // Export to file path
            long after = GC.GetTotalMemory(true);  // Memory after export

            long diffFile = after - before;
            Console.WriteLine($"Memory increase after ExportToXml(string): {diffFile} bytes");
        }

        // ------------------------------------------------------------
        // Scenario 2: Export barcode configuration to a memory stream
        // ------------------------------------------------------------
        using (var generator = CreateGenerator())
        {
            using (var ms = new MemoryStream())
            {
                // Force a full garbage collection to get a clean baseline
                GC.Collect();
                GC.WaitForPendingFinalizers();

                long before = GC.GetTotalMemory(true); // Memory before export
                generator.ExportToXml(ms);             // Export to stream
                long after = GC.GetTotalMemory(true);  // Memory after export

                long diffStream = after - before;
                Console.WriteLine($"Memory increase after ExportToXml(Stream): {diffStream} bytes");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(xmlFilePath))
                File.Delete(xmlFilePath);

            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }

    /// <summary>
    /// Creates and configures a <see cref="BarcodeGenerator"/> instance with QR code settings.
    /// </summary>
    /// <returns>A configured <see cref="BarcodeGenerator"/> ready for export.</returns>
    static BarcodeGenerator CreateGenerator()
    {
        var gen = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        gen.Parameters.Barcode.XDimension.Pixels = 4f;
        gen.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
        return gen;
    }
}
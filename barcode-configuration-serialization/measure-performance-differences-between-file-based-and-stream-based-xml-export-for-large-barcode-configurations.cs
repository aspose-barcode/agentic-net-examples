// Title: Performance Comparison of File‑Based vs Stream‑Based XML Export for Barcode Configurations
// Description: Demonstrates how to measure and compare the execution time of exporting a large barcode generator configuration to XML using a file path versus a memory stream.
// Category-Description: This example belongs to the Aspose.BarCode configuration export category, illustrating how to serialize barcode generator settings to XML. It uses the BarcodeGenerator class and its ExportToXml methods, which are commonly employed when persisting or sharing barcode configurations across applications or for backup purposes. Developers often need to evaluate performance of file‑based versus stream‑based serialization for large configurations.
// Prompt: Measure performance differences between file‑based and stream‑based XML export for large barcode configurations.
// Tags: barcode, pdf417, xml export, performance, file export, stream export, aspose.barcode, configuration serialization

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Shows how to compare the performance of file‑based and stream‑based XML export
/// for a barcode generator with a large configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode generator, configures it,
    /// exports its settings to XML using both a file and a memory stream, and
    /// reports the elapsed times.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the XML file output.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlPerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Initialize a BarcodeGenerator with Pdf417 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleDataForPerformanceTest"))
        {
            // ----- Appearance settings -----
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // ----- Size and resolution settings -----
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Resolution = 300f;
            generator.Parameters.RotationAngle = 0f;

            // ----- Padding settings -----
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // ----- Pdf417‑specific settings -----
            generator.Parameters.Barcode.Pdf417.Columns = 10;
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // ----- Measure file‑based XML export -----
            string xmlFilePath = Path.Combine(tempDir, "generator_config.xml");
            var swFile = Stopwatch.StartNew();
            generator.ExportToXml(xmlFilePath);
            swFile.Stop();

            // ----- Measure stream‑based XML export -----
            using (var ms = new MemoryStream())
            {
                var swStream = Stopwatch.StartNew();
                generator.ExportToXml(ms);
                swStream.Stop();

                // Output the measured times and the location of the saved XML file.
                Console.WriteLine($"File‑based ExportToXml time: {swFile.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine($"Stream‑based ExportToXml time: {swStream.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine($"XML file saved to: {xmlFilePath}");
            }
        }

        // Optional: clean up the temporary directory.
        // Directory.Delete(tempDir, true);
    }
}
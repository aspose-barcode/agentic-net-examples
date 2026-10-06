// Title: Measure performance of XML export for large barcode configurations
// Description: Demonstrates how to compare the execution time of file‑based and stream‑based ExportToXml methods when exporting a complex barcode configuration.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating the use of BarcodeGenerator, its Parameters, and the ExportToXml API for persisting barcode settings. Developers often need to evaluate serialization speed for large configurations, choosing between file paths and streams to optimize I/O in batch processing or server‑side scenarios. The snippet shows typical setup, timing with Stopwatch, and cleanup, serving as a reference for performance‑critical barcode applications.
// Prompt: Measure performance differences between file‑based and stream‑based XML export for large barcode configurations.
// Tags: barcode symbology, performance, xml export, file stream, aspose.barcode, stopwatch, qr code

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that measures and compares the time required to export a
/// barcode configuration to XML using a file path versus a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up a barcode generator with a rich
    /// configuration, exports the settings to XML via file and stream, and
    /// reports the elapsed times.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the file‑based export
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the XML file that will be created
        string xmlFilePath = Path.Combine(tempDir, "barcode_config.xml");

        // Create a barcode generator with a complex set of parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "PerformanceTest"))
        {
            // Configure visual and technical properties of the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.RotationAngle = 0f;
            generator.Parameters.Resolution = 300f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // -------------------- File‑based ExportToXml --------------------
            var swFile = Stopwatch.StartNew();               // Start timing
            generator.ExportToXml(xmlFilePath);               // Export configuration to a physical XML file
            swFile.Stop();                                    // Stop timing

            // -------------------- Stream‑based ExportToXml --------------------
            long streamExportTicks;
            using (var ms = new MemoryStream())
            {
                var swStream = Stopwatch.StartNew();          // Start timing for stream export
                generator.ExportToXml(ms);                    // Export configuration to an in‑memory stream
                swStream.Stop();                               // Stop timing
                streamExportTicks = swStream.ElapsedTicks;    // Capture elapsed ticks
                ms.Position = 0;                               // Reset stream position (optional for timing)
            }

            // Output the measured times to the console
            Console.WriteLine("File‑based ExportToXml time:   {0} ms", swFile.ElapsedMilliseconds);
            Console.WriteLine("Stream‑based ExportToXml time: {0} ms", streamExportTicks * 1000 / Stopwatch.Frequency);
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(xmlFilePath))
                File.Delete(xmlFilePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the example
        }
    }
}
// Title: Measure XML export performance for barcode generator
// Description: Demonstrates measuring the time taken to export a large barcode configuration to XML using both file‑based and stream‑based approaches.
// Category-Description: This example belongs to the Aspose.BarCode XML serialization category, illustrating how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml. Developers working with barcode configuration persistence often need to compare file and stream performance for large settings, and this snippet shows typical API usage for performance testing and verification.
// Prompt: Measure performance differences between file‑based and stream‑based XML export for large barcode configurations.
// Tags: barcode, xml, export, import, performance, stream, file, aspose.barcode, qrcode, configuration

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that measures and compares the execution time of exporting a barcode generator
/// configuration to XML via a file path versus a memory stream. It also verifies that the exported
/// XML can be imported back correctly from both sources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs the performance measurement and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to hold the XML file
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlPerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string xmlFilePath = Path.Combine(tempDir, "generatorConfig.xml");

        // Build a barcode generator with a comprehensive configuration
        using (BarcodeGenerator generator = CreateConfiguredGenerator())
        {
            // -------------------- Export to XML file --------------------
            Stopwatch sw = new Stopwatch();
            sw.Start();
            generator.ExportToXml(xmlFilePath); // Synchronous file export
            sw.Stop();
            long fileExportMs = sw.ElapsedMilliseconds;

            // -------------------- Export to XML stream --------------------
            using (MemoryStream ms = new MemoryStream())
            {
                sw.Restart();
                generator.ExportToXml(ms); // Synchronous stream export
                sw.Stop();
                long streamExportMs = sw.ElapsedMilliseconds;

                // Reset stream position so it can be read for import verification
                ms.Position = 0;

                // -------------------- Import from XML file (verification) --------------------
                using (BarcodeGenerator fromFile = BarcodeGenerator.ImportFromXml(xmlFilePath))
                {
                    // No further action required; successful import confirms validity
                }

                // -------------------- Import from XML stream (verification) --------------------
                using (BarcodeGenerator fromStream = BarcodeGenerator.ImportFromXml(ms))
                {
                    // No further action required; successful import confirms validity
                }

                // Output measured times
                Console.WriteLine($"Export to XML file time: {fileExportMs} ms");
                Console.WriteLine($"Export to XML stream time: {streamExportMs} ms");
            }
        }

        // -------------------- Cleanup temporary files and directory --------------------
        try
        {
            if (File.Exists(xmlFilePath))
                File.Delete(xmlFilePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }

    /// <summary>
    /// Creates and returns a <see cref="BarcodeGenerator"/> pre‑configured with QR code settings,
    /// visual appearance, padding, border, caption, and code‑text parameters.
    /// </summary>
    /// <returns>A fully configured <see cref="BarcodeGenerator"/> instance.</returns>
    static BarcodeGenerator CreateConfiguredGenerator()
    {
        // Basic generator with QR symbology and sample text
        BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "SampleCodeTextForPerformanceTest");

        // Appearance and size settings
        gen.Parameters.Barcode.XDimension.Pixels = 2f;
        gen.Parameters.Barcode.QR.Version = QRVersion.Version10;
        gen.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
        gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
        gen.Parameters.BackColor = Aspose.Drawing.Color.White;
        gen.Parameters.Resolution = 300f;
        gen.Parameters.RotationAngle = 0f;

        // Padding around the barcode
        gen.Parameters.Barcode.Padding.Left.Point = 5f;
        gen.Parameters.Barcode.Padding.Top.Point = 5f;
        gen.Parameters.Barcode.Padding.Right.Point = 5f;
        gen.Parameters.Barcode.Padding.Bottom.Point = 5f;

        // Border settings
        gen.Parameters.Border.Color = Aspose.Drawing.Color.Blue;
        gen.Parameters.Border.Width.Pixels = 1f;

        // Caption placed above the barcode
        gen.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
        gen.Parameters.CaptionAbove.Font.FamilyName = "Arial";
        gen.Parameters.CaptionAbove.Font.Size.Point = 12f;

        // Code text parameters (displayed below the barcode)
        gen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
        gen.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

        return gen;
    }
}
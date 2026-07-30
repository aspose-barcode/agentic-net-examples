// Title: Measure performance of file vs stream XML export for large barcode configurations
// Description: Demonstrates how to export a complex barcode generator configuration to XML using both file‑based and stream‑based methods, and measures the time taken for each approach.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of BarcodeGenerator, ExportToXml, and ImportFromXml APIs. Developers often need to persist and restore barcode settings for batch processing or configuration sharing, and comparing file and memory‑stream exports helps choose the most efficient method for large configurations.
// Prompt: Measure performance differences between file‑based and stream‑based XML export for large barcode configurations.
// Tags: barcode symbology, export, xml, performance, file, stream, aspose.barcode, configuration

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates measuring performance differences between file‑based and stream‑based XML export for a large barcode configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a complex barcode generator, exports its configuration to XML via file and memory stream,
    /// measures execution time for each, and validates import from the stream.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with a complex configuration to simulate a large setup
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123456"))
        {
            // Set various barcode parameters
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.Barcode.FilledBars = false;
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.Resolution = 300f;

            // Measure file‑based XML export performance
            var stopwatch = Stopwatch.StartNew();
            bool fileExportSuccess = generator.ExportToXml("barcode_config.xml");
            stopwatch.Stop();
            long fileExportTimeMs = stopwatch.ElapsedMilliseconds;

            // Measure stream‑based XML export performance
            using (var memoryStream = new MemoryStream())
            {
                stopwatch.Restart();
                bool streamExportSuccess = generator.ExportToXml(memoryStream);
                stopwatch.Stop();
                long streamExportTimeMs = stopwatch.ElapsedMilliseconds;

                // Verify that the exported stream can be imported back successfully
                memoryStream.Position = 0;
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(memoryStream))
                {
                    // Import validation only; no further actions required
                }

                // Output the results
                Console.WriteLine($"File export success: {fileExportSuccess}, time: {fileExportTimeMs} ms");
                Console.WriteLine($"Stream export success: {streamExportSuccess}, time: {streamExportTimeMs} ms");
            }
        }
    }
}
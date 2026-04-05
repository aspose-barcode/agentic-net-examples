using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator with a long code text to simulate a large configuration
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, new string('A', 1000)))
        {
            // Set various generation parameters to increase the configuration size
            generator.Parameters.ImageWidth.Point = 800f;
            generator.Parameters.ImageHeight.Point = 300f;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Uniform padding
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;

            // Additional settings
            generator.Parameters.Barcode.Code128.Code128EncodeMode = Code128EncodeMode.Auto;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Parameters.Barcode.FilledBars = true;
            generator.Parameters.Resolution = 300f; // DPI

            // Measure file‑based XML export
            var fileStopwatch = Stopwatch.StartNew();
            bool fileExportResult = generator.ExportToXml("barcode_config_file.xml");
            fileStopwatch.Stop();

            // Measure stream‑based XML export
            using (var memoryStream = new MemoryStream())
            {
                var streamStopwatch = Stopwatch.StartNew();
                bool streamExportResult = generator.ExportToXml(memoryStream);
                streamStopwatch.Stop();

                // Output results
                Console.WriteLine($"File export success: {fileExportResult}");
                Console.WriteLine($"File export time: {fileStopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Stream export success: {streamExportResult}");
                Console.WriteLine($"Stream export time: {streamStopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}
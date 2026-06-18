using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting and importing barcode configuration to/from XML using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, exports its configuration to XML,
    /// measures export performance, and verifies import from both file and stream.
    /// </summary>
    static void Main()
    {
        // Define barcode type and content
        BaseEncodeType type = EncodeTypes.Code128;
        string codeText = "Sample1234567890";

        // Create a barcode generator with the specified type and text
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Configure barcode appearance and generator settings to produce a sizable XML
            generator.Parameters.Barcode.BarHeight.Point = 50f;               // Height of the barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;              // Width of the smallest bar
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable checksum
            generator.Parameters.Barcode.Padding.Left.Point = 5f;            // Left padding
            generator.Parameters.Barcode.Padding.Top.Point = 5f;             // Top padding
            generator.Parameters.Barcode.Padding.Right.Point = 5f;           // Right padding
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;          // Bottom padding
            generator.Parameters.Resolution = 300f;                           // Image resolution (dpi)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;           // Disable auto‑sizing

            string xmlFilePath = "barcodeConfig.xml";

            // -------------------- Export to XML file --------------------
            var swFile = Stopwatch.StartNew();               // Start timing file export
            generator.ExportToXml(xmlFilePath);               // Export configuration to file
            swFile.Stop();                                    // Stop timing

            // -------------------- Export to XML stream --------------------
            long streamLength;                                // Will hold the size of the exported stream
            var swStream = Stopwatch.StartNew();              // Start timing stream export
            using (var ms = new MemoryStream())
            {
                generator.ExportToXml(ms);                    // Export configuration to memory stream
                ms.Position = 0;                              // Reset position for reading
                streamLength = ms.Length;                     // Capture stream length in bytes
            }
            swStream.Stop();                                  // Stop timing

            // Output performance metrics
            Console.WriteLine($"File export time: {swFile.ElapsedMilliseconds} ms");
            Console.WriteLine($"Stream export time: {swStream.ElapsedMilliseconds} ms");
            Console.WriteLine($"Exported stream size: {streamLength} bytes");

            // -------------------- Import from XML file --------------------
            using (var genFromFile = BarcodeGenerator.ImportFromXml(xmlFilePath))
            {
                // Import succeeded; no further action required for this demo
            }

            // -------------------- Import from XML stream --------------------
            using (var msLoad = new MemoryStream())
            {
                // Re‑export to a new stream to simulate loading from stream
                generator.ExportToXml(msLoad);
                msLoad.Position = 0;                          // Reset for reading
                using (var genFromStream = BarcodeGenerator.ImportFromXml(msLoad))
                {
                    // Import succeeded; no further action required for this demo
                }
            }
        }

        // Remove the temporary XML file created during the demo
        if (File.Exists("barcodeConfig.xml"))
        {
            File.Delete("barcodeConfig.xml");
        }
    }
}
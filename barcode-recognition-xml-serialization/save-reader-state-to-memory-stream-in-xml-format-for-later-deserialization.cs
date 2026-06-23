using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, reading it, and exporting the reader's state to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, reads it, and exports the reader configuration to an XML stream.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (var barcodeStream = new MemoryStream())
        {
            // Initialize the barcode generator with the desired symbology and data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning so it can be read.
            barcodeStream.Position = 0;

            // Create a barcode reader that can decode all supported barcode types.
            using (var reader = new BarCodeReader(barcodeStream, DecodeType.AllSupportedTypes))
            {
                // Perform an initial read to verify that the barcode can be detected.
                var initialResults = reader.ReadBarCodes();
                Console.WriteLine($"Initial read: {initialResults.Length} barcode(s) detected.");

                // Export the reader's current state (settings, quality, etc.) to an XML representation.
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);
                    Console.WriteLine($"Exported reader state to XML (size: {xmlStream.Length} bytes).");

                    // Rewind the XML stream to the beginning for reading its contents.
                    xmlStream.Position = 0;

                    // Read the XML data as a string for display or further processing.
                    using (var sr = new StreamReader(xmlStream, leaveOpen: true))
                    {
                        string xml = sr.ReadToEnd();
                        Console.WriteLine("Reader state XML:");
                        Console.WriteLine(xml);
                    }

                    // At this point, xmlStream contains the serialized reader state.
                    // It can be saved, transmitted, or later restored using BarCodeReader.ImportFromXml(xmlStream).
                }
            }
        }
    }
}
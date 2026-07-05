// Title: Scheduled Export of Barcode Reader State to XML
// Description: Demonstrates generating barcodes, reading them, and exporting the reader state to XML for audit logging.
// Prompt: Create a scheduled job that periodically exports reader state to XML for audit logging of processed barcodes.
// Tags: barcode symbology, generation, recognition, xml, audit logging, scheduled job

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates sample barcodes, reads them, and exports the reader state to XML.
/// Intended to be invoked by an external scheduler for periodic audit logging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates barcodes, reads them, and writes the reader state to XML files.
    /// </summary>
    static void Main()
    {
        // Define sample barcodes with their symbology and corresponding code text.
        var samples = new (BaseEncodeType EncodeType, string CodeText)[]
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DatabarStacked, "(01)01234567890123")
        };

        // Iterate over each sample barcode definition.
        for (int i = 0; i < samples.Length; i++)
        {
            var (encodeType, codeText) = samples[i];

            // Generate a barcode image and store it in a memory stream.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode as a PNG image into the stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for subsequent reading.

                // Initialize a barcode reader to decode all supported types from the image stream.
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    Console.WriteLine($"Reading barcode {i + 1}: {encodeType.TypeName}");

                    // Enumerate and display each detected barcode result.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                    }

                    // Export the internal reader state to an XML file for audit purposes.
                    string xmlPath = $"ReaderState_{i + 1}_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
                    bool exported = reader.ExportToXml(xmlPath);
                    Console.WriteLine(exported
                        ? $"  Reader state exported to: {xmlPath}"
                        : $"  Failed to export reader state for barcode {i + 1}");
                }
            }

            Console.WriteLine(); // Add a blank line as a visual separator between samples.
        }

        // Note: This console application runs once and exits.
        // To schedule periodic execution, configure an external scheduler (e.g., Windows Task Scheduler) to run this program at desired intervals.
    }
}
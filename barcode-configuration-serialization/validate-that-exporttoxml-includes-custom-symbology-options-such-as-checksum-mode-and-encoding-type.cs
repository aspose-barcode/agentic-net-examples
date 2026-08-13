// Title: Export QR barcode settings to XML and verify custom symbology options
// Description: Demonstrates exporting a QR barcode generator's configuration, including checksum and encoding settings, to XML and re-importing it to confirm the options are preserved.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, showcasing how to use BarcodeGenerator, its Parameters, and the ExportToXml/ImportFromXml APIs. Developers often need to persist barcode settings for later reuse, configuration files, or cross‑application sharing. The snippet illustrates setting custom symbology options, exporting them to an XML stream, and validating that they survive a round‑trip.
// Prompt: Validate that ExportToXml includes custom symbology options such as checksum mode and encoding type.
// Tags: barcode symbology, export, xml, checksum, encoding, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR barcode, configures custom symbology options,
/// exports the generator settings to XML, and verifies that the options are retained
/// after importing the XML back into a new generator instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export‑import validation of custom QR barcode options.
    /// </summary>
    static void Main()
    {
        // Initialize a QR barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Configure custom symbology options.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable checksum calculation.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;       // Set UTF‑8 encoding for QR data.

            // Export the generator's configuration to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                bool exportResult = generator.ExportToXml(xmlStream);
                Console.WriteLine($"ExportToXml succeeded: {exportResult}");

                // Reset the stream position to the beginning for reading.
                xmlStream.Position = 0;

                // Import the settings from the XML stream into a new generator instance.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Verify that the checksum option was preserved.
                    bool checksumRestored = importedGenerator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
                    // Verify that the encoding type was preserved.
                    bool eciRestored = importedGenerator.Parameters.Barcode.QR.ECIEncoding == ECIEncodings.UTF8;

                    Console.WriteLine($"Checksum option restored: {checksumRestored}");
                    Console.WriteLine($"Encoding type restored: {eciRestored}");
                }
            }
        }
    }
}
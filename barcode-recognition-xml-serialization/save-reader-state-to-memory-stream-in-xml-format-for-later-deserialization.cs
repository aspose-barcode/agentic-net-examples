// Title: Save BarCodeReader state to XML in memory stream
// Description: Demonstrates exporting a BarCodeReader configuration to an XML representation stored in a MemoryStream and then importing it back, showing how to persist and restore reader settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition configuration management category. It showcases the use of BarCodeReader, its BarcodeSettings and QualitySettings, and the ExportToXml / ImportFromXml APIs to serialize and deserialize reader state. Developers often need to persist barcode reader configurations across sessions or share them between services, and this pattern provides a quick way to achieve that using in‑memory XML streams.
// Prompt: Save the reader state to a memory stream in XML format for later deserialization.
// Tags: barcode, serialization, xml, memorystream, barcodereader, exporttoxml, importfromxml, configuration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that exports a BarCodeReader's configuration to XML,
/// stores it in a memory stream, and then imports it back to verify the settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the export, display, and import of the reader state.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the XML representation of the reader state
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Initialize a BarCodeReader and configure its settings
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Enable stripping of FNC characters and set a small X-dimension for quality
                reader.BarcodeSettings.StripFNC = true;
                reader.QualitySettings.XDimension = XDimensionMode.Small;

                // Export the configured reader state to the memory stream in XML format
                reader.ExportToXml(memoryStream);
            }

            // Reset the stream position to the beginning for reading the exported XML
            memoryStream.Position = 0;

            // Read and display the exported XML content
            using (StreamReader sr = new StreamReader(memoryStream, leaveOpen: true))
            {
                string xml = sr.ReadToEnd();
                Console.WriteLine("Exported BarCodeReader state (XML):");
                Console.WriteLine(xml);
            }

            // Reset the stream position again to prepare for deserialization
            memoryStream.Position = 0;

            // Import a new BarCodeReader instance from the XML stored in the memory stream
            using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(memoryStream))
            {
                // Verify that the settings were correctly restored after import
                Console.WriteLine($"Imported StripFNC: {importedReader.BarcodeSettings.StripFNC}");
                Console.WriteLine($"Imported XDimension: {importedReader.QualitySettings.XDimension}");
            }
        }
    }
}
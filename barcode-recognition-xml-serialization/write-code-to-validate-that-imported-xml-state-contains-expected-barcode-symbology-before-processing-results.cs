// Title: Validate barcode symbology from exported XML configuration
// Description: Demonstrates how to generate a QR barcode, export its configuration to XML, verify that the XML contains the expected symbology, and then decode the barcode image only after successful validation.
// Category-Description: This example belongs to the Aspose.BarCode XML configuration and validation category. It shows usage of BarcodeGenerator for creating barcodes, ExportToXml for persisting settings, XDocument for XML parsing, and BarCodeReader for decoding. Developers often need to ensure that imported barcode settings match expected types before processing, especially in automated workflows or when handling external configuration files.
// Prompt: Write code to validate that an imported XML state contains the expected barcode symbology before processing results.
// Tags: barcode symbology, validation, xml, aspose.barcode, generation, recognition, qr, exporttoxml, decode

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, exports its configuration to XML,
/// validates the symbology stored in the XML, and reads the barcode image if validation succeeds.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, validation, and decoding steps.
    /// </summary>
    static void Main()
    {
        // Expected symbology name (as it appears in the exported XML)
        const string expectedSymbology = "QR";

        // Prepare a temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeValidation_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the XML configuration and the barcode image
        string xmlPath = Path.Combine(tempFolder, "barcode_config.xml");
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // 1. Generate a QR barcode and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Save the barcode image to a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration (including symbology) to an XML file
            generator.ExportToXml(xmlPath);
        }

        // 2. Validate that the exported XML contains the expected symbology
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Configuration XML not found.");
            return;
        }

        XDocument doc;
        try
        {
            // Load the XML document for parsing
            doc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load XML: {ex.Message}");
            return;
        }

        // The symbology is stored in the <EncodeType> element under <Parameters>/<Barcode>
        string actualSymbology = doc.Root?
            .Descendants("EncodeType")
            .FirstOrDefault()?.Value ?? string.Empty;

        // Compare the actual symbology with the expected value (case‑insensitive)
        if (!string.Equals(actualSymbology, expectedSymbology, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Symbology mismatch. Expected: {expectedSymbology}, Found: {actualSymbology}");
            return;
        }

        Console.WriteLine($"Symbology validation succeeded: {actualSymbology}");

        // 3. Read the barcode image only if the symbology matches
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        // Use DecodeType.AllSupportedTypes to detect any barcode present in the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Optional: set high‑performance quality settings for faster decoding
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Decode all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // Cleanup temporary files and folder (best‑effort, ignore any errors)
        try
        {
            File.Delete(xmlPath);
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is non‑critical
        }
    }
}
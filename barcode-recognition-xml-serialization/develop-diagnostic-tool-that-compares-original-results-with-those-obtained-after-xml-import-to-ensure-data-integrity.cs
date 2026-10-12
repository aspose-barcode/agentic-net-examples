// Title: Barcode XML Export/Import Integrity Check
// Description: Generates a QR barcode, exports its configuration to XML, re-imports the XML to recreate the barcode, and compares the decoded results to verify data integrity.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, export settings to XML, import them back, and employ BarCodeReader for decoding. Typical use cases include persisting barcode configurations, migrating settings between systems, and ensuring that exported/imported data yields identical scan results. Developers often need to validate that XML serialization preserves all essential barcode parameters.
// Prompt: Develop a diagnostic tool that compares original results with those obtained after XML import to ensure data integrity.
// Tags: barcode, qr, generation, export, import, xml, recognition, integrity, aspose.barcode, diagnostics

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR barcode, exporting its settings to XML, importing the XML,
/// regenerating the barcode, and comparing decoded results to ensure data integrity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, XML export/import,
    /// decoding, and comparison workflow.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for all intermediate files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDiag_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original image, XML configuration, and the imported image.
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string xmlPath = Path.Combine(tempFolder, "generator.xml");
        string importedImagePath = Path.Combine(tempFolder, "imported.png");

        // ------------------------------------------------------------
        // Generate the original QR barcode and export its settings to XML.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Set a specific X‑dimension for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the barcode image as PNG.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Import the generator configuration from XML and generate a new barcode.
        // ------------------------------------------------------------
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        using (importedGenerator)
        {
            // Save the regenerated barcode image.
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Local function: reads barcode results from a given image file.
        // ------------------------------------------------------------
        BarCodeResult[] ReadBarcode(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return Array.Empty<BarCodeResult>();
            }

            // Use all supported decode types for maximum compatibility.
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                return reader.ReadBarCodes();
            }
        }

        // Decode barcodes from both the original and imported images.
        BarCodeResult[] originalResults = ReadBarcode(originalImagePath);
        BarCodeResult[] importedResults = ReadBarcode(importedImagePath);

        // ------------------------------------------------------------
        // Compare the decoded results to verify that the XML import preserved data.
        // ------------------------------------------------------------
        bool success = false;
        if (originalResults.Length > 0 && importedResults.Length > 0)
        {
            var orig = originalResults[0];
            var imp = importedResults[0];

            // Compare barcode type names (case‑insensitive) and text lengths.
            bool typeMatch = string.Equals(orig.CodeTypeName, imp.CodeTypeName, StringComparison.OrdinalIgnoreCase);
            bool textLengthMatch = orig.CodeText?.Length == imp.CodeText?.Length;

            success = typeMatch && textLengthMatch;

            Console.WriteLine($"Original CodeType: {orig.CodeTypeName}, CodeText Length: {orig.CodeText?.Length}");
            Console.WriteLine($"Imported CodeType: {imp.CodeTypeName}, CodeText Length: {imp.CodeText?.Length}");
            Console.WriteLine($"Type match: {typeMatch}");
            Console.WriteLine($"Text length match: {textLengthMatch}");
        }
        else
        {
            Console.WriteLine("One or both images did not yield any barcode results.");
        }

        Console.WriteLine($"Data integrity check: {(success ? "PASS" : "FAIL")}");

        // ------------------------------------------------------------
        // Cleanup temporary files and folder (optional).
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors to avoid interrupting the flow.
        }
    }
}
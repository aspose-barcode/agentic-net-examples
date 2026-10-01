// Title: Barcode XML configuration integrity diagnostic tool
// Description: Demonstrates generating a barcode, exporting its configuration to XML, re‑importing it, and comparing detection results to verify data integrity.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to use BarcodeGenerator, ExportToXml, ImportFromXml, and BarCodeReader to persist and restore barcode settings. Typical use cases include automated testing, migration of barcode generation settings, and ensuring that exported XML retains all necessary parameters. Developers often need to compare original and re‑generated barcodes to confirm that configuration round‑tripping does not alter output.
// Prompt: Develop a diagnostic tool that compares original results with those obtained after XML import to ensure data integrity.
// Tags: barcode, xml, configuration, integrity, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a diagnostic workflow that generates a barcode, exports its configuration to XML,
/// re‑imports the configuration, regenerates the barcode, and compares the detection results
/// to ensure that the XML round‑trip preserves data integrity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, export, import, and comparison steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "DiagTool_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original barcode image, the imported barcode image, and the XML configuration
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string importedImagePath = Path.Combine(tempFolder, "imported.png");
        string xmlPath = Path.Combine(tempFolder, "config.xml");

        // 1. Generate the original barcode using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Save the original barcode image to disk
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // 2. Read the original barcode from the saved image
            BarCodeResult[] originalResults = ReadBarcodes(originalImagePath);

            // 3. Export the generator's configuration to an XML stream
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0;

                // Convert the XML stream to a string for optional inspection and file saving
                using (var reader = new StreamReader(xmlStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    string xmlContent = reader.ReadToEnd();

                    // Save the XML configuration to a file (optional, useful for debugging)
                    File.WriteAllText(xmlPath, xmlContent);

                    // 4. Import the configuration from the XML string into a new generator instance
                    using (var importStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
                    {
                        using (var importedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                        {
                            // Save the barcode generated from the imported configuration
                            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
                        }
                    }
                }
            }

            // 5. Read the barcode generated from the imported configuration
            BarCodeResult[] importedResults = ReadBarcodes(importedImagePath);

            // 6. Compare detection results between the original and imported barcodes
            Console.WriteLine("=== Comparison Results ===");
            bool originalHasResult = originalResults != null && originalResults.Length > 0;
            bool importedHasResult = importedResults != null && importedResults.Length > 0;

            Console.WriteLine($"Original barcode detected: {originalHasResult}");
            Console.WriteLine($"Imported barcode detected: {importedHasResult}");

            if (originalHasResult && importedHasResult)
            {
                var orig = originalResults[0];
                var imp = importedResults[0];

                bool sameType = string.Equals(orig.CodeTypeName, imp.CodeTypeName, StringComparison.OrdinalIgnoreCase);
                bool bothHaveText = !string.IsNullOrEmpty(orig.CodeText) && !string.IsNullOrEmpty(imp.CodeText);

                Console.WriteLine($"Same symbology: {sameType}");
                Console.WriteLine($"Both have non‑empty CodeText: {bothHaveText}");
                Console.WriteLine($"Original CodeText (may contain watermark): {orig.CodeText}");
                Console.WriteLine($"Imported CodeText (may contain watermark): {imp.CodeText}");
            }
            else
            {
                Console.WriteLine("One of the reads failed; cannot compare detailed data.");
            }
        }

        // Cleanup temporary files and folder (optional)
        try
        {
            if (File.Exists(originalImagePath)) File.Delete(originalImagePath);
            if (File.Exists(importedImagePath)) File.Delete(importedImagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }

    /// <summary>
    /// Reads all barcodes from the specified image file using Aspose.BarCode's <see cref="BarCodeReader"/>.
    /// </summary>
    /// <param name="imagePath">Path to the image file containing barcodes.</param>
    /// <returns>An array of <see cref="BarCodeResult"/> objects; empty if none are found or an error occurs.</returns>
    private static BarCodeResult[] ReadBarcodes(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return Array.Empty<BarCodeResult>();
        }

        // Initialize the reader to decode all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            try
            {
                // Perform the detection and return the results
                return reader.ReadBarCodes();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading barcode: {ex.Message}");
                return Array.Empty<BarCodeResult>();
            }
        }
    }
}
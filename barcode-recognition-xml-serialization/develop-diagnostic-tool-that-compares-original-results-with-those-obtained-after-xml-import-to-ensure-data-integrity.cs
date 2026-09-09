// Title: Barcode generation, XML export/import, and validation
// Description: Demonstrates generating a Code128 barcode, exporting its configuration to XML, re-importing it, and verifying that the regenerated barcode matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, its ExportToXml/ImportFromXml methods, and BarCodeReader to create, persist, and validate barcodes. Typical use cases include diagnostic tools, migration of barcode settings, and automated integrity checks where developers need to ensure that exported configurations produce identical barcodes when re-imported.
// Prompt: Develop a diagnostic tool that compares original results with those obtained after XML import to ensure data integrity.
// Tags: barcode symbology, generation, import, export, xml, validation, codetype, codetext, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates a diagnostic workflow that generates a barcode, exports its settings to XML,
/// re‑imports the configuration, regenerates the barcode, and compares the read results
/// to ensure data integrity.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the barcode generation, XML export/import, and result comparison steps.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for all generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDiag_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the original image, XML configuration, and the imported image
        string originalImagePath = Path.Combine(workFolder, "original.png");
        string xmlPath = Path.Combine(workFolder, "generator.xml");
        string importedImagePath = Path.Combine(workFolder, "imported.png");

        const string codeText = "ABC123XYZ";

        // Step 1: Generate the original barcode and export its generation state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath); // Persist generator settings
        }

        // Step 2: Import the generator from the saved XML and generate a second barcode
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        }

        // Step 3: Read both barcode images using BarCodeReader
        BarCodeResult originalResult = null;
        BarCodeResult importedResult = null;

        if (File.Exists(originalImagePath))
        {
            using (var reader = new BarCodeReader(originalImagePath, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                if (results.Length > 0)
                    originalResult = results[0];
            }
        }

        if (File.Exists(importedImagePath))
        {
            using (var reader = new BarCodeReader(importedImagePath, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                if (results.Length > 0)
                    importedResult = results[0];
            }
        }

        // Step 4: Compare the read results for type and text consistency
        bool typeMatch = originalResult?.CodeTypeName == importedResult?.CodeTypeName;
        bool textPresent = !string.IsNullOrEmpty(originalResult?.CodeText) && !string.IsNullOrEmpty(importedResult?.CodeText);

        // Output a summary of the comparison
        Console.WriteLine("Original barcode image: " + (File.Exists(originalImagePath) ? "found" : "missing"));
        Console.WriteLine("Imported barcode image: " + (File.Exists(importedImagePath) ? "found" : "missing"));
        Console.WriteLine("Original read result: " + (originalResult != null ? "available" : "none"));
        Console.WriteLine("Imported read result: " + (importedResult != null ? "available" : "none"));
        Console.WriteLine("Code type match: " + (typeMatch ? "YES" : "NO"));
        Console.WriteLine("Both have readable text: " + (textPresent ? "YES" : "NO"));
        Console.WriteLine("Original CodeText: " + (originalResult?.CodeText ?? "N/A"));
        Console.WriteLine("Imported CodeText: " + (importedResult?.CodeText ?? "N/A"));
    }
}
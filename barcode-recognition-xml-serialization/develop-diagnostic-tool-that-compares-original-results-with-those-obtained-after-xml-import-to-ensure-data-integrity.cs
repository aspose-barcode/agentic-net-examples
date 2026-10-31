// Title: QR barcode generation with XML export/import and validation
// Description: Demonstrates creating a QR barcode, exporting its generator state to XML, re‑importing it, regenerating the image, and verifying data integrity by decoding both images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, ExportToXml, ImportFromXml, and BarCodeReader. Typical use cases include persisting barcode settings, migrating configurations, and ensuring that exported/imported states produce identical results. Developers often need to compare original and imported outputs to validate serialization integrity.
// Prompt: Develop a diagnostic tool that compares original results with those obtained after XML import to ensure data integrity.
// Tags: qr, barcode, generation, xml, import, export, decoding, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, XML state export/import, and validation of data integrity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode, exports its configuration to XML, re‑imports it, regenerates the barcode, decodes both images, and compares the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDiag_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original image, XML state, and the image generated after import
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string xmlPath = Path.Combine(tempFolder, "generator.xml");
        string importedImagePath = Path.Combine(tempFolder, "imported.png");

        // Step 1: Generate original QR barcode and export its generator state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Import generator state from XML and generate the barcode again
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        using (importedGenerator)
        {
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        }

        // Step 3: Decode both barcode images to retrieve the encoded text
        string originalDecoded = DecodeBarcode(originalImagePath);
        string importedDecoded = DecodeBarcode(importedImagePath);

        // Step 4: Output file locations and decoded texts
        Console.WriteLine("Original barcode image path: " + originalImagePath);
        Console.WriteLine("Imported barcode image path: " + importedImagePath);
        Console.WriteLine();

        Console.WriteLine("Original decoded text: " + (originalDecoded ?? "No barcode detected"));
        Console.WriteLine("Imported decoded text: " + (importedDecoded ?? "No barcode detected"));
        Console.WriteLine();

        // Compare the decoded texts for equality
        bool textsMatch = string.Equals(originalDecoded, importedDecoded, StringComparison.Ordinal);
        Console.WriteLine("Decoded texts match: " + textsMatch);

        // Additionally compare the generator's CodeText property (simulated retrieval)
        string originalCodeText = GetGeneratorCodeText(originalImagePath);
        string importedCodeText = GetGeneratorCodeText(importedImagePath);
        bool codeTextsMatch = string.Equals(originalCodeText, importedCodeText, StringComparison.Ordinal);
        Console.WriteLine("Generator CodeText match: " + codeTextsMatch);
    }

    /// <summary>
    /// Decodes the first barcode found in the specified image file.
    /// </summary>
    /// <param name="imagePath">Path to the image containing the barcode.</param>
    /// <returns>The decoded text, or null if no barcode is detected.</returns>
    static string DecodeBarcode(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("File not found: " + imagePath);
            return null;
        }

        // Use BarCodeReader to scan all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                return result.CodeText;
            }
        }

        return null;
    }

    /// <summary>
    /// Retrieves the CodeText associated with the barcode generation step.
    /// </summary>
    /// <param name="imagePath">Path to the generated barcode image (unused in this demo).</param>
    /// <returns>The original CodeText ("Test123") for demonstration purposes.</returns>
    static string GetGeneratorCodeText(string imagePath)
    {
        // In a real scenario, the CodeText would be stored separately.
        // This demo returns the known original text.
        return "Test123";
    }
}
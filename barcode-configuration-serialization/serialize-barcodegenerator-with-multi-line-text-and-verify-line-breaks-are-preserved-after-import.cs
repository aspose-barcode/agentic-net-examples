// Title: Serialize and Import a BarcodeGenerator with Multi‑Line Text
// Description: Demonstrates how to generate a PDF417 barcode with multi‑line text, export its configuration to XML, import it back, and verify that line breaks are retained.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator, configure visual parameters, export the generator state to XML with ExportToXml, and later recreate the generator using ImportFromXml. Developers often need to persist barcode settings, share them across services, or store them for later regeneration.
// Prompt: Serialize a BarcodeGenerator with multi‑line text and verify line breaks are preserved after import.
// Tags: pdf417, serialization, png, barcodegenerator, exporttoxml, importfromxml

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a PDF417 barcode with multi‑line text,
/// serializes the generator to XML, imports it back, and checks that
/// line breaks are preserved in the CodeText property.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation,
    /// serialization, import, and verification steps.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder to store generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for XML state and PNG images.
        string xmlPath = Path.Combine(workFolder, "generator.xml");
        string originalImagePath = Path.Combine(workFolder, "original.png");
        string importedImagePath = Path.Combine(workFolder, "imported.png");

        // Multi‑line text (using Windows line breaks) to be encoded in the barcode.
        string multiLineText = "First line\r\nSecond line\r\nThird line";

        // -----------------------------------------------------------------
        // Create a barcode generator, configure visual parameters,
        // save the original image, and export the generator state to XML.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, multiLineText))
        {
            // Optional visual tweaks.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.Pdf417.Rows = 12;

            // Save the generated barcode as a PNG image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the complete generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // -----------------------------------------------------------------
        // Import the barcode generator from the previously saved XML,
        // regenerate the image, and verify that the multi‑line text is unchanged.
        // -----------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode generated from the imported configuration.
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);

            // Retrieve the CodeText after import and compare with the original.
            string importedCodeText = importedGenerator.CodeText;
            bool isPreserved = multiLineText == importedCodeText;

            // Output verification results to the console.
            Console.WriteLine("Original CodeText:");
            Console.WriteLine(multiLineText);
            Console.WriteLine("Imported CodeText:");
            Console.WriteLine(importedCodeText);
            Console.WriteLine("Line breaks preserved: " + (isPreserved ? "Yes" : "No"));
        }

        // Optional clean‑up of temporary files.
        // Directory.Delete(workFolder, true);
    }
}
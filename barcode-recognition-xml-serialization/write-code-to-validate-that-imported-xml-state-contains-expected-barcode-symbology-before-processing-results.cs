// Title: Validate barcode symbology from imported XML state
// Description: Demonstrates how to generate a barcode, export its configuration to XML, import it back, and verify that the symbology matches the expected type before further processing.
// Category-Description: This example belongs to the Aspose.BarCode XML serialization and symbology validation category. It shows how to use BarcodeGenerator, EncodeTypes, BaseEncodeType, and BarCodeImageFormat to generate a barcode, export its state to XML, import it, and compare the BarcodeType. Developers often need to ensure that imported barcode configurations retain the correct symbology when loading from persisted XML files.
// Prompt: Write code to validate that an imported XML state contains the expected barcode symbology before processing results.
// Tags: barcode, symbology, validation, xml, import, export, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode symbology validation using Aspose.BarCode XML import/export.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode, exports its state to XML, re-imports it, and validates the symbology.
/// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Set up temporary working directory and file paths
        // -----------------------------------------------------------------
        string basePath = Path.Combine(Path.GetTempPath(), "SymbologyValidateDemo");
        Directory.CreateDirectory(basePath);
        string xmlPath = Path.Combine(basePath, "generator.xml");
        string imagePath = Path.Combine(basePath, "barcode.png");

        // -----------------------------------------------------------------
        // Define the expected symbology name (as it appears in EncodeTypes)
        // -----------------------------------------------------------------
        string expectedSymbologyName = "QR";

        // Resolve the corresponding BaseEncodeType value via reflection
        FieldInfo expectedField = typeof(EncodeTypes).GetField(expectedSymbologyName);
        if (expectedField == null)
        {
            Console.WriteLine($"Unknown symbology name: {expectedSymbologyName}");
            return;
        }
        BaseEncodeType expectedEncodeType = (BaseEncodeType)expectedField.GetValue(null);

        // -----------------------------------------------------------------
        // Generate a barcode and export its configuration to XML
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // -----------------------------------------------------------------
        // Ensure the XML file was created before attempting import
        // -----------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // -----------------------------------------------------------------
        // Import the barcode generator from the XML and validate symbology
        // -----------------------------------------------------------------
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            BaseEncodeType importedEncodeType = importedGen.BarcodeType;
            if (importedEncodeType == null)
            {
                Console.WriteLine("Imported generator does not have a BarcodeType.");
                return;
            }

            // Compare imported symbology with the expected one
            if (importedEncodeType.Equals(expectedEncodeType))
            {
                Console.WriteLine($"Symbology validation succeeded: {expectedSymbologyName}");

                // Optionally regenerate the barcode using the imported settings
                string importedImagePath = Path.Combine(basePath, "barcode_imported.png");
                importedGen.Save(importedImagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {importedImagePath}");
            }
            else
            {
                Console.WriteLine($"Symbology validation failed. Expected: {expectedSymbologyName}, Found: {importedEncodeType}");
            }
        }
    }
}
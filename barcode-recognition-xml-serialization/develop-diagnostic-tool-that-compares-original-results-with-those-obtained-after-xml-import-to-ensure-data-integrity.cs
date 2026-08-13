// Title: Barcode XML Settings Import Diagnostic
// Description: Demonstrates generating a barcode, exporting its settings to XML, re-importing them, and verifying image integrity.
// Category-Description: This example belongs to the Aspose.BarCode settings management category, illustrating how to use BarcodeGenerator, ExportToXml, and ImportFromXml for preserving barcode configuration. Developers often need to serialize barcode settings for storage or transfer and ensure that reconstituted barcodes remain identical to the originals. The snippet shows typical use cases such as configuration backup, migration, and automated testing of data integrity.
// Prompt: Develop a diagnostic tool that compares original results with those obtained after XML import to ensure data integrity.
// Tags: barcode symbology, generation, xml import, integrity check, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode, exporting its settings to XML, importing them back,
/// and comparing the resulting images to ensure data integrity.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the diagnostic tool.
    /// </summary>
    static void Main()
    {
        // Prepare output folder for generated files
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeDiagnostic");
        Directory.CreateDirectory(outputDir);

        // Define file paths for original image, imported image, and XML settings
        string originalImagePath = Path.Combine(outputDir, "original.png");
        string importedImagePath = Path.Combine(outputDir, "imported.png");
        string xmlPath = Path.Combine(outputDir, "settings.xml");

        // 1. Create original barcode generator with sample settings
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Configure a few barcode parameters
            originalGenerator.Parameters.Barcode.XDimension.Point = 2f;
            originalGenerator.Parameters.Barcode.BarHeight.Point = 40f;
            originalGenerator.Parameters.Barcode.FilledBars = true;
            originalGenerator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            originalGenerator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Save the original barcode image to PNG
            originalGenerator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file
            originalGenerator.ExportToXml(xmlPath);
        }

        // 2. Import settings from XML into a new generator instance
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import barcode settings from XML.");
            return;
        }

        // Save the barcode generated from the imported settings
        importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);

        // 3. Compare the two images byte by byte to verify they are identical
        bool imagesIdentical = false;
        if (File.Exists(originalImagePath) && File.Exists(importedImagePath))
        {
            byte[] originalBytes = File.ReadAllBytes(originalImagePath);
            byte[] importedBytes = File.ReadAllBytes(importedImagePath);

            if (originalBytes.Length == importedBytes.Length)
            {
                imagesIdentical = true;
                for (int i = 0; i < originalBytes.Length; i++)
                {
                    if (originalBytes[i] != importedBytes[i])
                    {
                        imagesIdentical = false;
                        break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("One of the barcode images was not created.");
            return;
        }

        // 4. Output the comparison result to the console
        if (imagesIdentical)
        {
            Console.WriteLine("Success: The barcode image after XML import matches the original.");
        }
        else
        {
            Console.WriteLine("Warning: The barcode image after XML import differs from the original.");
        }

        // Clean up the imported generator instance
        importedGenerator.Dispose();
    }
}
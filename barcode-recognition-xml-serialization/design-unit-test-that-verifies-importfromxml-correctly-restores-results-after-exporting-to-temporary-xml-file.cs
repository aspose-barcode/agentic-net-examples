// Title: Verify ImportFromXml Restores Barcode Generator State
// Description: Demonstrates exporting a barcode generator configuration to XML, importing it back, and confirming that the regenerated image matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to persist and restore barcode generator settings using XML. It highlights the BarcodeGenerator class, its ExportToXml and ImportFromXml methods, and typical scenarios such as configuration backup, sharing settings across applications, or unit testing generator consistency.
// Prompt: Design a unit test that verifies ImportFromXml correctly restores results after exporting to a temporary XML file.
// Tags: barcode symbology, import, export, xml, unit-test, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates the ImportFromXml functionality of Aspose.BarCode by
/// exporting a generator configuration to XML, re-importing it, and comparing the resulting images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export, import, and comparison steps.
    /// </summary>
    static void Main()
    {
        // ---------- Prepare a unique temporary directory ----------
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Paths for XML configuration and generated PNG images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string originalImagePath = Path.Combine(tempDir, "original.png");
        string loadedImagePath = Path.Combine(tempDir, "loaded.png");

        // ---------- Create and configure a barcode generator ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "TestCode123"))
        {
            // Example customizations
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.Pdf417.Columns = 4; // additional setting for demonstration

            // Save the barcode image generated with the original settings
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // ---------- Import the generator configuration from XML ----------
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        using (importedGenerator)
        {
            // Generate a new image using the imported settings
            importedGenerator.Save(loadedImagePath, BarCodeImageFormat.Png);
        }

        // ---------- Compare the two images byte by byte ----------
        bool passed = false;
        if (File.Exists(originalImagePath) && File.Exists(loadedImagePath))
        {
            byte[] originalBytes = File.ReadAllBytes(originalImagePath);
            byte[] loadedBytes = File.ReadAllBytes(loadedImagePath);

            if (originalBytes.Length == loadedBytes.Length)
            {
                passed = true;
                for (int i = 0; i < originalBytes.Length; i++)
                {
                    if (originalBytes[i] != loadedBytes[i])
                    {
                        passed = false;
                        break;
                    }
                }
            }
        }

        // Output the test result
        Console.WriteLine(passed
            ? "PASSED: ImportFromXml restored generator state correctly."
            : "FAILED: Imported generator produced different output.");

        // ---------- Cleanup temporary files and directory (optional) ----------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignoring any cleanup errors to avoid affecting test outcome
        }
    }
}
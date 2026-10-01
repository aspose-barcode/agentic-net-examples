// Title: ImportFromXml restores barcode reader configuration and yields identical results
// Description: Demonstrates exporting a BarCodeReader configuration to XML, then importing it to verify that barcode reading results remain unchanged.
// Category-Description: This example belongs to the Aspose.BarCode configuration persistence category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and the ExportToXml/ImportFromXml methods. Typical use cases include saving reader settings for later reuse, sharing configurations across environments, and writing unit tests that validate configuration round‑tripping. Developers working with barcode generation and recognition often need to persist and restore reader configurations reliably.
// Prompt: Design a unit test that verifies ImportFromXml correctly restores results after exporting to a temporary XML file.
// Tags: barcode symbology, generation, recognition, xml, import, export, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, exports the reader configuration to XML,
/// imports it back, and verifies that the read results are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, configuration export/import,
    /// and result verification without requiring interactive console input.
    /// </summary>
    static void Main()
    {
        // Prepare temporary directory and file paths
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerConfig.xml");

        try
        {
            // 1. Generate a barcode image using Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // 2. Read the barcode with a fresh reader and export its configuration to XML
            BarCodeResult[] originalResults;
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                originalResults = reader.ReadBarCodes();
                // Persist reader settings (e.g., decoding options) to an XML file
                reader.ExportToXml(xmlPath);
            }

            // Ensure that the initial read produced at least one result
            if (originalResults == null || originalResults.Length == 0)
            {
                Console.WriteLine("Original read failed: no results.");
                return;
            }

            // 3. Import the reader configuration from the XML file
            using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Associate the same image with the imported reader (ImportFromXml restores only settings)
                importedReader.SetBarCodeImage(imagePath);
                BarCodeResult[] importedResults = importedReader.ReadBarCodes();

                // 4. Verify that the imported reader yields the same results as the original reader
                if (importedResults == null || importedResults.Length == 0)
                {
                    Console.WriteLine("Imported read failed: no results.");
                    return;
                }

                bool match = true;
                if (originalResults.Length != importedResults.Length)
                {
                    match = false;
                }
                else
                {
                    for (int i = 0; i < originalResults.Length; i++)
                    {
                        if (originalResults[i].CodeText != importedResults[i].CodeText ||
                            originalResults[i].CodeTypeName != importedResults[i].CodeTypeName)
                        {
                            match = false;
                            break;
                        }
                    }
                }

                Console.WriteLine(match
                    ? "Success: Imported reader restored identical results."
                    : "Failure: Imported reader results differ from original.");
            }
        }
        finally
        {
            // Cleanup temporary files and directory, ignoring any errors during deletion
            try { if (File.Exists(imagePath)) File.Delete(imagePath); } catch { }
            try { if (File.Exists(xmlPath)) File.Delete(xmlPath); } catch { }
            try { Directory.Delete(tempDir, true); } catch { }
        }
    }
}
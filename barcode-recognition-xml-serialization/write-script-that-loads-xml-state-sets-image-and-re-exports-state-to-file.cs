// Title: Load BarCodeReader XML state, set image, and re-export to XML
// Description: Demonstrates loading a BarCodeReader configuration from an XML file, assigning a barcode image, and exporting the updated state back to XML. Useful for persisting and reusing reader settings.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to export and import BarCodeReader settings via XML. It uses BarcodeGenerator to create a sample image, BarCodeReader for decoding, and the ExportToXml/ImportFromXml APIs. Developers often need to save reader configurations, modify them programmatically, and reload them across sessions or environments.
// Prompt: Write a script that loads an XML state, sets an image, and re‑exports the state to a file.
// Tags: code128, xml, export, import, png, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, exports the BarCodeReader settings to XML,
/// imports the settings back, assigns the barcode image, and re‑exports the updated state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the full workflow of barcode generation,
    /// XML export/import, image assignment, and final XML export.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory to store generated files
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the barcode image and the XML state files
        string barcodeImagePath = Path.Combine(workDir, "barcode.png");
        string originalXmlPath = Path.Combine(workDir, "reader_original.xml");
        string updatedXmlPath = Path.Combine(workDir, "reader_updated.xml");

        // 1. Generate a simple Code128 barcode image that will be used for reading
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // 2. Create a BarCodeReader for the generated image and export its default settings to XML
        using (var reader = new BarCodeReader(barcodeImagePath, DecodeType.Code128))
        {
            // Export current reader settings into a memory stream
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position before copying

                // Write the XML content to the original XML file
                using (var fileStream = new FileStream(originalXmlPath, FileMode.Create, FileAccess.Write))
                {
                    xmlStream.CopyTo(fileStream);
                }
            }
        }

        // 3. Load the previously exported XML state into a new BarCodeReader instance
        BarCodeReader importedReader;
        using (var xmlFileStream = new FileStream(originalXmlPath, FileMode.Open, FileAccess.Read))
        {
            importedReader = BarCodeReader.ImportFromXml(xmlFileStream);
        }

        // 4. Assign the barcode image to the imported reader so it can perform decoding
        importedReader.SetBarCodeImage(barcodeImagePath);

        // 5. Re‑export the updated reader state (now with the image source set) to a new XML file
        using (importedReader)
        {
            using (var updatedXmlStream = new MemoryStream())
            {
                importedReader.ExportToXml(updatedXmlStream);
                updatedXmlStream.Position = 0; // Reset before writing to file

                using (var outFile = new FileStream(updatedXmlPath, FileMode.Create, FileAccess.Write))
                {
                    updatedXmlStream.CopyTo(outFile);
                }
            }
        }

        // Output the locations of the generated files for verification
        Console.WriteLine("Barcode image saved to: " + barcodeImagePath);
        Console.WriteLine("Original reader XML saved to: " + originalXmlPath);
        Console.WriteLine("Updated reader XML saved to: " + updatedXmlPath);
    }
}
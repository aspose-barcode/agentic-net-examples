// Title: Generate Codabar barcode, export to XML, modify checksum mode, re‑import and verify
// Description: Demonstrates creating a Codabar barcode, exporting its configuration to XML, changing the CodabarChecksumMode to Mod16, re‑importing the settings, and confirming checksum validation through reading.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, export and import settings via XML, adjust checksum modes, and employ BarCodeReader for validation. Developers working with one‑dimensional symbologies often need to persist generator configurations, modify checksum behavior, and verify encoded data programmatically.
// Prompt: Generate a barcode, export its XML, modify CodabarChecksumMode to Mod16, re‑import, and verify checksum calculation.
// Tags: codabar, checksum, xml, export, import, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Codabar barcode, manipulates its XML configuration,
/// re‑imports the settings, and validates the checksum using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, XML export/import,
    /// and checksum verification workflow.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for XML configuration and barcode images
        string xmlPath = Path.Combine(Path.GetTempPath(), "codabar.xml");
        string imgPath = Path.Combine(Path.GetTempPath(), "codabar.png");

        // 1. Create a Codabar barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum calculation (optional for Codabar but required for verification)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Export the generator's settings to an XML file for later modification
            generator.ExportToXml(xmlPath);

            // Save the generated barcode image (used later for checksum verification)
            generator.Save(imgPath, BarCodeImageFormat.Png);
        }

        // 2. Modify the exported XML to set CodabarChecksumMode to Mod16
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file was not created.");
            return;
        }

        string xmlContent = File.ReadAllText(xmlPath);

        // Replace any existing checksum mode with Mod16 (default is Mod16, but we enforce it)
        xmlContent = xmlContent.Replace("<CodabarChecksumMode>Mod10</CodabarChecksumMode>", "<CodabarChecksumMode>Mod16</CodabarChecksumMode>");

        // If the element was not present, add it under the Codabar settings
        if (!xmlContent.Contains("<CodabarChecksumMode>"))
        {
            // Simple insertion before the closing </Codabar> tag
            xmlContent = xmlContent.Replace("</Codabar>", "  <CodabarChecksumMode>Mod16</CodabarChecksumMode>\n</Codabar>");
        }

        // Write the updated XML back to the file system
        File.WriteAllText(xmlPath, xmlContent);

        // 3. Re‑import the barcode generator from the modified XML
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Ensure checksum remains enabled after import
            importedGenerator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the regenerated barcode image (optional, used for verification)
            string regeneratedImgPath = Path.Combine(Path.GetTempPath(), "codabar_regenerated.png");
            importedGenerator.Save(regeneratedImgPath, BarCodeImageFormat.Png);

            // 4. Verify checksum calculation by reading the regenerated barcode
            using (var reader = new BarCodeReader(regeneratedImgPath, DecodeType.Codabar))
            {
                // Enable checksum validation during reading
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all detected barcodes (should be only one)
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    // For Codabar, checksum is available in the OneD extended parameters
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // Clean up temporary files (optional)
        try { File.Delete(xmlPath); } catch { }
        try { File.Delete(imgPath); } catch { }
    }
}
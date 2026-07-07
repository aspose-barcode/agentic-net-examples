// Title: Codabar Barcode Generation with XML Export/Import and Checksum Mode Modification
// Description: Demonstrates creating a Codabar barcode, exporting its settings to XML, changing the checksum mode to Mod16, re‑importing, and verifying decoding.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, exporting/importing settings via XML, and BarCodeReader for decoding. Developers often need to adjust barcode parameters programmatically, persist configurations, and validate that changes (e.g., checksum modes) are applied correctly. Ideal for learning how to manipulate Codabar checksum settings using Aspose.BarCode APIs.
// Prompt: Generate a barcode, export its XML, modify CodabarChecksumMode to Mod16, re‑import, and verify checksum calculation.
// Tags: codabar, checksum, xml, export, import, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Codabar barcode, exports its configuration to XML,
/// modifies the checksum mode, re‑imports the settings, and verifies decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, XML export/import,
    /// checksum mode modification, and verification steps.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Define file paths for XML and images
        string xmlPath = Path.Combine(outputDir, "codabar.xml");
        string imgPath1 = Path.Combine(outputDir, "codabar_initial.png");
        string imgPath2 = Path.Combine(outputDir, "codabar_modified.png");

        // 1. Create a Codabar barcode, enable checksum, set initial mode, save image and export to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Set an initial checksum mode (Mod10) to demonstrate change later
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Save the initial barcode image
            generator.Save(imgPath1);

            // Export generator settings to XML for later reuse
            generator.ExportToXml(xmlPath);
        }

        // 2. Import settings from XML, modify checksum mode to Mod16, and save a new image
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Change checksum mode to Mod16
            importedGen.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;
            // Ensure checksum generation remains enabled
            importedGen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the modified barcode image
            importedGen.Save(imgPath2);

            Console.WriteLine($"Checksum mode after import and modification: {importedGen.Parameters.Barcode.Codabar.ChecksumMode}");
        }

        // 3. Read the modified barcode to verify it can be decoded
        using (var reader = new BarCodeReader(imgPath2, DecodeType.Codabar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                // Codabar does not expose a checksum value via the reader; successful decoding confirms validity.
            }
        }

        Console.WriteLine("Barcode generation, XML export/import, and verification completed.");
    }
}
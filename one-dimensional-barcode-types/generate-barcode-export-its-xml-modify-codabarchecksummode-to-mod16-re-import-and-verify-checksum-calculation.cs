// Title: Codabar Barcode Generation, XML Export/Import, and Checksum Mode Verification
// Description: Demonstrates generating a Codabar barcode with checksum enabled, exporting its configuration to XML, modifying the checksum mode, re‑importing the settings, and confirming the checksum calculation by decoding the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to use BarcodeGenerator for creating barcodes, exporting and importing generator settings via XML, and employing BarCodeReader for decoding. Developers often need to persist barcode configurations, adjust parameters like CodabarChecksumMode, and validate the resulting barcodes in automated workflows.
// Prompt: Generate a barcode, export its XML, modify CodabarChecksumMode to Mod16, re‑import, and verify checksum calculation.
// Tags: codabar,checksum,xml,export,import,barcode generation,barcode recognition,aspose.barcode

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Codabar barcode, exports its settings to XML,
/// modifies the checksum mode, re‑imports the configuration, and validates the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the barcode generation, XML manipulation, re‑import, and verification steps.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary working directory for all generated files.
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "CodabarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for original and modified barcode images and XML files.
        string imagePath1 = Path.Combine(workDir, "codabar_original.png");
        string imagePath2 = Path.Combine(workDir, "codabar_modified.png");
        string xmlPath = Path.Combine(workDir, "codabar_state.xml");
        string xmlModifiedPath = Path.Combine(workDir, "codabar_state_modified.xml");

        // ------------------------------------------------------------
        // Step 1: Generate a Codabar barcode with checksum enabled (Mod16) and export its state to XML.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the barcode image and export the generator configuration.
            generator.Save(imagePath1, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Step 2: Load the exported XML and ensure the ChecksumMode element is set to "Mod16".
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        var checksumModeElement = doc.Descendants("ChecksumMode").FirstOrDefault();
        if (checksumModeElement != null)
        {
            checksumModeElement.Value = "Mod16";
        }
        else
        {
            // If the element is missing, add it under the Codabar node.
            var codabarNode = doc.Descendants("Codabar").FirstOrDefault();
            if (codabarNode != null)
            {
                codabarNode.Add(new XElement("ChecksumMode", "Mod16"));
            }
        }
        doc.Save(xmlModifiedPath);

        // ------------------------------------------------------------
        // Step 3: Import the modified XML, verify the checksum mode, and generate a new barcode image.
        // ------------------------------------------------------------
        using (var genFromXml = BarcodeGenerator.ImportFromXml(xmlModifiedPath))
        {
            // Output the imported checksum mode for verification.
            var mode = genFromXml.Parameters.Barcode.Codabar.ChecksumMode;
            Console.WriteLine($"Imported ChecksumMode: {mode}");

            // Save the barcode generated from the imported settings.
            genFromXml.Save(imagePath2, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Step 4: Decode the modified barcode image and display the decoded text.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath2))
        {
            Console.WriteLine("Modified barcode image not found.");
            return;
        }

        using (var reader = new BarCodeReader(imagePath2, DecodeType.Codabar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the temporary working directory.
        // Directory.Delete(workDir, true);
    }
}
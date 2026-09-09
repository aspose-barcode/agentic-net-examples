// Title: Codabar Barcode Generation with XML Modification of Checksum Mode
// Description: Shows how to generate a Codabar barcode with a Mod10 checksum, export its configuration to XML, change the checksum mode to Mod16, re‑import the settings, and verify the barcode can be decoded correctly.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator, its Parameters, and BarCodeReader classes. Developers often need to persist barcode settings, edit them programmatically (e.g., change checksum modes), and reload them for further processing. The snippet demonstrates exporting to XML, editing configuration, importing back, and validating the result—common tasks when integrating barcode generation into automated workflows.
// Prompt: Generate a barcode, export its XML, modify CodabarChecksumMode to Mod16, re‑import, and verify checksum calculation.
// Tags: codabar, checksum, xml, barcode generation, barcode recognition, aspose.barcode, export, import

using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Codabar barcode, exporting its configuration to XML,
/// modifying the checksum mode, re‑importing the settings, and verifying decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the barcode generation, XML manipulation, re‑import, and decoding steps.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for XML and PNG images
        string xmlPath = Path.Combine(workDir, "barcode.xml");
        string imgPath = Path.Combine(workDir, "barcode.png");
        string imgMod16Path = Path.Combine(workDir, "barcode_mod16.png");

        // Step 1: Generate Codabar barcode with Mod10 checksum and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;
            generator.Save(imgPath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML and modify the CodabarChecksumMode to Mod16
        var doc = XDocument.Load(xmlPath);
        var modeElement = doc.Descendants("CodabarChecksumMode").FirstOrDefault();
        if (modeElement != null)
        {
            modeElement.Value = "Mod16";
        }
        else
        {
            // If the element does not exist, add it under the Barcode element
            var barcodeElem = doc.Descendants("Barcode").FirstOrDefault();
            if (barcodeElem != null)
            {
                barcodeElem.Add(new XElement("CodabarChecksumMode", "Mod16"));
            }
        }
        doc.Save(xmlPath);

        // Step 3: Import the modified XML, verify the checksum mode, and generate a new image
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            var importedMode = importedGen.Parameters.Barcode.Codabar.ChecksumMode;
            Console.WriteLine($"Imported checksum mode: {importedMode}");

            importedGen.Save(imgMod16Path, BarCodeImageFormat.Png);
        }

        // Step 4: Read the newly generated barcode and display the decoded text
        using (var reader = new BarCodeReader(imgMod16Path))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the temporary working directory
        // Directory.Delete(workDir, true);
    }
}
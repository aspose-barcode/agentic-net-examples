// Title: Codabar Checksum Mode Modification via XML
// Description: Demonstrates generating a Codabar barcode with Mod10 checksum, exporting its configuration to XML, changing the checksum mode to Mod16, re‑importing the settings, and verifying that the checksum digits differ.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator to create barcodes, export and import configuration via XML, and manipulate checksum settings. Typical use cases include dynamic barcode configuration, batch processing, and validation of checksum algorithms. Developers often need to adjust barcode parameters programmatically and verify the impact on encoded data.
// Prompt: Generate a barcode, export its XML, modify CodabarChecksumMode to Mod16, re‑import, and verify checksum calculation.
// Tags: codabar, checksum, xml, export, import, barcode-generation, barcode-recognition, aspose.barcode

using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Codabar barcode, modifies its checksum mode via XML,
/// re‑generates the barcode, and validates the checksum change.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, XML manipulation,
    /// re‑import, and verification steps.
    /// </summary>
    static void Main()
    {
        // Prepare temporary directory and file paths for the generated images and XML configuration.
        string tempDir = Path.Combine(Path.GetTempPath(), "CodabarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string mod10Path = Path.Combine(tempDir, "codabar_mod10.png");
        string mod16Path = Path.Combine(tempDir, "codabar_mod16.png");
        string xmlPath = Path.Combine(tempDir, "codabar_config.xml");

        // Step 1: Generate a Codabar barcode with Mod10 checksum and export its configuration to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;
            generator.Save(mod10Path, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML, locate the checksum mode element, and change it to Mod16.
        XDocument doc = XDocument.Load(xmlPath);
        var checksumElement = doc.Descendants()
                                 .FirstOrDefault(e => e.Name.LocalName.Equals("CodabarChecksumMode", StringComparison.OrdinalIgnoreCase) ||
                                                      e.Name.LocalName.Equals("ChecksumMode", StringComparison.OrdinalIgnoreCase));
        if (checksumElement != null)
        {
            checksumElement.Value = "Mod16";
        }
        doc.Save(xmlPath);

        // Step 3: Import the modified XML configuration and generate a new barcode using Mod16 checksum.
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            importedGen.Save(mod16Path, BarCodeImageFormat.Png);
        }

        // Step 4: Decode both barcodes and compare the resulting code texts to verify checksum differences.
        string decodedMod10 = DecodeBarcode(mod10Path);
        string decodedMod16 = DecodeBarcode(mod16Path);

        Console.WriteLine($"Decoded (Mod10): {decodedMod10}");
        Console.WriteLine($"Decoded (Mod16): {decodedMod16}");

        if (!string.IsNullOrEmpty(decodedMod10) && !string.IsNullOrEmpty(decodedMod16) && decodedMod10 != decodedMod16)
        {
            Console.WriteLine("Checksum verification succeeded: checksum digits differ as expected.");
        }
        else
        {
            Console.WriteLine("Checksum verification failed: decoded texts are identical or missing.");
        }
    }

    /// <summary>
    /// Decodes a barcode image using Aspose.BarCode's <see cref="BarCodeReader"/> and returns the first detected code text.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    /// <returns>The decoded code text, or <c>null</c> if decoding fails.</returns>
    static string DecodeBarcode(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return null;
        }

        // Specify the expected barcode type for decoding.
        BaseDecodeType decodeType = DecodeType.Codabar;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Return the first decoded result.
            foreach (var result in reader.ReadBarCodes())
            {
                return result.CodeText;
            }
        }
        return null;
    }
}
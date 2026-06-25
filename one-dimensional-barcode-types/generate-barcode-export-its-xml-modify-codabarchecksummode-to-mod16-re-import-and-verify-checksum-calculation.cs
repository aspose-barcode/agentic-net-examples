using System;
using System.IO;
using System.Xml;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Codabar barcodes with different checksum modes,
/// exporting/importing configuration via XML, and reading the resulting barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for generated images and XML configuration files.
        string imageMod10Path = "codabar_mod10.png";
        string xmlPath = "codabar.xml";
        string xmlMod16Path = "codabar_mod16.xml";
        string imageMod16Path = "codabar_mod16.png";

        // ------------------------------------------------------------
        // 1. Generate a Codabar barcode using the default checksum mode (Mod10).
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Explicitly set checksum mode to Mod10 (default, but set for clarity).
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Save the barcode image to file.
            generator.Save(imageMod10Path);

            // Export the generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // 2. Load the exported XML, modify the checksum mode to Mod16, and save as a new XML file.
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML not found.");
            return;
        }

        var doc = new XmlDocument();
        doc.Load(xmlPath);

        // Locate the <CodabarChecksumMode> element in the XML.
        XmlNode? checksumNode = doc.SelectSingleNode("//CodabarChecksumMode");
        if (checksumNode == null)
        {
            Console.WriteLine("CodabarChecksumMode element not found in XML.");
            return;
        }

        // Change the checksum mode to Mod16 (enum value 1) and save the modified XML.
        checksumNode.InnerText = ((int)CodabarChecksumMode.Mod16).ToString();
        doc.Save(xmlMod16Path);

        // ------------------------------------------------------------
        // 3. Import the modified XML to create a new barcode generator instance.
        // ------------------------------------------------------------
        if (!File.Exists(xmlMod16Path))
        {
            Console.WriteLine("Modified XML not found.");
            return;
        }

        // Import configuration from the modified XML file.
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlMod16Path);

        // Save the barcode image generated with Mod16 checksum mode.
        importedGenerator.Save(imageMod16Path);

        // ------------------------------------------------------------
        // 4. Verify checksum calculation by reading the barcode image with Mod16.
        // ------------------------------------------------------------
        Console.WriteLine("Reading barcode with Mod16 checksum mode:");
        using (var reader = new BarCodeReader(imageMod16Path, DecodeType.Codabar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum (Mod16): {result.Extended.OneD.CheckSum}");
            }
        }

        // ------------------------------------------------------------
        // Optional: read the original barcode (Mod10) for comparison.
        // ------------------------------------------------------------
        Console.WriteLine("\nReading original barcode with Mod10 checksum mode:");
        using (var reader = new BarCodeReader(imageMod10Path, DecodeType.Codabar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum (Mod10): {result.Extended.OneD.CheckSum}");
            }
        }
    }
}
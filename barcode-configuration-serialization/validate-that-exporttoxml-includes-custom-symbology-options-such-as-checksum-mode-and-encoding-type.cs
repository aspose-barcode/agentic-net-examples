// Title: Export barcode generation settings to XML with custom symbology options
// Description: Demonstrates exporting barcode generator state to XML files, including custom symbology settings such as Codabar checksum mode and QR code ECI encoding.
// Category-Description: Shows how to use Aspose.BarCode Generation API to configure symbology‑specific options and persist them via ExportToXml. Typical use cases include saving barcode configuration for later reuse, auditing, or integration with external systems. Key classes: BarcodeGenerator, EncodeTypes, CodabarChecksumMode, ECIEncodings. Developers often need to validate that custom options are correctly serialized.
// Prompt: Validate that ExportToXml includes custom symbology options such as checksum mode and encoding type.
// Tags: barcode, symbology, export, xml, checksum, encoding, aspose.barcode, generation

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting barcode generation settings to XML, including custom symbology options.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes, sets custom options, exports to XML, and validates the output.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ExportXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the exported XML files
        string codabarXmlPath = Path.Combine(tempFolder, "codabar.xml");
        string qrXmlPath = Path.Combine(tempFolder, "qr.xml");

        // ---------- Codabar barcode with custom checksum mode ----------
        using (var codabarGen = new BarcodeGenerator(EncodeTypes.Codabar, "A123B"))
        {
            // Set a custom checksum mode (example: Mod10)
            codabarGen.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Export generation state to XML
            codabarGen.ExportToXml(codabarXmlPath);
        }

        // ---------- QR barcode with custom encoding type ----------
        using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set ECI encoding to UTF-8
            qrGen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Export generation state to XML
            qrGen.ExportToXml(qrXmlPath);
        }

        // ---------- Validate exported XML for custom options ----------
        ValidateXmlOption(codabarXmlPath, "ChecksumMode", "Mod10");
        ValidateXmlOption(qrXmlPath, "ECIEncoding", "UTF8");

        // Clean up temporary files (optional)
        try
        {
            File.Delete(codabarXmlPath);
            File.Delete(qrXmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is best‑effort
        }
    }

    static void ValidateXmlOption(string xmlPath, string elementName, string expectedValue)
    {
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            XDocument doc = XDocument.Load(xmlPath);
            var element = doc.Descendants(elementName).FirstOrDefault();
            if (element != null && string.Equals(element.Value, expectedValue, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Validation succeeded: <{elementName}> = {expectedValue}");
            }
            else
            {
                string actual = element != null ? element.Value : "missing";
                Console.WriteLine($"Validation failed for {Path.GetFileName(xmlPath)}: expected <{elementName}> = {expectedValue}, but found '{actual}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading XML file '{xmlPath}': {ex.Message}");
        }
    }
}
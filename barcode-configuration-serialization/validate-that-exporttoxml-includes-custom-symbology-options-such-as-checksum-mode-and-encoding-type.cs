// Title: Export barcode configuration to XML with custom QR symbology options
// Description: Demonstrates generating a QR barcode with checksum and ECI encoding, exporting its configuration to XML, and verifying the custom settings in the XML.
// Category-Description: This example belongs to the Aspose.BarCode generation and export category. It showcases the use of BarcodeGenerator, setting symbology‑specific options (such as checksum mode and encoding type), and persisting those settings with ExportToXml. Developers working with barcode creation, configuration persistence, or automated validation of barcode parameters will find this pattern useful for testing and documentation purposes.
// Prompt: Validate that ExportToXml includes custom symbology options such as checksum mode and encoding type.
// Tags: barcode, symbology, export, xml, qrcode, checksum, encoding, aspose.barcode, generation

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting barcode generation settings to XML and validating custom QR symbology options.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode with checksum and ECI encoding, exports settings to XML,
    /// and checks that the XML contains the expected values.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "ExportXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the exported XML configuration
        string xmlPath = Path.Combine(tempFolder, "barcodeConfig.xml");

        // Generate a QR barcode with custom checksum and encoding settings
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Enable checksum (applies to all symbologies)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set QR specific encoding mode to ECI and choose UTF-8 encoding
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Export the configuration to XML
            generator.ExportToXml(xmlPath);
        }

        // Load the exported XML and verify that the custom options are present
        XDocument doc = XDocument.Load(xmlPath);

        bool checksumEnabled = false;
        bool encodeModeEci = false;
        bool eciEncodingUtf8 = false;

        // Check for checksum setting
        foreach (var elem in doc.Descendants("IsChecksumEnabled"))
        {
            if (elem.Value.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                checksumEnabled = true;
                break;
            }
        }

        // Check for QR encode mode set to ECI
        foreach (var elem in doc.Descendants("EncodeMode"))
        {
            if (elem.Value.Equals("ECI", StringComparison.OrdinalIgnoreCase))
            {
                encodeModeEci = true;
                break;
            }
        }

        // Check for QR ECI encoding set to UTF8
        foreach (var elem in doc.Descendants("ECIEncoding"))
        {
            if (elem.Value.Equals("UTF8", StringComparison.OrdinalIgnoreCase))
            {
                eciEncodingUtf8 = true;
                break;
            }
        }

        // Output validation results
        Console.WriteLine("Exported XML validation results:");
        Console.WriteLine($"Checksum enabled: {(checksumEnabled ? "Found" : "Missing")}");
        Console.WriteLine($"QR EncodeMode set to ECI: {(encodeModeEci ? "Found" : "Missing")}");
        Console.WriteLine($"QR ECIEncoding set to UTF8: {(eciEncodingUtf8 ? "Found" : "Missing")}");

        // Clean up temporary files (optional)
        try
        {
            File.Delete(xmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
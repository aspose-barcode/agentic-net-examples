// Title: Export QR Barcode to XML with Custom Symbology Options
// Description: Demonstrates exporting a QR barcode generation configuration to XML and verifying that custom symbology settings like checksum mode and encoding type are included.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to configure barcode parameters (checksum, encoding), generate a barcode, and export the full generation state to an XML document using the ExportToXml API. Developers often need to persist or transmit barcode settings for later reuse, auditing, or integration with other systems.
// Prompt: Validate that ExportToXml includes custom symbology options such as checksum mode and encoding type.
// Tags: barcode symbology, export, xml, checksum, encoding, qrcode, aspose.barcode

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR barcode, applies custom symbology options,
/// exports the generation state to XML, and validates that the options are present
/// in the resulting XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML export, and validation.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for any output files (not strictly needed here)
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Initialize a QR barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            // Enable checksum (generic property; may be ignored for QR)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set QR‑specific encoding (ECI) to UTF‑8
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Export the generation state to XML in a memory stream
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading

                // Read the XML content without closing the stream
                using (var reader = new StreamReader(xmlStream, leaveOpen: true))
                {
                    string xmlContent = reader.ReadToEnd();

                    // Parse XML into an XDocument for querying
                    XDocument doc = XDocument.Parse(xmlContent);

                    // Validate presence of checksum setting in the XML
                    bool hasChecksum = doc.Descendants()
                        .Any(e => e.Name.LocalName.Equals("IsChecksumEnabled", StringComparison.OrdinalIgnoreCase) ||
                                  (e.Name.LocalName.Equals("ChecksumEnabled", StringComparison.OrdinalIgnoreCase) && e.Value.Equals("Yes", StringComparison.OrdinalIgnoreCase)));

                    // Validate presence of encoding setting in the XML
                    bool hasEncoding = doc.Descendants()
                        .Any(e => e.Name.LocalName.Equals("ECIEncoding", StringComparison.OrdinalIgnoreCase) ||
                                  e.Value.Equals("UTF8", StringComparison.OrdinalIgnoreCase));

                    Console.WriteLine("Exported XML contains checksum option: " + hasChecksum);
                    Console.WriteLine("Exported XML contains encoding option: " + hasEncoding);
                }
            }
        }

        // Clean up temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting and importing barcode generator settings to/from XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder to store generated XML files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempFolder);

        // -------------------------------------------------
        // Example 1: Code39 barcode with checksum enabled
        // -------------------------------------------------
        string code39XmlPath = Path.Combine(tempFolder, "code39.xml");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
        {
            // Enable checksum for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Export the generator's configuration to an XML file.
            generator.ExportToXml(code39XmlPath);
        }

        // Import the configuration from the XML file and verify that checksum is enabled.
        var loadedCode39 = BarcodeGenerator.ImportFromXml(code39XmlPath);
        bool checksumEnabled = loadedCode39.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
        Console.WriteLine($"Code39 checksum enabled after import: {checksumEnabled}");

        // -------------------------------------------------
        // Example 2: QR code with UTF-8 ECI encoding
        // -------------------------------------------------
        string qrXmlPath = Path.Combine(tempFolder, "qr.xml");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Set the ECI (Extended Channel Interpretation) encoding to UTF-8.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Export the generator's configuration to an XML file.
            generator.ExportToXml(qrXmlPath);
        }

        // Import the configuration from the XML file and verify that the ECI encoding is UTF-8.
        var loadedQr = BarcodeGenerator.ImportFromXml(qrXmlPath);
        bool eciIsUtf8 = loadedQr.Parameters.Barcode.QR.ECIEncoding == ECIEncodings.UTF8;
        Console.WriteLine($"QR ECI encoding is UTF-8 after import: {eciIsUtf8}");

        // Clean up temporary files (optional). Errors during cleanup are ignored.
        try
        {
            File.Delete(code39XmlPath);
            File.Delete(qrXmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored
        }
    }
}
// Title: ExportToXml with custom symbology options demonstration
// Description: Shows how to export barcode generator settings, including checksum mode and encoding type, to XML and then import them back.
// Prompt: Validate that ExportToXml includes custom symbology options such as checksum mode and encoding type.
// Tags: barcode symbology, export, xml, checksum, encoding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting barcode generation settings (including custom symbology options)
/// to XML and importing them back to verify that the options are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes export/import validation for Codabar and QR symbologies.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for XML files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // ---------- Codabar with checksum mode ----------
        string codabarXml = Path.Combine(tempDir, "codabar.xml");
        using (var codabarGen = new BarcodeGenerator(EncodeTypes.Codabar, "A123B"))
        {
            // Set custom checksum mode (Mod10) for Codabar
            codabarGen.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Export the generator settings to an XML file
            bool exported = codabarGen.ExportToXml(codabarXml);
            Console.WriteLine($"Codabar ExportToXml success: {exported}");
        }

        // Import the Codabar settings from XML and validate the checksum mode
        using (var importedCodabar = BarcodeGenerator.ImportFromXml(codabarXml))
        {
            var mode = importedCodabar.Parameters.Barcode.Codabar.ChecksumMode;
            Console.WriteLine($"Imported Codabar ChecksumMode: {mode}");
        }

        // ---------- QR with ECI encoding ----------
        string qrXml = Path.Combine(tempDir, "qr.xml");
        using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
        {
            // Set QR encoding mode to ECI and specify UTF-8 as the ECI encoding
            qrGen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;
            qrGen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Export the QR generator settings to an XML file
            bool exported = qrGen.ExportToXml(qrXml);
            Console.WriteLine($"QR ExportToXml success: {exported}");
        }

        // Import the QR settings from XML and validate the encoding options
        using (var importedQr = BarcodeGenerator.ImportFromXml(qrXml))
        {
            var encodeMode = importedQr.Parameters.Barcode.QR.EncodeMode;
            var eci = importedQr.Parameters.Barcode.QR.ECIEncoding;
            Console.WriteLine($"Imported QR EncodeMode: {encodeMode}");
            Console.WriteLine($"Imported QR ECIEncoding: {eci}");
        }

        // Cleanup temporary files (optional)
        try
        {
            File.Delete(codabarXml);
            File.Delete(qrXml);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}
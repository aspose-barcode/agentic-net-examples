// Title: Export barcode generation settings to XML with custom symbology options
// Description: Demonstrates how to generate Codabar and QR barcodes with custom checksum and encoding settings, then export the generator state to XML files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and parameter customization. Developers often need to persist barcode configuration, such as checksum modes or ECI encoding, for later reuse or auditing. ExportToXml provides a convenient way to serialize these settings into a readable XML format.
// Prompt: Validate that ExportToXml includes custom symbology options such as checksum mode and encoding type.
// Tags: barcode symbology, export, xml, checksum, encoding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting barcode generation parameters, including custom symbology options,
/// to XML files using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates Codabar and QR barcodes with custom settings,
    /// exports their configuration to XML, validates the exported content, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the exported XML files
        string codabarXml = Path.Combine(tempDir, "codabar.xml");
        string qrXml = Path.Combine(tempDir, "qr.xml");

        // -------------------------------------------------
        // Generate Codabar barcode with a custom checksum mode
        // -------------------------------------------------
        using (var codabarGen = new BarcodeGenerator(EncodeTypes.Codabar, "A123B"))
        {
            // Set Mod10 checksum mode for Codabar
            codabarGen.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;
            // Enable checksum generation
            codabarGen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save an image to ensure the generator is fully initialized (optional)
            string codabarImg = Path.Combine(tempDir, "codabar.png");
            codabarGen.Save(codabarImg, BarCodeImageFormat.Png);

            // Export the generator's state, including custom settings, to XML
            codabarGen.ExportToXml(codabarXml);
        }

        // -------------------------------------------------
        // Generate QR barcode with a custom ECI encoding type
        // -------------------------------------------------
        using (var qrGen = new BarcodeGenerator(EncodeTypes.QR, "Sample QR"))
        {
            // Use ECI encoding mode and specify UTF-8 as the character set
            qrGen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            qrGen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save an image to ensure the generator is fully initialized (optional)
            string qrImg = Path.Combine(tempDir, "qr.png");
            qrGen.Save(qrImg, BarCodeImageFormat.Png);

            // Export the generator's state, including custom encoding settings, to XML
            qrGen.ExportToXml(qrXml);
        }

        // -------------------------------------------------
        // Validate that the exported XML files contain the expected custom options
        // -------------------------------------------------
        bool codabarValid = false;
        bool qrValid = false;

        if (File.Exists(codabarXml))
        {
            string codabarContent = File.ReadAllText(codabarXml);
            codabarValid = codabarContent.Contains("<ChecksumMode>") && codabarContent.Contains("Mod10");
        }

        if (File.Exists(qrXml))
        {
            string qrContent = File.ReadAllText(qrXml);
            qrValid = qrContent.Contains("<EncodeMode>") && qrContent.Contains("ECI") &&
                      qrContent.Contains("<ECIEncoding>") && qrContent.Contains("UTF8");
        }

        // Output validation results
        Console.WriteLine($"Codabar XML contains custom checksum mode: {codabarValid}");
        Console.WriteLine($"QR XML contains custom encoding type: {qrValid}");

        // -------------------------------------------------
        // Cleanup temporary files and directory (optional)
        // -------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}
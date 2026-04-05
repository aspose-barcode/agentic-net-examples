using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path for the exported XML file
        string xmlPath = "barcodeSettings.xml";

        // Create a barcode generator, set custom checksum and QR ECI options
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC-123"))
        {
            // Enable checksum and make it visible
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Set QR specific ECI encoding (stored even though symbology is Code39)
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECIEncoding;

            // Export the current settings to XML
            bool exported = generator.ExportToXml(xmlPath);
            Console.WriteLine("Exported to XML: " + exported);
        }

        // Import the settings from the XML file into a new generator instance
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Validate that checksum settings were preserved
            bool checksumEnabled = loadedGenerator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
            bool checksumShown = loadedGenerator.Parameters.Barcode.ChecksumAlwaysShow;

            // Validate that QR ECI settings were preserved
            bool eciEncodingSet = loadedGenerator.Parameters.Barcode.QR.ECIEncoding == ECIEncodings.UTF8;
            bool encodeModeSet = loadedGenerator.Parameters.Barcode.QR.EncodeMode == QREncodeMode.ECIEncoding;

            Console.WriteLine("Checksum Enabled: " + checksumEnabled);
            Console.WriteLine("Checksum Always Show: " + checksumShown);
            Console.WriteLine("QR ECI Encoding Set: " + eciEncodingSet);
            Console.WriteLine("QR Encode Mode ECI: " + encodeModeSet);
        }
    }
}
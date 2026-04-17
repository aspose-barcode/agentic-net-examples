using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string xmlPath = "barcodeSettings.xml";

        // Create a barcode generator (Codabar) and set custom options
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Set a QR specific encode mode (different from default) to be stored in XML
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

            bool exported = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Exported to XML: {exported}");
        }

        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file was not created.");
            return;
        }

        string xmlContent;
        using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8, true, 1024, leaveOpen: false))
            {
                xmlContent = reader.ReadToEnd();
            }
        }

        bool hasChecksumMode = xmlContent.Contains("ChecksumMode");
        bool hasEncodeMode = xmlContent.Contains("EncodeMode");

        Console.WriteLine($"XML contains ChecksumMode: {hasChecksumMode}");
        Console.WriteLine($"XML contains EncodeMode: {hasEncodeMode}");

        // Import the settings back into a new generator to confirm they are applied
        using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            if (importedGenerator == null)
            {
                Console.WriteLine("Failed to import generator from XML.");
                return;
            }

            bool importedChecksumEnabled = importedGenerator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes;
            bool importedChecksumMode = importedGenerator.Parameters.Barcode.Codabar.ChecksumMode == CodabarChecksumMode.Mod16;
            bool importedEncodeMode = importedGenerator.Parameters.Barcode.QR.EncodeMode == QREncodeMode.Binary;

            Console.WriteLine($"Imported IsChecksumEnabled: {importedChecksumEnabled}");
            Console.WriteLine($"Imported Codabar ChecksumMode: {importedChecksumMode}");
            Console.WriteLine($"Imported QR EncodeMode: {importedEncodeMode}");
        }

        File.Delete(xmlPath);
    }
}
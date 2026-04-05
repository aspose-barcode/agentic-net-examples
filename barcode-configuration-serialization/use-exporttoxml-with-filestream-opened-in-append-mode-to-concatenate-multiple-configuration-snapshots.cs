using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the XML file that will hold concatenated configuration snapshots
        const string configFile = "barcodeConfigs.xml";

        // First barcode configuration (Code128)
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Example of setting a parameter
            generator1.Parameters.Barcode.Code128.Code128EncodeMode = Code128EncodeMode.Auto;

            // Append the configuration to the XML file
            using (var stream = new FileStream(configFile, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                bool success = generator1.ExportToXml(stream);
                Console.WriteLine($"Export first config: {(success ? "Success" : "Failed")}");
            }
        }

        // Second barcode configuration (QR)
        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Example of setting a QR parameter
            generator2.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Append the second configuration to the same XML file
            using (var stream = new FileStream(configFile, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                bool success = generator2.ExportToXml(stream);
                Console.WriteLine($"Export second config: {(success ? "Success" : "Failed")}");
            }
        }

        // Optional: read back the first configuration to verify import works
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(configFile))
        {
            Console.WriteLine($"Imported barcode type: {importedGenerator.BarcodeType}");
            Console.WriteLine($"Imported code text: {importedGenerator.CodeText}");
        }
    }
}
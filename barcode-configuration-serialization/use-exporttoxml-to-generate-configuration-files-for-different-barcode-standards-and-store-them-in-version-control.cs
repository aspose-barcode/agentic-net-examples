using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define an array of barcode configurations to export.
        var configs = new (BaseEncodeType type, string codeText, string fileName)[]
        {
            (EncodeTypes.Code128, "ABC123", "Code128Config.xml"),
            (EncodeTypes.QR, "https://example.com", "QRConfig.xml"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "DataMatrixConfig.xml"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "Pdf417Config.xml"),
            (EncodeTypes.EAN13, "123456789012", "EAN13Config.xml")
        };

        foreach (var (type, codeText, fileName) in configs)
        {
            // Create a barcode generator for the specified type and set the code text.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Example of setting a size-related property using the correct unit member.
                // Here we set XDimension to 2 points for demonstration purposes.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Export the generator's configuration to an XML file.
                bool success = generator.ExportToXml(fileName);
                Console.WriteLine($"{fileName} export {(success ? "succeeded" : "failed")}.");
            }
        }

        // Indicate that the process has completed.
        Console.WriteLine("All barcode configuration files have been exported.");
    }
}
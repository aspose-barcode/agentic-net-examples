using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator, set its type and value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";

            // Export the generator settings to an XML stream
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);

                // Reset stream position for reading
                xmlStream.Position = 0;

                // Import a new generator instance from the XML stream
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Save the barcode image created from the imported settings
                    importedGenerator.Save("restored.png");
                }
            }
        }

        Console.WriteLine("Barcode image saved as 'restored.png'.");
    }
}
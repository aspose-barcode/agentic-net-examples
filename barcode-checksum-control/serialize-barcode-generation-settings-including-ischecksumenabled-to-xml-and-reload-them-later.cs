using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator, set code text and enable checksum
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456789";
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

            // Export the current settings (including checksum option) to XML
            generator.ExportToXml("barcodeSettings.xml");

            // Save the barcode image for reference
            generator.Save("original.png");
        }

        // Import the settings from the XML file into a new generator instance
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml("barcodeSettings.xml"))
        {
            // The loaded generator retains the checksum setting
            // Generate the barcode image and save it
            loadedGenerator.Save("loaded.png");
        }

        Console.WriteLine("Export and import completed. Images saved as original.png and loaded.png.");
    }
}
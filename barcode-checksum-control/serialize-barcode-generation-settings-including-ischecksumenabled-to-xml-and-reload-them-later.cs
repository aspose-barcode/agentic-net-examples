using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Export the current settings (including checksum option) to XML
            string xmlPath = "barcodeSettings.xml";
            generator.ExportToXml(xmlPath);
            Console.WriteLine($"Settings exported to {xmlPath}");

            // Save a barcode image using the original settings
            string imagePath = "originalBarcode.png";
            generator.Save(imagePath);
            Console.WriteLine($"Original barcode saved to {imagePath}");
        }

        // Import the settings from the XML file into a new generator instance
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml("barcodeSettings.xml"))
        {
            // Verify that the checksum setting was restored (optional)
            Console.WriteLine($"IsChecksumEnabled after import: {loadedGenerator.Parameters.Barcode.IsChecksumEnabled}");

            // Save a barcode image using the imported settings
            string loadedImagePath = "loadedBarcode.png";
            loadedGenerator.Save(loadedImagePath);
            Console.WriteLine($"Barcode with imported settings saved to {loadedImagePath}");
        }
    }
}
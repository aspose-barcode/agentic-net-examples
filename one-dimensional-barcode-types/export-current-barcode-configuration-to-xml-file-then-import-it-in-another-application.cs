using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define file paths
        string xmlPath = "barcodeConfig.xml";
        string imagePath = "importedBarcode.png";

        // Create a barcode generator, set code text and a sample parameter
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Example: set barcode bar color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Export the current configuration to an XML file
            bool exportSuccess = generator.ExportToXml(xmlPath);
            Console.WriteLine(exportSuccess
                ? $"Configuration exported to '{xmlPath}'."
                : $"Failed to export configuration to '{xmlPath}'.");
        }

        // Verify that the XML file was created before attempting import
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML configuration file '{xmlPath}' does not exist. Exiting.");
            return;
        }

        // Import the barcode configuration from the XML file
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import barcode configuration.");
            return;
        }

        // Use the imported generator to save the barcode image
        using (importedGenerator)
        {
            importedGenerator.Save(imagePath);
            Console.WriteLine($"Barcode image generated from imported configuration and saved to '{imagePath}'.");
        }
    }
}
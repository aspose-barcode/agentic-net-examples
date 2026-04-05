using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Paths for the XML configuration and output images
        string xmlConfigPath = "barcodeConfig.xml";
        string originalImagePath = "originalBarcode.png";
        string loadedImagePath = "loadedBarcode.png";

        // -----------------------------------------------------------------
        // Step 1: Create a barcode generator, configure it, and export to XML
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "123ABC";

            // Example of unit‑based settings (using .Point with float literals)
            generator.Parameters.Barcode.XDimension.Point = 2f;      // smallest bar width
            generator.Parameters.Barcode.BarHeight.Point = 40f;     // height of bars
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f; // no reduction

            // Optional image size settings (used when AutoSizeMode is set to Interpolation or Nearest)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image generated with the original settings
            generator.Save(originalImagePath);

            // Export the current configuration to an XML file for later reuse
            generator.ExportToXml(xmlConfigPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Import the saved XML configuration and generate the same barcode
        // -----------------------------------------------------------------
        // ImportFromXml creates a new BarcodeGenerator instance based on the XML file
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            if (generatorFromXml == null)
            {
                throw new InvalidOperationException("Failed to import barcode configuration from XML.");
            }

            // The imported generator already contains all settings, including the code text.
            // Save the barcode image generated from the imported configuration
            generatorFromXml.Save(loadedImagePath);
        }

        // Indicate completion
        Console.WriteLine("Barcode images generated:");
        Console.WriteLine($" - Original: {Path.GetFullPath(originalImagePath)}");
        Console.WriteLine($" - Loaded from XML: {Path.GetFullPath(loadedImagePath)}");
    }
}
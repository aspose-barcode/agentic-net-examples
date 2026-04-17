using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string configPath = "barcodeConfig.xml";
        const string outputPath = "generatedBarcode.png";

        // If the XML configuration does not exist, create a sample configuration.
        if (!File.Exists(configPath))
        {
            // Create a barcode generator with initial settings.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Example of setting some properties.
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Export the configuration to XML for later reuse.
                bool exported = generator.ExportToXml(configPath);
                Console.WriteLine(exported
                    ? $"Configuration exported to '{configPath}'."
                    : $"Failed to export configuration to '{configPath}'.");
            }
        }

        // Load the barcode generator from the saved XML configuration.
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file '{configPath}' not found. Cannot generate barcode.");
            return;
        }

        BarcodeGenerator loadedGenerator;
        try
        {
            loadedGenerator = BarcodeGenerator.ImportFromXml(configPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing XML configuration: {ex.Message}");
            return;
        }

        // Use the loaded generator to create and save the barcode image.
        using (loadedGenerator)
        {
            try
            {
                loadedGenerator.Save(outputPath);
                Console.WriteLine($"Barcode image generated and saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving barcode image: {ex.Message}");
            }
        }
    }
}
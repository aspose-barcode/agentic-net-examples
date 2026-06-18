using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates loading a barcode generator configuration from an XML file,
/// displaying its properties, and saving the generated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the path to the XML configuration file.
        string xmlPath = "barcodeConfig.xml";

        // Verify that the XML configuration file exists before attempting to load it.
        if (!File.Exists(xmlPath))
        {
            // Inform the user that the configuration file could not be found and exit.
            Console.WriteLine($"Configuration file not found: {xmlPath}");
            return;
        }

        // Load the barcode generator settings from the specified XML file.
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Output the loaded symbology type name to the console for verification.
            Console.WriteLine($"Loaded symbology: {generator.BarcodeType.TypeName}");

            // Output the loaded code text (data to encode) to the console.
            Console.WriteLine($"Loaded codetext: {generator.CodeText}");

            // Define the output file path for the generated barcode image.
            string outputPath = "generatedBarcode.png";

            // Save the barcode image using the loaded configuration.
            generator.Save(outputPath);

            // Notify the user that the barcode image has been saved successfully.
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}
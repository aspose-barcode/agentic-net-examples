// Title: Barcode appearance configuration export/import example
// Description: Demonstrates how to configure barcode appearance settings, export them to an XML file, and reuse them for generating barcodes.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing the use of BarcodeGenerator, its Parameters, and XML import/export APIs. Developers often need to persist barcode visual settings such as AutoSizeMode, XDimension, and padding for reuse across applications or environments. The snippet illustrates typical workflows for saving and loading these settings.
// Prompt: Design a configuration file format to store barcode appearance settings such as AutoSizeMode, XDimension, and padding values.
// Tags: barcode, configuration, autosizemode, xdimension, padding, export, import, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting and importing barcode appearance settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode, saves its appearance to XML, generates an image, then reloads the settings to create another barcode.
    /// </summary>
    static void Main()
    {
        // Define file paths for the configuration XML and generated images
        string xmlPath = "barcodeSettings.xml";
        string imagePath = "barcode.png";

        // -----------------------------------------------------------------
        // Create a barcode generator, configure appearance settings, and save
        // the configuration to an XML file.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Auto-size the barcode using interpolation mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the module size (XDimension) to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Apply uniform padding of 5 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Export the current settings to an XML configuration file
            generator.ExportToXml(xmlPath);

            // Save a sample barcode image using the configured settings
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Load the barcode appearance settings from the XML file and generate
        // a new barcode to demonstrate that the configuration is applied.
        // -----------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Change the encoded text to verify that settings are retained
                loadedGenerator.CodeText = "Loaded123";

                string loadedImagePath = "barcode_loaded.png";
                loadedGenerator.Save(loadedImagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated with loaded settings saved to {loadedImagePath}");
            }
        }
        else
        {
            Console.WriteLine($"Configuration file not found: {xmlPath}");
        }
    }
}
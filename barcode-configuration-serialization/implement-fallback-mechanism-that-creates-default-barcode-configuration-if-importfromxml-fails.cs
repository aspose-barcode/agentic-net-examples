// Title: Barcode generation with XML import and fallback to default configuration
// Description: Demonstrates loading barcode settings from an XML file and falling back to a default Code128 barcode when the import fails or the file is missing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator.ImportFromXml, configure barcode parameters, and handle errors gracefully. Developers often need to load barcode configurations from external files for dynamic generation, and require a reliable fallback to ensure production continuity. The snippet showcases key classes like BarcodeGenerator, EncodeTypes, and AutoSizeMode, useful for creating 1D barcodes in PNG format.
// Prompt: Implement a fallback mechanism that creates a default barcode configuration if ImportFromXml fails.
// Tags: barcode symbology, generation, png, importfromxml, fallback, default configuration, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode using configuration loaded from XML,
/// with a fallback to a default configuration when loading fails.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Attempts to import barcode settings from an XML file;
    /// if unsuccessful, creates a default barcode generator and saves the image.
    /// </summary>
    static void Main()
    {
        const string xmlPath = "barcodeConfig.xml";
        const string outputPath = "barcode.png";

        BarcodeGenerator generator;

        // Check if the XML configuration file exists
        if (File.Exists(xmlPath))
        {
            try
            {
                // Attempt to import generator settings from the XML file
                generator = BarcodeGenerator.ImportFromXml(xmlPath);
                Console.WriteLine("Barcode configuration loaded from XML.");
            }
            catch (Exception ex)
            {
                // Log the error and fall back to a default configuration
                Console.WriteLine($"ImportFromXml failed: {ex.Message}");
                Console.WriteLine("Falling back to default barcode configuration.");
                generator = CreateDefaultGenerator();
            }
        }
        else
        {
            // XML file not found; use the default configuration
            Console.WriteLine("XML configuration file not found. Using default barcode configuration.");
            generator = CreateDefaultGenerator();
        }

        // Save the generated barcode image to the specified path
        using (generator)
        {
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to '{outputPath}'.");
        }
    }

    // Creates a simple default barcode (Code128) with basic settings
    private static BarcodeGenerator CreateDefaultGenerator()
    {
        var gen = new BarcodeGenerator(EncodeTypes.Code128, "Default");
        // Set common barcode parameters
        gen.Parameters.Barcode.XDimension.Point = 2f;          // Module size
        gen.Parameters.Barcode.BarHeight.Point = 40f;         // Bar height for 1D barcodes
        gen.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
        gen.Parameters.BackColor = Aspose.Drawing.Color.White;
        gen.Parameters.AutoSizeMode = AutoSizeMode.None;
        gen.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
        gen.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
        return gen;
    }
}
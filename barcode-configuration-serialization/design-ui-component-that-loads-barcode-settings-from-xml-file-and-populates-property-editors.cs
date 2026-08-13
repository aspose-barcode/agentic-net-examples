// Title: Load barcode settings from XML and generate barcode image
// Description: Demonstrates loading barcode configuration from an XML file, displaying key properties, and generating a barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to import and export barcode generator settings via XML. It showcases key API classes such as BarcodeGenerator, BarcodeParameters, and BarCodeImageFormat, which developers commonly use to persist settings, customize symbology, and produce barcode images in various formats.
// Prompt: Design a UI component that loads barcode settings from an XML file and populates property editors.
// Tags: barcode, xml, configuration, generation, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that loads barcode settings from an XML file,
/// displays selected properties (simulating UI editors), and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the XML file that stores barcode settings.
        string xmlPath = "barcodeSettings.xml";

        // Path for the generated barcode image.
        string outputImage = "generatedBarcode.png";

        // --------------------------------------------------------------------
        // Create a sample XML settings file if it does not already exist.
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            // Initialize a BarcodeGenerator with Code128 symbology and sample text.
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Configure a few common barcode properties.
                sampleGenerator.Parameters.Barcode.XDimension.Point = 2f;      // Module size (points)
                sampleGenerator.Parameters.Barcode.BarHeight.Point = 40f;    // Bar height for 1D barcode (points)
                sampleGenerator.Parameters.Barcode.BarColor = Color.Blue;   // Foreground color
                sampleGenerator.Parameters.BackColor = Color.White;          // Background color
                sampleGenerator.Parameters.Barcode.FilledBars = false;      // No filled bars

                // Export the configured settings to an XML file for later reuse.
                sampleGenerator.ExportToXml(xmlPath);
                Console.WriteLine($"Sample XML settings created at '{xmlPath}'.");
            }
        }

        // --------------------------------------------------------------------
        // Load barcode settings from the XML file and display them.
        // --------------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Simulate property editors by writing key settings to the console.
            Console.WriteLine("=== Loaded Barcode Settings ===");
            Console.WriteLine($"Symbology      : {generator.BarcodeType.TypeName}");
            Console.WriteLine($"CodeText       : {generator.CodeText}");
            Console.WriteLine($"XDimension (pt): {generator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"BarHeight (pt) : {generator.Parameters.Barcode.BarHeight.Point}");
            Console.WriteLine($"BarColor       : {generator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"BackColor      : {generator.Parameters.BackColor}");
            Console.WriteLine($"FilledBars     : {generator.Parameters.Barcode.FilledBars}");
            Console.WriteLine();

            // Generate the barcode image using the loaded settings.
            generator.Save(outputImage, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image generated and saved as '{outputImage}'.");
        }

        // Program ends successfully.
    }
}
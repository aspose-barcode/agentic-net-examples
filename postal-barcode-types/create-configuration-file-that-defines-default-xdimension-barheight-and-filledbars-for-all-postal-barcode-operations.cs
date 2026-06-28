using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a POSTNET barcode and exporting its configuration to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode with predefined settings and saves its configuration.
    /// </summary>
    static void Main()
    {
        // Default barcode parameters
        const float defaultXDimension = 2f;   // module width in points
        const float defaultBarHeight = 40f;   // bar height in points
        const bool defaultFilledBars = true; // bars are filled

        // Initialize the barcode generator with POSTNET type and data "12345"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set the X dimension (module width)
            generator.Parameters.Barcode.XDimension.Point = defaultXDimension;

            // Disable auto sizing and set a fixed bar height
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = defaultBarHeight;

            // Specify whether the bars should be filled
            generator.Parameters.Barcode.FilledBars = defaultFilledBars;

            // Build the full path for the XML configuration file
            string configPath = Path.Combine(Directory.GetCurrentDirectory(), "postal_defaults.xml");

            // Export the current generator settings to the XML file
            generator.ExportToXml(configPath);
        }
    }
}
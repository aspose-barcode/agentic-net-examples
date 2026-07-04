// Title: Barcode Generation with XML Import and Fallback
// Description: Demonstrates loading a barcode configuration from an XML file and falling back to a default configuration when the import fails.
// Prompt: Implement a fallback mechanism that creates a default barcode configuration if ImportFromXml fails.
// Tags: barcode symbology, import, fallback, xml, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that tries to generate a barcode from an XML configuration file.
/// If the import fails, it creates a default barcode configuration as a fallback.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the XML configuration file.
        const string xmlPath = "barcodeConfig.xml";

        // Check whether the XML file exists before attempting import.
        if (File.Exists(xmlPath))
        {
            try
            {
                // ImportFromXml creates a BarcodeGenerator instance based on the XML settings.
                using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Save the generated barcode image to a file.
                    generator.Save("imported.png");
                    Console.WriteLine("Barcode generated from XML configuration.");
                }

                // Import succeeded; exit the method early.
                return;
            }
            catch (Exception ex)
            {
                // Log the exception and continue to the fallback logic.
                Console.WriteLine($"ImportFromXml failed: {ex.Message}");
            }
        }
        else
        {
            // XML file not found – inform the user and proceed with default settings.
            Console.WriteLine("XML configuration file not found. Using default settings.");
        }

        // ---------- Fallback section ----------
        // Create a default barcode generator with a hard‑coded symbology and value.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Fallback123"))
        {
            // Set a few default visual parameters.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the fallback barcode image.
            generator.Save("fallback.png");
            Console.WriteLine("Default barcode generated as fallback.");
        }
    }
}
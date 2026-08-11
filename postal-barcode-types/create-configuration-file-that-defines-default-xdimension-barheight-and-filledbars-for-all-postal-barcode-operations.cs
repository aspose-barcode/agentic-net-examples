// Title: Generate Default Configuration XML for Postal Barcodes
// Description: Creates XML configuration files that set default XDimension, BarHeight, and FilledBars values for supported postal barcode symbologies.
// Category-Description: This example belongs to the Aspose.BarCode configuration generation category. It demonstrates using the BarcodeGenerator class to apply common settings across multiple postal symbologies, export those settings to XML, and manage output files. Developers working with postal barcodes often need to standardize dimensions and visual properties, and this pattern shows how to automate that process for reuse in larger applications.
// Prompt: Create a configuration file that defines default XDimension, BarHeight, and FilledBars for all postal barcode operations.
// Tags: postal barcode, configuration, xdimension, barheight, filledbars, aspose.barcode, xml export, c#

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate XML configuration files with default settings for postal barcode symbologies using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates default configuration files for each supported postal symbology.
    /// </summary>
    static void Main()
    {
        // Default values applied to all postal barcode operations
        const float defaultXDimension = 2f;   // module size in points
        const float defaultBarHeight = 50f;   // height in points
        const bool defaultFilledBars = false; // bars not filled by default

        // List of postal symbologies supported by Aspose.BarCode
        var postalSymbologies = new List<BaseEncodeType>
        {
            EncodeTypes.Postnet,
            EncodeTypes.Planet
        };

        // Ensure the output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "PostalConfigs");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each symbology and generate its configuration file
        foreach (var symbology in postalSymbologies)
        {
            // Obtain a minimal valid code text for the current symbology
            string sampleCodeText = GetSampleCodeText(symbology);

            // Initialize the barcode generator with the symbology and sample text
            using (var generator = new BarcodeGenerator(symbology, sampleCodeText))
            {
                // Apply the default configuration settings
                generator.Parameters.Barcode.XDimension.Point = defaultXDimension;
                generator.Parameters.Barcode.BarHeight.Point = defaultBarHeight;
                generator.Parameters.Barcode.FilledBars = defaultFilledBars;

                // Build the output file name and path
                string fileName = $"{symbology.TypeName}_Config.xml";
                string filePath = Path.Combine(outputDir, fileName);

                // Export the configured settings to an XML file
                generator.ExportToXml(filePath);

                Console.WriteLine($"Exported configuration for {symbology.TypeName} to {filePath}");
            }
        }

        Console.WriteLine("All postal barcode configurations have been generated.");
    }

    // Provides a minimal valid code text for the given postal symbology
    private static string GetSampleCodeText(BaseEncodeType symbology)
    {
        // Postnet expects a 5, 6, 9, or 11 digit ZIP code; use 5 digits.
        // Planet expects a 6-digit ZIP+4; use 6 digits.
        if (symbology == EncodeTypes.Postnet)
            return "12345";
        if (symbology == EncodeTypes.Planet)
            return "123456";

        // Fallback generic code text
        return "12345";
    }
}
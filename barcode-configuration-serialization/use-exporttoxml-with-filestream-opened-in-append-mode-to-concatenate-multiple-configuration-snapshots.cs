// Title: Export Multiple Barcode Configurations to a Single XML File
// Description: Demonstrates using ExportToXml with a FileStream opened in Append mode to concatenate several barcode configuration snapshots into one XML document.
// Prompt: Use ExportToXml with a FileStream opened in Append mode to concatenate multiple configuration snapshots.
// Tags: barcode, export, xml, configuration, append, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates several barcode generators,
/// modifies a property, and appends each generator's configuration
/// to a single XML file using <c>ExportToXml</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode configurations and concatenates
    /// their XML representations into <c>config.xml</c>.
    /// </summary>
    static void Main()
    {
        // Path of the XML file that will hold all concatenated configuration snapshots
        const string xmlFilePath = "config.xml";

        // Ensure the file exists; create an empty file if it does not
        if (!File.Exists(xmlFilePath))
        {
            using (var createStream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Empty file created – no content needed at this point
            }
        }

        // Sample barcode texts to be used for generating configurations
        string[] codeTexts = { "ABC123", "DEF456", "GHI789" };

        // Iterate over each sample text, generate a barcode, and export its configuration
        foreach (string text in codeTexts)
        {
            // Initialize a barcode generator with Code128 symbology and the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Example of customizing a generator property (set barcode bar color to blue)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

                // Open the XML file in Append mode so each configuration is added sequentially
                using (var stream = new FileStream(xmlFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    // Export the current generator's configuration to the XML stream
                    bool exported = generator.ExportToXml(stream);
                    Console.WriteLine($"Exported configuration for '{text}': {exported}");
                }
            }
        }

        // Inform the user that all configurations have been successfully concatenated
        Console.WriteLine("All configurations have been concatenated to " + xmlFilePath);
    }
}
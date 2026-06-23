using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading a barcode generator state from XML,
/// modifying its parameters, and saving both the barcode image
/// and the updated state back to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Loads a barcode configuration from an XML file, changes the background color,
    /// saves the generated barcode image, and exports the modified configuration.
    /// </summary>
    static void Main()
    {
        // Define file paths for the input XML, output image, and output XML.
        string inputXmlPath = "barcode_state.xml";
        string outputImagePath = "barcode.png";
        string outputXmlPath = "barcode_state_modified.xml";

        // Ensure the input XML file exists before attempting to load it.
        if (!File.Exists(inputXmlPath))
        {
            Console.WriteLine($"Input XML file not found: {inputXmlPath}");
            return;
        }

        // Import the barcode generator state from the XML file.
        using (var generator = BarcodeGenerator.ImportFromXml(inputXmlPath))
        {
            // Example modification: set the barcode background color to light gray.
            generator.Parameters.BackColor = Color.LightGray;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputImagePath, BarCodeImageFormat.Png);

            // Export the modified generator state to a new XML file.
            generator.ExportToXml(outputXmlPath);
        }

        // Inform the user that the process completed successfully.
        Console.WriteLine("Barcode image and modified XML state have been saved.");
    }
}
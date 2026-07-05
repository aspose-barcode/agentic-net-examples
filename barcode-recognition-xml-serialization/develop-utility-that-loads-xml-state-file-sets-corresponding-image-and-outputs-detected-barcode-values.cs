// Title: Barcode detection from XML state file
// Description: Demonstrates loading a barcode reader configuration from an XML file, applying it to an image, and printing detected barcode types and values.
// Prompt: Develop a utility that loads an XML state file, sets the corresponding image, and outputs detected barcode values.
// Tags: barcode, detection, xml, aspose, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example utility that reads barcode detection settings from an XML state file,
/// applies them to a specified image, and writes detected barcode information to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads the XML configuration, validates input files, performs barcode detection,
    /// and outputs each detected barcode's type and value.
    /// </summary>
    static void Main()
    {
        // Default file names – replace with your own paths or pass as arguments.
        string xmlPath = "state.xml";
        string imagePath = "barcode.png";

        // Validate XML file existence.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML state file not found: {xmlPath}");
            return;
        }

        // Validate image file existence.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load reader settings from the XML file.
        // ImportFromXml returns a BarCodeReader instance with the imported configuration.
        using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Assign the image to be processed.
            reader.SetBarCodeImage(imagePath);

            // Perform barcode detection.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detected barcode type and value.
                Console.WriteLine($"Type: {result.CodeTypeName}, Value: {result.CodeText}");
            }
        }
    }
}
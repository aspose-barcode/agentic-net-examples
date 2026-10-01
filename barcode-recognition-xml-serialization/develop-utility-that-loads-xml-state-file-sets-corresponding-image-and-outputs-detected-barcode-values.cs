// Title: Load XML Reader State, Set Image, and Read Barcodes
// Description: Demonstrates how to import barcode reader settings from an XML state file, assign an image, and output detected barcode values.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the BarCodeReader class for importing configuration via XML, setting a source image, and extracting barcode information. Typical use cases include batch processing where reader settings are persisted and reused, or integrating barcode detection into automated workflows. Developers often need to load saved reader states, process images, and retrieve code text, type, and quality metrics.
// Prompt: Develop a utility that loads an XML state file, sets the corresponding image, and outputs detected barcode values.
// Tags: xml, barcode, reading, aspose.barcode, image, detection, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that imports barcode reader settings from an XML file,
/// assigns an image to the reader, and prints detected barcode information to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the utility.
    /// Accepts optional command‑line arguments for the XML state file and image file paths.
    /// </summary>
    /// <param name="args">Command‑line arguments: [0] = XML path, [1] = image path.</param>
    static void Main(string[] args)
    {
        // Default file paths (can be overridden by command‑line arguments)
        string xmlPath = "reader_state.xml";
        string imagePath = "barcode.png";

        if (args.Length >= 2)
        {
            xmlPath = args[0];
            imagePath = args[1];
        }

        // Validate XML state file existence
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML state file not found: {xmlPath}");
            return;
        }

        // Validate image file existence
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Open the XML state file stream for reading
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                // Import reader settings from the XML stream
                using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlStream))
                {
                    // Assign the image that will be processed
                    reader.SetBarCodeImage(imagePath);

                    // Perform barcode detection on the assigned image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output detection results
                    if (results == null || results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected.");
                    }
                    else
                    {
                        Console.WriteLine($"Detected {results.Length} barcode(s):");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Code Text : {result.CodeText}");
                            Console.WriteLine($"Code Type : {result.CodeTypeName}");
                            Console.WriteLine($"Quality   : {result.ReadingQuality}");
                            Console.WriteLine(new string('-', 30));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Graceful error handling: report any exceptions that occur during processing
            Console.WriteLine($"Error during barcode processing: {ex.Message}");
        }
    }
}
// Title: Export barcode state to XML, modify WidthReduction, and regenerate barcode
// Description: Demonstrates exporting a barcode generator's configuration to XML, adjusting the BarWidthReduction property, re‑importing the settings, and creating an updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist barcode generation settings using ExportToXml and ImportFromXml. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and the Parameters.Barcode settings, useful for developers who need to store, modify, or version barcode configurations across environments.
// Prompt: Export barcode state to XML, change WidthReduction to 10 percent, re‑import, and generate updated barcode.
// Tags: barcode, export, import, xml, widthreduction, code128, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode's configuration to XML, modifying the width reduction,
/// re‑importing the configuration, and generating an updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export, modification, import, and save steps.
    /// </summary>
    static void Main()
    {
        // Define temporary XML file path and final image output path
        string xmlPath = "barcode.xml";
        string outputPath = "updated.png";

        // --------------------------------------------------------------------
        // Create a barcode generator, configure basic properties, and export its state to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Export current generator settings to an XML file
            bool exported = generator.ExportToXml(xmlPath);
            if (!exported)
            {
                Console.WriteLine("Failed to export barcode settings to XML.");
                return;
            }
        }

        // Ensure the XML file was created before attempting to import it
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // --------------------------------------------------------------------
        // Import the barcode settings from the XML file, modify WidthReduction, and save the updated barcode
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Set BarWidthReduction to 10 percent (value expressed in points as required by the API)
            importedGenerator.Parameters.Barcode.BarWidthReduction.Point = 10f;

            // Save the updated barcode image to the specified output path
            importedGenerator.Save(outputPath);
        }

        // Inform the user where the updated barcode image was saved
        Console.WriteLine($"Updated barcode saved to: {outputPath}");
    }
}
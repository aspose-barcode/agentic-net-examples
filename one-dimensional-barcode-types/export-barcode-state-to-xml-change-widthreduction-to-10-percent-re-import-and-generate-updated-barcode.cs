// Title: Export Barcode State to XML, Modify Width Reduction, and Regenerate Barcode
// Description: Demonstrates exporting a barcode generator's state to an XML file, adjusting the BarWidthReduction property, and creating an updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and state management category. It shows how to use BarcodeGenerator to save a barcode image, export its configuration to XML, modify parameters such as BarWidthReduction, re-import the configuration, and generate a new barcode. Developers working with barcode customization, persistence, and batch processing commonly use the BarcodeGenerator, EncodeTypes, and related parameter classes to store and reuse barcode settings.
// Prompt: Export barcode state to XML, change WidthReduction to 10 percent, re‑import, and generate updated barcode.
// Tags: barcode, code128, xml, export, import, widthreduction, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that demonstrates exporting a barcode's configuration to XML,
/// modifying the BarWidthReduction setting, and regenerating the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an initial barcode, saves its state,
    /// updates the width reduction, and saves the updated barcode.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original image, XML state, and updated image.
        string originalImagePath = "barcode_original.png";
        string xmlPath = "barcode_state.xml";
        string updatedImagePath = "barcode_updated.png";

        // ------------------------------------------------------------
        // Step 1: Generate a barcode, save the image, and export its state to XML.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the original barcode image to disk.
            generator.Save(originalImagePath);

            // Export the generator's configuration (state) to an XML file.
            bool exported = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Exported to XML: {exported}");
        }

        // ------------------------------------------------------------
        // Step 2: Verify the XML file exists before attempting import.
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file not found. Exiting.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Import the barcode generator from the XML, modify the
        // BarWidthReduction property, and save the updated barcode image.
        // ------------------------------------------------------------
        using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Set BarWidthReduction to 10 points (approximately 10 percent of the bar width).
            importedGenerator.Parameters.Barcode.BarWidthReduction.Point = 10f;

            // Save the updated barcode image to disk.
            importedGenerator.Save(updatedImagePath);
            Console.WriteLine($"Updated barcode saved to: {updatedImagePath}");
        }
    }
}
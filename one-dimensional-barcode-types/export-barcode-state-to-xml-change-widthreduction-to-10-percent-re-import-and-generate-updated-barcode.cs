// Title: Export barcode state to XML, modify width reduction, and regenerate barcode
// Description: Demonstrates exporting a barcode generator's state to XML, adjusting the BarWidthReduction property, and creating an updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and state management category. It showcases how to use BarcodeGenerator, ExportToXml, and ImportFromXml to persist and modify barcode settings. Typical use cases include batch processing, dynamic barcode updates, and configuration versioning, which developers often need when integrating barcode generation into automated workflows.
// Prompt: Export barcode state to XML, change WidthReduction to 10 percent, re‑import, and generate updated barcode.
// Tags: barcode, code128, widthreduction, xml, export, import, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode's state to XML, modifying the bar width reduction,
/// and regenerating the barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates an initial barcode, saves its state,
    /// updates the BarWidthReduction, and saves the updated barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original barcode, the updated barcode, and the XML state file
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string updatedImagePath = Path.Combine(tempFolder, "updated.png");
        string xmlPath = Path.Combine(tempFolder, "barcode_state.xml");

        // Generate the initial barcode and export its configuration to an XML file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Save the barcode image in PNG format
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's current state to XML for later reuse
            generator.ExportToXml(xmlPath);
        }

        // Import the previously saved state, modify the BarWidthReduction, and generate a new barcode
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Set BarWidthReduction to 10 percent (using Point unit as per API)
            importedGenerator.Parameters.Barcode.BarWidthReduction.Point = 10f;

            // Save the updated barcode image in PNG format
            importedGenerator.Save(updatedImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine("Original barcode saved to: " + originalImagePath);
        Console.WriteLine("Updated barcode saved to: " + updatedImagePath);
    }
}
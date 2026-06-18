using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to XML,
/// importing it back, and saving both original and cloned barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the temporary files used in the demo.
        string xmlPath = "barcode_config.xml";      // Path for the exported XML configuration.
        string originalImagePath = "original.png";  // Path for the original barcode image.
        string clonedImagePath = "cloned.png";      // Path for the cloned barcode image.

        // --------------------------------------------------------------------
        // Create and configure the original barcode generator.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set visual properties of the barcode.
            generator.Parameters.Barcode.BarColor = Color.Blue;   // Barcode color.
            generator.Parameters.Barcode.XDimension.Point = 2f; // Module width.
            generator.Parameters.Resolution = 300f;              // Image resolution (dpi).

            // Save the generated barcode image to a file.
            generator.Save(originalImagePath);

            // Export the current generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the configuration from the XML file and create a cloned generator.
        // --------------------------------------------------------------------
        using (var clonedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image generated from the imported configuration.
            clonedGenerator.Save(clonedImagePath);
        }

        // --------------------------------------------------------------------
        // Output the locations of the generated files to the console.
        // --------------------------------------------------------------------
        Console.WriteLine($"Original barcode saved to: {Path.GetFullPath(originalImagePath)}");
        Console.WriteLine($"Cloned barcode saved to: {Path.GetFullPath(clonedImagePath)}");
        Console.WriteLine($"Configuration exported to: {Path.GetFullPath(xmlPath)}");
    }
}
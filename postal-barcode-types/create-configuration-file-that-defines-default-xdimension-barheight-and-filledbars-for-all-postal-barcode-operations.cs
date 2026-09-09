// Title: Create and Apply Default Postal Barcode Settings via XML Configuration
// Description: Demonstrates how to export default barcode parameters to an XML file and reuse them for generating Planet and RM4SCC postal barcodes.
// Category-Description: This example belongs to the Aspose.BarCode configuration and generation category. It shows how to use BarcodeGenerator, its Parameters (XDimension, BarHeight, FilledBars), and XML export/import to define default settings for postal symbologies. Developers often need a single source of truth for barcode appearance across multiple generations, especially in batch processing or CI pipelines.
// Prompt: Create a configuration file that defines default XDimension, BarHeight, and FilledBars for all postal barcode operations.
// Tags: postal barcode, configuration, xdimension, barheight, filledbars, aspose.barcode, xml, generation, png

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a configuration file with default barcode parameters
/// and using it to generate postal barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, writes defaults to XML,
    /// reads them back, and generates Planet and RM4SCC barcodes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "PostalConfigDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Path for the configuration XML that will store default parameters
        string configPath = Path.Combine(tempDir, "PostalDefaults.xml");

        // -----------------------------------------------------------------
        // Step 1: Create a sample generator, set defaults, and export to XML
        // -----------------------------------------------------------------
        using (var gen = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set default visual properties
            gen.Parameters.Barcode.XDimension.Pixels = 3f;
            gen.Parameters.Barcode.BarHeight.Pixels = 60f;
            gen.Parameters.Barcode.FilledBars = false;

            // Export the configured parameters to an XML file
            gen.ExportToXml(configPath);
        }

        Console.WriteLine($"Configuration file created at: {configPath}");

        // -----------------------------------------------------------------
        // Step 2: Load defaults from the configuration file
        // -----------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            Console.WriteLine("Configuration file not found. Exiting.");
            return;
        }

        // Parse the XML to retrieve default values
        XDocument doc = XDocument.Load(configPath);
        float defaultXDim = float.Parse(
            doc.Root.Element("Parameters")?.Element("Barcode")?.Element("XDimension")?.Value ?? "3");
        float defaultBarHeight = float.Parse(
            doc.Root.Element("Parameters")?.Element("Barcode")?.Element("BarHeight")?.Value ?? "60");
        bool defaultFilledBars = bool.Parse(
            doc.Root.Element("Parameters")?.Element("Barcode")?.Element("FilledBars")?.Value ?? "false");

        // -----------------------------------------------------------------
        // Step 3: Generate a Planet barcode using the loaded defaults
        // -----------------------------------------------------------------
        using (var planetGen = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Apply the defaults read from the configuration file
            planetGen.Parameters.Barcode.XDimension.Pixels = defaultXDim;
            planetGen.Parameters.Barcode.BarHeight.Pixels = defaultBarHeight;
            planetGen.Parameters.Barcode.FilledBars = defaultFilledBars;

            // Save the generated barcode as PNG
            string planetPath = Path.Combine(tempDir, "Planet_FromConfig.png");
            planetGen.Save(planetPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Planet barcode saved to: {planetPath}");
        }

        // -----------------------------------------------------------------
        // Step 4: Generate an RM4SCC barcode using the same defaults
        // -----------------------------------------------------------------
        using (var rmGen = new BarcodeGenerator(EncodeTypes.RM4SCC, "123456"))
        {
            // Apply the same defaults to a different postal symbology
            rmGen.Parameters.Barcode.XDimension.Pixels = defaultXDim;
            rmGen.Parameters.Barcode.BarHeight.Pixels = defaultBarHeight;
            rmGen.Parameters.Barcode.FilledBars = defaultFilledBars;

            // Save the generated barcode as PNG
            string rmPath = Path.Combine(tempDir, "RM4SCC_FromConfig.png");
            rmGen.Save(rmPath, BarCodeImageFormat.Png);
            Console.WriteLine($"RM4SCC barcode saved to: {rmPath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}
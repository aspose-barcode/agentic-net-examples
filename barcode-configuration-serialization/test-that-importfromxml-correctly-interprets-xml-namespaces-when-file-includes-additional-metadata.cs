// Title: ImportFromXml with XML Namespace Handling
// Description: Demonstrates importing a barcode configuration from an XML file that includes additional namespace elements, verifying that macro properties are correctly interpreted.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It showcases the use of BarcodeGenerator for creating PDF417 barcodes with macro metadata, exporting the configuration to XML, modifying the XML (adding extra namespaces), and re-importing it. Developers working with barcode serialization, configuration persistence, or custom XML processing will find this pattern useful for ensuring robust import/export operations.
// Prompt: Test that ImportFromXml correctly interprets XML namespaces when the file includes additional metadata.
// Tags: pdf417, macro, import, xml, barcode, generation, aspose.barcode

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a PDF417 barcode with macro metadata,
/// exports its configuration to XML, augments the XML with an extra namespace,
/// and then re-imports the configuration to verify correct handling of namespaces.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, XML manipulation,
    /// and re-import verification steps.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for all generated files.
        string workDir = Path.Combine(Path.GetTempPath(), "ImportFromXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the XML configuration and barcode images.
        string xmlPath = Path.Combine(workDir, "generator.xml");
        string originalImagePath = Path.Combine(workDir, "original.png");
        string importedImagePath = Path.Combine(workDir, "imported.png");

        // --------------------------------------------------------------------
        // Create a PDF417 barcode with macro metadata and save the original image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleMacroData"))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure macro properties specific to PDF417.
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345678;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 12;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 20;
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileName = "file01";
            generator.Parameters.Barcode.Pdf417.MacroPdf417Checksum = 1234;

            // Save the generated barcode image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Load the exported XML and add an extra namespace element to simulate
        // additional metadata that is not part of the barcode configuration.
        // --------------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);
        XNamespace extraNs = "http://example.com/extra";
        XElement extraElement = new XElement(extraNs + "Extra", "AdditionalMetadata");
        doc.Root.Add(extraElement);
        doc.Save(xmlPath);

        // --------------------------------------------------------------------
        // Import the configuration from the modified XML and verify macro values.
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Output imported macro properties to the console for verification.
            Console.WriteLine("Imported MacroPdf417FileID: " + importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417FileID);
            Console.WriteLine("Imported MacroPdf417SegmentID: " + importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID);
            Console.WriteLine("Imported MacroPdf417SegmentsCount: " + importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount);
            Console.WriteLine("Imported MacroPdf417FileName: " + importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417FileName);
            Console.WriteLine("Imported MacroPdf417Checksum: " + importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417Checksum);

            // Generate and save a barcode image using the imported configuration.
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Display the locations of the generated files.
        // --------------------------------------------------------------------
        Console.WriteLine("Original image saved to: " + originalImagePath);
        Console.WriteLine("Imported image saved to: " + importedImagePath);
        Console.WriteLine("XML with extra namespace saved to: " + xmlPath);
    }
}
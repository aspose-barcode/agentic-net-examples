// Title: Generate Barcodes from XML Configurations
// Description: Demonstrates creating barcode configuration XML files, then loading them to generate barcode images.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It showcases the use of BarcodeGenerator for encoding, exporting settings to XML via ExportToXml, and re‑importing those settings with ImportFromXml to produce images. Developers often need to store barcode definitions centrally (e.g., in XML) for batch processing or dynamic generation scenarios.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode symbology, generation, xml configuration, aspose.barcode, image output

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates barcode configuration XML files,
/// then reads each configuration to generate corresponding barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated images and XML configuration files.
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // List of barcode configurations: type, text, XML file path, and image file path.
        var configs = new List<(BaseEncodeType type, string text, string xmlFile, string imageFile)>
        {
            (EncodeTypes.Code128, "ABC123", Path.Combine(outputDir, "config1.xml"), Path.Combine(outputDir, "code128.png")),
            (EncodeTypes.QR, "https://example.com", Path.Combine(outputDir, "config2.xml"), Path.Combine(outputDir, "qr.png")),
            (EncodeTypes.DataMatrix, "DataMatrixSample", Path.Combine(outputDir, "config3.xml"), Path.Combine(outputDir, "datamatrix.png"))
        };

        // --------------------------------------------------------------------
        // Step 1: Create XML configuration files for each barcode definition.
        // --------------------------------------------------------------------
        foreach (var cfg in configs)
        {
            using (var generator = new BarcodeGenerator(cfg.type, cfg.text))
            {
                // Optional: set visual parameters for the barcode.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Export the current generator settings to an XML file.
                generator.ExportToXml(cfg.xmlFile);
            }
        }

        // ---------------------------------------------------------------
        // Step 2: Load each configuration from XML and generate the image.
        // ---------------------------------------------------------------
        foreach (var cfg in configs)
        {
            // Import a BarcodeGenerator instance from the previously saved XML.
            using (var generator = BarcodeGenerator.ImportFromXml(cfg.xmlFile))
            {
                // Save the generated barcode image to the specified file.
                generator.Save(cfg.imageFile);
                Console.WriteLine($"Generated barcode saved to: {cfg.imageFile}");
            }
        }

        Console.WriteLine("All barcodes have been processed.");
    }
}
// Title: Import barcode configuration from XML with error handling for missing properties
// Description: Demonstrates loading a barcode generator configuration from an XML file and handling errors when required properties such as CodeText are absent.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to use BarcodeGenerator.ImportFromXml to load settings from external XML. Typical use cases include dynamic barcode generation based on user‑provided configurations, and developers often need to validate required properties and gracefully handle import exceptions.
// Prompt: Implement error handling for ImportFromXml when the XML file is missing required barcode properties.
// Tags: barcode symbology, import, xml, error handling, aspose.barcode, barcodegenerator, configuration

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates an invalid XML barcode configuration,
/// attempts to import it using <see cref="BarcodeGenerator.ImportFromXml(string)"/>,
/// and demonstrates graceful error handling when required properties are missing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary XML file lacking required
    /// barcode properties, tries to import it, and handles any resulting exceptions.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a temporary directory and XML file that intentionally omits required properties.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string xmlPath = Path.Combine(tempDir, "invalidConfig.xml");

        // Build a minimal XML configuration without the <CodeText> element.
        XDocument doc = new XDocument(
            new XElement("BarcodeGenerator",
                new XElement("Parameters",
                    new XElement("Barcode",
                        new XElement("EncodeType", "Code128")
                        // Note: CodeText element is omitted on purpose.
                    )
                )
            )
        );
        doc.Save(xmlPath);

        // --------------------------------------------------------------------
        // Attempt to import the configuration and handle errors gracefully.
        // --------------------------------------------------------------------
        try
        {
            // ImportFromXml may throw if required properties are missing.
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // If import succeeded, generate a barcode image to verify the configuration.
                string outputPath = Path.Combine(tempDir, "output.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exception that indicates missing required properties.
            Console.WriteLine("Failed to import barcode configuration from XML.");
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // --------------------------------------------------------------------
            // Clean up temporary files (optional).
            // --------------------------------------------------------------------
            try
            {
                if (File.Exists(xmlPath))
                {
                    File.Delete(xmlPath);
                }
                // Note: The generated image is left for inspection; delete if not needed.
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }
    }
}
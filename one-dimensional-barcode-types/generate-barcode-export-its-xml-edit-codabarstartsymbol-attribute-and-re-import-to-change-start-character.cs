using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a Codabar barcode, exporting its configuration to XML,
/// modifying the start symbol, and regenerating the barcode with the updated settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Performs barcode generation, XML export/import,
    /// and saves the original and modified barcode images.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for the XML configuration and barcode images.
        string tempDir = Path.GetTempPath();
        string xmlPath = Path.Combine(tempDir, "codabar.xml");
        string originalImagePath = Path.Combine(tempDir, "codabar_original.png");
        string modifiedImagePath = Path.Combine(tempDir, "codabar_modified.png");

        // Step 1: Create a Codabar barcode generator with the default start symbol (A) and save its image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Save the original barcode image (optional, for verification).
            generator.Save(originalImagePath);
            // Export the generator's configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // Verify that the XML file was successfully created.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to export XML configuration.");
            return;
        }

        // Step 2: Load the XML configuration, modify the CodabarStartSymbol element to a different start symbol (B).
        XDocument doc;
        using (var stream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        {
            doc = XDocument.Load(stream);
        }

        // Locate the CodabarStartSymbol element within the XML and change its value.
        XElement startSymbolElement = doc.Root?.Descendants("CodabarStartSymbol").FirstOrDefault();
        if (startSymbolElement != null)
        {
            startSymbolElement.Value = "B";
        }
        else
        {
            Console.WriteLine("CodabarStartSymbol element not found in XML.");
            return;
        }

        // Save the modified XML back to the same file.
        using (var stream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
        {
            doc.Save(stream);
        }

        // Step 3: Import the modified XML to create a new barcode generator.
        BarcodeGenerator modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (modifiedGenerator == null)
        {
            Console.WriteLine("Failed to import barcode configuration from XML.");
            return;
        }

        // Save the barcode image generated from the modified configuration.
        modifiedGenerator.Save(modifiedImagePath);
        modifiedGenerator.Dispose();

        // Output the locations of the generated files for user reference.
        Console.WriteLine($"Original barcode image: {originalImagePath}");
        Console.WriteLine($"Modified barcode image (start symbol changed): {modifiedImagePath}");
        Console.WriteLine($"XML configuration file: {xmlPath}");
    }
}
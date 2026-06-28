using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a barcode, exporting its configuration to XML,
/// modifying the XML, and re-importing it to generate a modified barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define file paths for the original and modified barcode images.
        // --------------------------------------------------------------------
        string originalImagePath = Path.Combine(Directory.GetCurrentDirectory(), "original.png");
        string modifiedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "modified.png");

        // --------------------------------------------------------------------
        // Step 1: Create a barcode generator with default settings and save the image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Ensure BarWidthReduction is at its default value (0).
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Save the generated barcode image to the original path.
            generator.Save(originalImagePath);
        }

        // --------------------------------------------------------------------
        // Step 2: Export the generator's state to an in‑memory XML document.
        // --------------------------------------------------------------------
        MemoryStream xmlStream = new MemoryStream();
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Keep the same BarWidthReduction setting as before.
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Export the generator configuration to the memory stream as XML.
            generator.ExportToXml(xmlStream);
        }

        // Reset the stream position to the beginning for reading.
        xmlStream.Position = 0;

        // Load the exported XML into an XDocument for manipulation.
        XDocument xmlDoc = XDocument.Load(xmlStream);

        // Locate the <BarWidthReduction> element within the XML.
        XElement reductionElement = xmlDoc.Root?.Descendants("BarWidthReduction").FirstOrDefault();
        if (reductionElement != null)
        {
            // Modify the value to increase the width reduction.
            reductionElement.Value = "0.2";
        }
        else
        {
            // Inform the user if the expected element is missing.
            Console.WriteLine("BarWidthReduction element not found in exported XML.");
        }

        // Save the modified XML back into a new memory stream.
        MemoryStream modifiedXmlStream = new MemoryStream();
        xmlDoc.Save(modifiedXmlStream);
        // Reset position to the beginning for subsequent reading.
        modifiedXmlStream.Position = 0;

        // --------------------------------------------------------------------
        // Step 3: Import the modified XML to create a new generator and save the image.
        // --------------------------------------------------------------------
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlStream))
        {
            // Save the barcode image that now reflects the modified BarWidthReduction.
            modifiedGenerator.Save(modifiedImagePath);
        }

        // --------------------------------------------------------------------
        // Output the results to the console.
        // --------------------------------------------------------------------
        Console.WriteLine($"Original barcode saved to: {originalImagePath}");
        Console.WriteLine($"Modified barcode saved to: {modifiedImagePath}");
        Console.WriteLine("BarWidthReduction was changed via XML export/import.");
    }
}
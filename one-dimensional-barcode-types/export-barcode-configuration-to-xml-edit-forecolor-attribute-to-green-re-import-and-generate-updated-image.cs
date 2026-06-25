using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode configuration to XML, modifying it, and re-importing to generate an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, modifies its color via XML, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for the original and modified XML configurations.
        string originalXmlPath = Path.GetTempFileName();
        string modifiedXmlPath = Path.GetTempFileName();

        // Define the output image file path.
        string outputImagePath = "barcode.png";

        // --------------------------------------------------------------------
        // Create a barcode generator, configure basic properties, and export to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the initial bar color (default is black).
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Export the current configuration to an XML file.
            generator.ExportToXml(originalXmlPath);
        }

        // --------------------------------------------------------------------
        // Load the exported XML, modify the ForeColor attribute to green.
        // --------------------------------------------------------------------
        string xmlContent = File.ReadAllText(originalXmlPath);

        // Replace any existing ForeColor attribute value with "Green".
        string updatedXml = Regex.Replace(xmlContent, "ForeColor=\"[^\"]*\"", "ForeColor=\"Green\"");

        // If the attribute does not exist, add it to the <Barcode> element.
        if (!updatedXml.Contains("ForeColor=\"Green\""))
        {
            // Insert ForeColor attribute after the opening Barcode tag.
            updatedXml = Regex.Replace(updatedXml, "(<Barcode[^>]*?)>", "$1 ForeColor=\"Green\">");
        }

        // Write the modified XML to a new temporary file.
        File.WriteAllText(modifiedXmlPath, updatedXml);

        // --------------------------------------------------------------------
        // Import the modified XML configuration and generate the barcode image.
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlPath))
        {
            // Save the barcode image with the updated color.
            importedGenerator.Save(outputImagePath);
        }

        // --------------------------------------------------------------------
        // Clean up temporary files.
        // --------------------------------------------------------------------
        try { File.Delete(originalXmlPath); } catch { }
        try { File.Delete(modifiedXmlPath); } catch { }

        // Output the location of the generated barcode image.
        Console.WriteLine($"Barcode image generated at: {Path.GetFullPath(outputImagePath)}");
    }
}
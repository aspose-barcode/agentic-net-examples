// Title: ImportFromXml with XML namespaces handling demonstration
// Description: Shows how to export a barcode generator to XML, inject extra namespaced metadata, and import it back, verifying that namespaces are correctly interpreted.
// Prompt: Test that ImportFromXml correctly interprets XML namespaces when the file includes additional metadata.
// Tags: barcode, import, xml, namespaces, code128, aspose.barcodes

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode to XML, adding extra namespaced metadata, and importing it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, modification, import, and validation steps.
    /// </summary>
    static void Main()
    {
        // Paths for temporary files
        string xmlPath = "barcode.xml";
        string modifiedXmlPath = "barcode_modified.xml";

        // Step 1: Create a barcode generator and set a property
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set the barcode color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Export the generator settings to XML
            generator.ExportToXml(xmlPath);
        }

        // Verify the original XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create the original XML file.");
            return;
        }

        // Step 2: Load the XML and insert additional metadata with its own namespace
        string xmlContent = File.ReadAllText(xmlPath);

        // Find the position before the closing root element to insert extra data
        int insertPos = xmlContent.LastIndexOf("</BarcodeGenerator>", StringComparison.Ordinal);
        if (insertPos == -1)
        {
            Console.WriteLine("Unexpected XML format.");
            return;
        }

        // Define extra metadata using a custom namespace
        string extraMetadata = @"
  <ExtraInfo xmlns:ex=""http://example.com/schema"">
    <ex:Note>Sample metadata</ex:Note>
  </ExtraInfo>
";

        // Insert the extra metadata into the original XML content
        string modifiedXml = xmlContent.Insert(insertPos, extraMetadata);
        File.WriteAllText(modifiedXmlPath, modifiedXml);

        // Verify the modified XML file exists
        if (!File.Exists(modifiedXmlPath))
        {
            Console.WriteLine("Failed to create the modified XML file.");
            return;
        }

        // Step 3: Import the barcode generator from the modified XML
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("ImportFromXml returned null.");
            return;
        }

        // Output key properties to confirm successful import
        Console.WriteLine("Imported CodeText: " + importedGenerator.CodeText);
        Console.WriteLine("Imported BarColor: " + importedGenerator.Parameters.Barcode.BarColor.Name);

        // Clean up temporary files (optional)
        try
        {
            File.Delete(xmlPath);
            File.Delete(modifiedXmlPath);
        }
        catch
        {
            // Ignored - cleanup not critical for the test
        }
    }
}
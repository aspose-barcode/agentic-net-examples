using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Paths for temporary XML files
        string originalXmlPath = "barcode.xml";
        string modifiedXmlPath = "barcode_with_metadata.xml";

        // Step 1: Create a barcode generator and set basic properties
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Export the generator settings to an XML file
            bool exportSuccess = generator.ExportToXml(originalXmlPath);
            Console.WriteLine($"Export to original XML succeeded: {exportSuccess}");
        }

        // Step 2: Load the exported XML, add extra namespace and custom metadata, then save
        XDocument doc = XDocument.Load(originalXmlPath);
        // Define a new namespace for custom metadata
        XNamespace customNs = "http://example.com/custom";
        // Add a custom element under the root element
        doc.Root.Add(new XElement(customNs + "CustomInfo", "Additional metadata"));
        // Save the modified XML
        doc.Save(modifiedXmlPath);
        Console.WriteLine("Modified XML with additional metadata saved.");

        // Step 3: Import the barcode generator from the modified XML
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("ImportFromXml returned null. Test failed.");
            return;
        }

        // Verify that core properties are correctly interpreted
        Console.WriteLine($"Imported Barcode Type: {importedGenerator.BarcodeType}");
        Console.WriteLine($"Imported CodeText: {importedGenerator.CodeText}");

        // Clean up temporary files
        try
        {
            File.Delete(originalXmlPath);
            File.Delete(modifiedXmlPath);
        }
        catch
        {
            // Ignored - cleanup not critical for the test
        }
    }
}
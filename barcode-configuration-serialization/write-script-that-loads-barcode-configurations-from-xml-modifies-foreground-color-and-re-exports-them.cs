using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare temporary file paths
        string tempDir = Path.GetTempPath();
        string originalXmlPath = Path.Combine(tempDir, "barcodeConfig.xml");
        string modifiedXmlPath = Path.Combine(tempDir, "barcodeConfigModified.xml");
        string outputImagePath = Path.Combine(tempDir, "barcode.png");

        // Step 1: Create a barcode generator and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "12345";
            // Export initial configuration to XML
            generator.ExportToXml(originalXmlPath);
            // Also save an image for reference (optional)
            generator.Save(outputImagePath);
        }

        // Step 2: Load the configuration from the XML file
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(originalXmlPath))
        {
            // Step 3: Modify the foreground (bar) color
            loadedGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Step 4: Re‑export the modified configuration to a new XML file
            loadedGenerator.ExportToXml(modifiedXmlPath);

            // Optional: Save the barcode image with the new color
            string modifiedImagePath = Path.Combine(tempDir, "barcode_modified.png");
            loadedGenerator.Save(modifiedImagePath);
        }

        // Output the locations of the generated files
        Console.WriteLine("Original XML: " + originalXmlPath);
        Console.WriteLine("Modified XML: " + modifiedXmlPath);
        Console.WriteLine("Original Image: " + outputImagePath);
        Console.WriteLine("Modified Image: " + Path.Combine(tempDir, "barcode_modified.png"));
    }
}
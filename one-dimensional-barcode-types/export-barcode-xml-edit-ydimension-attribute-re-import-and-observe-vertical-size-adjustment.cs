// Title: Export Barcode to XML, Modify YDimension, and Re‑Import to Adjust Size
// Description: Demonstrates exporting a barcode's generation state to XML, editing the YDimension attribute, re‑importing the XML, and observing the resulting change in the barcode's vertical size.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator to create a barcode, export its configuration with ExportToXml, modify XML attributes, and reload the configuration with ImportFromXml. Developers working with barcode customization, persistence, or batch processing often need to serialize settings, adjust parameters like YDimension, and regenerate barcodes without recreating them from scratch.
// Prompt: Export barcode XML, edit YDimension attribute, re‑import, and observe vertical size adjustment.
// Tags: code128, xml, export, import, ydimension, image, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a barcode to XML, modifying its YDimension attribute,
/// re‑importing the XML, and comparing the original and modified barcode image sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, exports its state,
    /// edits the XML, re‑imports the configuration, and outputs image dimensions.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for XML and PNG images
        string xmlPath = Path.Combine(tempFolder, "barcode.xml");
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string modifiedImagePath = Path.Combine(tempFolder, "modified.png");

        // Step 1: Generate a barcode and export its state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the original barcode image
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Output original image dimensions for comparison
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Original image size: {bmp.Width}x{bmp.Height}");
            }

            // Export the generator's configuration (including all settings) to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Edit the exported XML – add or modify the YDimension attribute
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);

            // Locate the <Barcode> element; if missing, fall back to the root element
            var barcodeElement = doc.Descendants("Barcode").FirstOrDefault();
            if (barcodeElement != null)
            {
                // Set YDimension to 50 (units are typically points)
                barcodeElement.SetAttributeValue("YDimension", "50");
            }
            else if (doc.Root != null)
            {
                // If <Barcode> is not present, apply the attribute to the root element
                doc.Root.SetAttributeValue("YDimension", "50");
            }

            // Save the modified XML back to disk
            doc.Save(xmlPath);
        }
        else
        {
            Console.WriteLine("XML file was not created.");
            return;
        }

        // Step 3: Import the modified XML and generate the barcode again
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        using (importedGenerator)
        {
            // Save the modified barcode image
            importedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);

            // Output modified image dimensions to show the effect of YDimension change
            using (Bitmap bmp = importedGenerator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Modified image size: {bmp.Width}x{bmp.Height}");
            }
        }

        // Optional: display file paths for manual inspection
        Console.WriteLine($"Original image: {originalImagePath}");
        Console.WriteLine($"Modified image: {modifiedImagePath}");
        Console.WriteLine($"XML file: {xmlPath}");
    }
}
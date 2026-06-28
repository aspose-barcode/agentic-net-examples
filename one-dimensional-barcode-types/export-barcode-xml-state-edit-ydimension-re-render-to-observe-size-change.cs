using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode, exporting its state to XML,
/// modifying the XML to change the barcode image height, and
/// re‑generating the barcode image from the modified XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Executes the barcode generation, XML manipulation, and size verification steps.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Define file paths for the original image, XML state, and modified image
        // -----------------------------------------------------------------
        string originalImagePath = "original.png";
        string xmlPath = "barcode_state.xml";
        string modifiedImagePath = "modified.png";

        // -----------------------------------------------------------------
        // Step 1: Create a barcode, set explicit size, export to XML and save image
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Use interpolation mode so ImageWidth/ImageHeight control the size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;   // width in points
            generator.Parameters.ImageHeight.Point = 150f; // initial height (YDimension) in points

            // Save the original barcode image to file
            generator.Save(originalImagePath);

            // Export the current generator state (including size settings) to XML
            generator.ExportToXml(xmlPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Load the exported XML, modify the YDimension (ImageHeight), and save back
        // -----------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file not found. Exiting.");
            return;
        }

        // Load the XML document containing the barcode generator state
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Find the ImageHeight element (case‑sensitive) and change its value
        var imageHeightElement = xmlDoc.Descendants("ImageHeight").FirstOrDefault();
        if (imageHeightElement != null)
        {
            // Set new height (e.g., 300 points) to observe size change
            imageHeightElement.Value = "300";
        }
        else
        {
            Console.WriteLine("ImageHeight element not found in XML. Exiting.");
            return;
        }

        // Save the modified XML back to the same file
        xmlDoc.Save(xmlPath);

        // -----------------------------------------------------------------
        // Step 3: Load the modified XML into a new generator and render the barcode again
        // -----------------------------------------------------------------
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Ensure the same AutoSizeMode is applied (it should be persisted, but set explicitly for safety)
            generatorFromXml.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the modified barcode image to file
            generatorFromXml.Save(modifiedImagePath);
        }

        // -----------------------------------------------------------------
        // Step 4: Load both images and output their dimensions to verify the change
        // -----------------------------------------------------------------
        if (!File.Exists(originalImagePath) || !File.Exists(modifiedImagePath))
        {
            Console.WriteLine("One of the image files was not created. Exiting.");
            return;
        }

        // Open the original and modified images and display their pixel dimensions
        using (var originalImg = Image.FromFile(originalImagePath))
        using (var modifiedImg = Image.FromFile(modifiedImagePath))
        {
            Console.WriteLine($"Original image size:  Width = {originalImg.Width} px, Height = {originalImg.Height} px");
            Console.WriteLine($"Modified image size: Width = {modifiedImg.Width} px, Height = {modifiedImg.Height} px");
        }
    }
}
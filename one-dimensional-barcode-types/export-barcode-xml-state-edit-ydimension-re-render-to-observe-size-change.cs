// Title: Export barcode XML state, modify XDimension, and compare image sizes
// Description: Demonstrates how to generate a barcode, export its configuration to XML, edit the XDimension property, re‑import the settings, and render a new image to see the size change.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It shows how to use BarcodeGenerator, its Parameters.Barcode settings, ExportToXml and ImportFromXml methods to persist and modify barcode state. Typical use cases include saving barcode configurations, batch editing, and reproducing barcodes with altered dimensions. Developers often need to adjust XDimension or YDimension to control module size for printing or scanning requirements.
// Prompt: Export barcode XML state, edit YDimension, re‑render to observe size change.
// Tags: barcode, code128, xdimension, ydimension, export, import, xml, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode, exports its state to XML,
/// modifies the XDimension, and re‑renders the barcode to illustrate the size change.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves its XML state,
    /// updates the XDimension, and outputs the dimensions of the original and modified images.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare temporary folder and file paths for the demo assets
        // ------------------------------------------------------------
        string basePath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(basePath);
        string originalImagePath = Path.Combine(basePath, "original.png");
        string modifiedImagePath = Path.Combine(basePath, "modified.png");
        string xmlPath = Path.Combine(basePath, "state.xml");

        // ------------------------------------------------------------
        // Step 1: Create a barcode generator, set initial XDimension,
        //         save the barcode image, and export its configuration to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // narrow module width
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Step 2: Import the generator settings from the XML file,
        //         modify XDimension to a larger value, and save the new image
        // ------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Increase XDimension to observe size change
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(modifiedImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Step 3: Load the generated images and report their dimensions
        // ------------------------------------------------------------
        if (File.Exists(originalImagePath))
        {
            using (var bmp = new Bitmap(originalImagePath))
            {
                Console.WriteLine($"Original image size: {bmp.Width}x{bmp.Height}");
            }
        }
        else
        {
            Console.WriteLine("Original image not found.");
        }

        if (File.Exists(modifiedImagePath))
        {
            using (var bmp = new Bitmap(modifiedImagePath))
            {
                Console.WriteLine($"Modified image size: {bmp.Width}x{bmp.Height}");
            }
        }
        else
        {
            Console.WriteLine("Modified image not found.");
        }

        // ------------------------------------------------------------
        // Optional cleanup: delete the temporary folder and its contents
        // ------------------------------------------------------------
        // Directory.Delete(basePath, true);
    }
}
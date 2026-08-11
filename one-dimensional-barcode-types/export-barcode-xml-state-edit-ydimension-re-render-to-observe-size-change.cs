// Title: Export barcode XML state, modify YDimension, and re-render
// Description: Demonstrates how to export a BarcodeGenerator's configuration to XML, change the bar height (Y dimension), and generate new images to observe size differences.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category, showcasing the use of BarcodeGenerator, ExportToXml, ImportFromXml, and bar dimension properties. Typical use cases include persisting barcode settings, batch editing, and dynamic resizing for different output requirements. Developers often need to serialize settings, adjust parameters like YDimension, and regenerate barcodes without recreating the generator from scratch.
// Prompt: Export barcode XML state, edit YDimension, re‑render to observe size change.
// Tags: barcode, code128, export, xml, ydimension, barheight, aspose.barcode, generation, image, png, serialization

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode generator's state to XML, modifying the Y dimension, and re‑rendering the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves its original image, exports its configuration to XML,
    /// imports it, changes the bar height, and saves the modified image.
    /// </summary>
    static void Main()
    {
        // Create initial barcode generator with a sample code text.
        using (BarcodeGenerator generator1 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set initial bar height (Y dimension) to 30 points.
            generator1.Parameters.Barcode.BarHeight.Point = 30f;

            // Export the generator's state to XML in a memory stream.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                generator1.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset for reading.

                // Render the original barcode and output its size.
                using (Aspose.Drawing.Bitmap originalImage = generator1.GenerateBarCodeImage())
                {
                    Console.WriteLine($"Original image size: {originalImage.Width}x{originalImage.Height} pixels");
                    generator1.Save("barcode_original.png", BarCodeImageFormat.Png);
                }

                // Import the XML into a new generator instance.
                BarcodeGenerator generator2 = BarcodeGenerator.ImportFromXml(xmlStream);

                // Modify the Y dimension (bar height) to a larger value.
                generator2.Parameters.Barcode.BarHeight.Point = 80f;

                // Render the modified barcode and output its new size.
                using (Aspose.Drawing.Bitmap modifiedImage = generator2.GenerateBarCodeImage())
                {
                    Console.WriteLine($"Modified image size: {modifiedImage.Width}x{modifiedImage.Height} pixels");
                    generator2.Save("barcode_modified.png", BarCodeImageFormat.Png);
                }

                // Dispose the imported generator explicitly.
                generator2.Dispose();
            }
        }
    }
}
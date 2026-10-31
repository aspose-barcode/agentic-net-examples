// Title: Export barcode to XML and PNG with proper resource disposal
// Description: Demonstrates generating a Code128 barcode, exporting its definition to an XML file, and saving the barcode image as PNG while ensuring FileStream objects are correctly disposed.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set barcode parameters, and export to XML and image formats. Developers often need to create barcodes programmatically, customize dimensions, and persist definitions without locking files, which this sample illustrates. Ideal for CI pipelines and automated reporting scenarios.
// Prompt: Ensure proper disposal of FileStream objects after calling ExportToXml to prevent file locks.
// Tags: barcode, code128, export, xml, png, filestream, disposal, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation, XML export, and image saving with proper disposal of resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, exports its XML definition, and saves a PNG image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML definition and the barcode image
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the data to encode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Adjust the X-dimension (module width) of the barcode in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Export the barcode definition to an XML file.
            // The FileStream is wrapped in a using block to guarantee disposal and release the file lock.
            using (FileStream fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                generator.ExportToXml(fs);
            }

            // Save the rendered barcode image as a PNG file.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the files have been written
        Console.WriteLine("Exported XML to: " + xmlPath);
        Console.WriteLine("Saved barcode image to: " + imagePath);
    }
}
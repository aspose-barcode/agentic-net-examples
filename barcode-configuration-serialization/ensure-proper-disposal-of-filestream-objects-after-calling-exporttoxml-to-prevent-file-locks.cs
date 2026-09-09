// Title: Export barcode generator state to XML and reload it
// Description: Demonstrates exporting a BarcodeGenerator's configuration to an XML file, then importing it to recreate the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and persistence category, showcasing how to use BarcodeGenerator, ExportToXml, and ImportFromXml for saving and restoring barcode settings. Typical use cases include persisting barcode configurations, sharing them across services, or version‑controlling barcode definitions. Developers often need to serialize generator state to XML, then reload it to reproduce identical barcodes.
// Prompt: Ensure proper disposal of FileStream objects after calling ExportToXml to prevent file locks.
// Tags: barcode, export, xml, import, persistence, aspose.barcode, generation, qrcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's state to XML, saving the barcode image,
/// importing the state back, and saving the regenerated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, import, and image saving operations.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all output files
        string tempDir = Path.Combine(Path.GetTempPath(), "ExportXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML state and barcode images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string imagePath = Path.Combine(tempDir, "barcode.png");
        string loadedImagePath = Path.Combine(tempDir, "barcode_loaded.png");

        // Initialize a barcode generator with QR code symbology and sample text
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        generator.Parameters.Barcode.XDimension.Pixels = 4f;

        // Export the generator's state to XML using a FileStream and ensure proper disposal
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            generator.ExportToXml(xmlStream);
        }

        // Save the generated barcode image to a PNG file using a FileStream
        using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            generator.Save(imgStream, BarCodeImageFormat.Png);
        }

        // Import the generator state from the previously saved XML file
        BarcodeGenerator loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);

        // Save the barcode generated from the imported state to another PNG file
        using (FileStream img2Stream = new FileStream(loadedImagePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            loadedGenerator.Save(img2Stream, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine("Exported XML: " + xmlPath);
        Console.WriteLine("Original barcode image: " + imagePath);
        Console.WriteLine("Loaded barcode image: " + loadedImagePath);
    }
}
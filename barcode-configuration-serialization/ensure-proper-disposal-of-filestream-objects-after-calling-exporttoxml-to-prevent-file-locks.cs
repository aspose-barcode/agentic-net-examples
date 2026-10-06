// Title: Export Barcode Generator Configuration to XML with Proper FileStream Disposal
// Description: Demonstrates exporting a barcode generator's settings to an XML file using both a direct path and a FileStream, ensuring the stream is disposed to avoid file locks.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, ExportToXml, ImportFromXml, and Save methods to configure, persist, and render barcodes. Developers commonly need to serialize barcode settings for reuse, versioning, or sharing across applications, and proper resource management (e.g., disposing FileStream) is essential to prevent file access issues.
// Prompt: Ensure proper disposal of FileStream objects after calling ExportToXml to prevent file locks.
// Tags: barcode symbology, export, xml, file stream disposal, aspose.barcode, generation, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to XML,
/// importing it back, and saving the resulting barcode image while
/// correctly disposing file streams to avoid file locks.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export, import, and image generation steps.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeExportDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the XML configuration and the barcode image
        string xmlPath = Path.Combine(outputDir, "generator.xml");
        string imagePath = Path.Combine(outputDir, "barcode.png");

        // Create a barcode generator with Code128 symbology and sample data
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");
        // Adjust the X-dimension (module width) for better visual quality
        generator.Parameters.Barcode.XDimension.Pixels = 2f;

        // Export the generator's configuration directly to an XML file (no stream needed)
        generator.ExportToXml(xmlPath);

        // Export the same configuration using a FileStream wrapped in a using statement
        // The using block guarantees the stream is disposed, preventing file locks
        using (FileStream fileStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            generator.ExportToXml(fileStream);
        }

        // Import the generator state from the previously saved XML file
        BarcodeGenerator loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);

        // Save the barcode image to verify that the imported configuration works correctly
        loadedGenerator.Save(imagePath, BarCodeImageFormat.Png);

        // Inform the user where the output files are located
        Console.WriteLine($"Exported XML and generated image are located in: {outputDir}");
    }
}
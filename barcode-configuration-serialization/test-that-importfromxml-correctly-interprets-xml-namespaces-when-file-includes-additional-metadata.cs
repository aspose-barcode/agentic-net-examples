// Title: ImportFromXml with MacroPdf417 metadata verification
// Description: Demonstrates exporting a MacroPdf417 barcode to XML and re-importing it, confirming that XML namespaces and macro metadata are correctly interpreted.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator to create a barcode, export its generation state to XML, and later import it using ImportFromXml. Developers working with barcode persistence, metadata handling, or XML-based workflows commonly need to serialize and deserialize barcode settings, especially when dealing with macro PDF417 metadata and namespace handling.
// Prompt: Test that ImportFromXml correctly interprets XML namespaces when the file includes additional metadata.
// Tags: macro pdf417, barcode generation, xml import, xml export, aspose.barcode, metadata, serialization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MacroPdf417 barcode, exports its state to XML,
/// re-imports it, and verifies that macro metadata is preserved correctly.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML export/import,
    /// and outputs the imported macro metadata for verification.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for all generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for XML, original image, and imported image
        string xmlPath = Path.Combine(workFolder, "generator.xml");
        string originalImagePath = Path.Combine(workFolder, "original.png");
        string importedImagePath = Path.Combine(workFolder, "imported.png");

        // --------------------------------------------------------------------
        // Create a MacroPdf417 barcode and set its macro metadata
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MacroPdf417, "SampleText"))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure macro metadata (prefix MacroPdf417*)
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345678;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 12;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 20;
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileName = "file01";
            generator.Parameters.Barcode.Pdf417.MacroPdf417Checksum = 1234;
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileSize = 400000;
            generator.Parameters.Barcode.Pdf417.MacroPdf417TimeStamp = new DateTime(2019, 11, 1);
            generator.Parameters.Barcode.Pdf417.MacroPdf417Addressee = "street";
            generator.Parameters.Barcode.Pdf417.MacroPdf417Sender = "aspose";

            // Save the generated barcode image
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's state (including metadata) to an XML file
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import generation state from XML and verify that macro metadata is restored
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image generated from the imported state
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);

            // Output macro metadata to the console for verification
            Console.WriteLine("Imported Macro Metadata:");
            Console.WriteLine($"MacroPdf417FileID: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417FileID}");
            Console.WriteLine($"MacroPdf417SegmentID: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID}");
            Console.WriteLine($"MacroPdf417SegmentsCount: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount}");
            Console.WriteLine($"MacroPdf417FileName: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417FileName}");
            Console.WriteLine($"MacroPdf417Checksum: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417Checksum}");
            Console.WriteLine($"MacroPdf417FileSize: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417FileSize}");
            Console.WriteLine($"MacroPdf417TimeStamp: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417TimeStamp}");
            Console.WriteLine($"MacroPdf417Addressee: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417Addressee}");
            Console.WriteLine($"MacroPdf417Sender: {importedGenerator.Parameters.Barcode.Pdf417.MacroPdf417Sender}");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(originalImagePath);
            File.Delete(importedImagePath);
            File.Delete(xmlPath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
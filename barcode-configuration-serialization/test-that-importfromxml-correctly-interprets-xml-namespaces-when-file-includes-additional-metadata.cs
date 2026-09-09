// Title: ImportFromXml with PDF417 macro metadata and XML namespace handling
// Description: Demonstrates exporting a BarcodeGenerator configuration to XML, then importing it back while preserving PDF417 macro metadata.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to use BarcodeGenerator, ExportToXml, and ImportFromXml for persisting and restoring barcode settings. Developers often need to serialize barcode configurations, share them across services, or validate import integrity, especially when XML includes additional namespaces.
// Prompt: Test that ImportFromXml correctly interprets XML namespaces when the file includes additional metadata.
// Tags: pdf417, macro, importfromxml, xml, barcode, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a PDF417 macro barcode, exports its configuration to XML,
/// imports the configuration back, and verifies that all settings are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML export/import,
    /// and validation of imported settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original image, XML configuration, and imported image
        string imagePath = Path.Combine(tempFolder, "original.png");
        string xmlPath = Path.Combine(tempFolder, "generator.xml");
        string importedImagePath = Path.Combine(tempFolder, "imported.png");

        // --------------------------------------------------------------------
        // Generate a PDF417 macro barcode and configure its macro metadata
        // --------------------------------------------------------------------
        using (var gen = new BarcodeGenerator(EncodeTypes.MacroPdf417, "Åspóse.Barcóde©"))
        {
            // Basic barcode appearance settings
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Parameters.Barcode.Pdf417.Columns = 4;

            // PDF417 macro-specific metadata
            gen.Parameters.Barcode.Pdf417.MacroPdf417FileID = 12345678;
            gen.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 12;
            gen.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 20;
            gen.Parameters.Barcode.Pdf417.MacroPdf417FileName = "file01";
            gen.Parameters.Barcode.Pdf417.MacroPdf417Checksum = 1234;
            gen.Parameters.Barcode.Pdf417.MacroPdf417FileSize = 400000;
            gen.Parameters.Barcode.Pdf417.MacroPdf417TimeStamp = new DateTime(2019, 11, 1);
            gen.Parameters.Barcode.Pdf417.MacroPdf417Addressee = "street";
            gen.Parameters.Barcode.Pdf417.MacroPdf417Sender = "aspose";

            // Save the generated barcode image
            gen.Save(imagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file
            gen.ExportToXml(xmlPath);
        }

        // Verify that the XML file was created successfully
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Import the barcode configuration from the XML file and validate settings
        // --------------------------------------------------------------------
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save an image generated from the imported configuration
            importedGen.Save(importedImagePath, BarCodeImageFormat.Png);

            // Compare each relevant setting to ensure they match the original values
            bool xDimEqual = Math.Abs(importedGen.Parameters.Barcode.XDimension.Pixels - 2f) < 0.001f;
            bool columnsEqual = importedGen.Parameters.Barcode.Pdf417.Columns == 4;
            bool fileIdEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417FileID == 12345678;
            bool segmentIdEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417SegmentID == 12;
            bool segmentsCountEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount == 20;
            bool fileNameEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417FileName == "file01";
            bool checksumEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417Checksum == 1234;
            bool fileSizeEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417FileSize == 400000;
            bool timeStampEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417TimeStamp == new DateTime(2019, 11, 1);
            bool addresseeEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417Addressee == "street";
            bool senderEqual = importedGen.Parameters.Barcode.Pdf417.MacroPdf417Sender == "aspose";

            // Output verification results
            Console.WriteLine("ImportFromXml verification results:");
            Console.WriteLine($"XDimension.Pixels: {(xDimEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"Pdf417.Columns: {(columnsEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417FileID: {(fileIdEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417SegmentID: {(segmentIdEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417SegmentsCount: {(segmentsCountEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417FileName: {(fileNameEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417Checksum: {(checksumEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417FileSize: {(fileSizeEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417TimeStamp: {(timeStampEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417Addressee: {(addresseeEqual ? "OK" : "Mismatch")}");
            Console.WriteLine($"MacroPdf417Sender: {(senderEqual ? "OK" : "Mismatch")}");
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}
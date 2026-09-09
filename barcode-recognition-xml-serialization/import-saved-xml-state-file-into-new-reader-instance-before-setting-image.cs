// Title: Import BarCodeReader state from XML and decode barcode
// Description: Demonstrates exporting a BarCodeReader configuration to an XML file, importing it into a new reader instance, and decoding a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to persist and restore BarCodeReader settings using XML. It covers key API classes such as BarcodeGenerator, BarCodeReader, and related settings objects. Typical use cases include saving recognition configurations for later reuse, sharing settings across applications, and reducing initialization overhead. Developers working with barcode generation and recognition often need to export/import reader state to maintain consistent decoding behavior.
/// Prompt: Import a saved XML state file into a new reader instance before setting the image.
/// Tags: barcode symbology, import, export, xml, state, generation, recognition, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a barcode, exports the reader's configuration to XML,
/// imports the configuration into a new reader, and decodes the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and decoding workflow.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a temporary working directory for generated files
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // ------------------------------------------------------------
        // 2. Define file paths for the barcode image and the XML state file
        // ------------------------------------------------------------
        string imagePath = Path.Combine(workDir, "sample.png");
        string xmlPath = Path.Combine(workDir, "readerState.xml");

        // ------------------------------------------------------------
        // 3. Generate a simple Code128 barcode image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 4. Create a BarCodeReader, configure it, and export its state to XML
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Example setting – can be adjusted as needed
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Export the reader's configuration (the image itself is not stored)
            reader.ExportToXml(xmlPath);
        }

        // Verify that the XML state file was created successfully
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to export reader state to XML.");
            return;
        }

        // ------------------------------------------------------------
        // 5. Import the saved XML state into a new BarCodeReader instance
        // ------------------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Set the image source (required after import)
            importedReader.SetBarCodeImage(imagePath);

            // Set decode type again because it is not stored in XML
            importedReader.SetBarCodeReadType(DecodeType.Code128);

            // Perform barcode recognition
            BarCodeResult[] results = importedReader.ReadBarCodes();

            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 6. Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            File.Delete(xmlPath);
            Directory.Delete(workDir);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}
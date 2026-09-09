// Title: Barcode State Export/Import Demo
// Description: Demonstrates generating a barcode, exporting the BarCodeReader state to XML, importing that state later, setting the image, and reading detected barcode values.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to persist and restore a BarCodeReader's configuration using XML. It utilizes key API classes such as BarcodeGenerator, BarCodeReader, and related settings (BarcodeSettings, QualitySettings). Typical use cases include saving recognition configurations for later reuse, batch processing, or sharing settings across applications. Developers often need to export/import reader state to maintain consistent detection parameters without reconfiguring each run.
/// Prompt: Develop a utility that loads an XML state file, sets the corresponding image, and outputs detected barcode values.
/// Tags: barcode symbology, export, import, xml, state, recognition, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates loading a barcode recognition state from XML, assigning an image, and reading barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, exports reader state, imports it, and reads barcodes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeStateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the barcode image and the exported XML state
        string imagePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");

        // -------------------------------------------------
        // Generate a sample QR code image
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the X-dimension (module size) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Create a BarCodeReader, configure its settings, and export its state to XML
        // -------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Example configuration: strip FNC characters and use high-performance quality settings
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Export the current recognition state to an XML file
            reader.ExportToXml(xmlPath);

            // Optional initial read to demonstrate detection before import
            BarCodeResult[] initialResults = reader.ReadBarCodes();
            foreach (BarCodeResult result in initialResults)
            {
                Console.WriteLine($"[Initial] Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Import the saved state, assign the barcode image, and read barcodes again
        // -------------------------------------------------
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Associate the previously generated image with the imported reader
            importedReader.SetBarCodeImage(imagePath);

            // Perform barcode detection using the imported configuration
            BarCodeResult[] importedResults = importedReader.ReadBarCodes();
            foreach (BarCodeResult result in importedResults)
            {
                Console.WriteLine($"[Imported] Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Clean up temporary files (optional)
        // -------------------------------------------------
        try
        {
            File.Delete(imagePath);
            File.Delete(xmlPath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any cleanup errors to avoid interrupting the demo flow
        }
    }
}
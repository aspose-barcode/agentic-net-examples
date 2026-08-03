// Title: Import XML Settings into BarCodeReader and Read Barcode
// Description: Demonstrates exporting a BarCodeReader configuration to XML, importing it into a new reader instance, and reading a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode, BarCodeReader to configure recognition settings, and the ExportToXml/ImportFromXml methods to persist and restore reader state. Developers often need to save recognition configurations for reuse across applications or environments, especially when dealing with batch processing or CI pipelines.
// Prompt: Import a saved XML state file into a new reader instance before setting the image.
// Tags: code128, import, export, read, png, xml, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, saves reader settings to XML,
/// imports those settings into a new reader, and reads the barcode from an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, settings export,
    /// settings import, and barcode reading workflow.
    /// </summary>
    static void Main()
    {
        // Paths for the barcode image and the XML settings file
        string imagePath = "barcode.png";
        string xmlPath = "readerSettings.xml";

        // -------------------------------------------------
        // Step 1: Generate a simple barcode image and save it
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set visual parameters (optional)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to a PNG file
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Step 2: Create a BarCodeReader, configure it, and export its settings to XML
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath))
        {
            // Example of a quality setting (optional)
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Export the current reader configuration to an XML file
            reader.ExportToXml(xmlPath);
        }

        // -------------------------------------------------
        // Step 3: Import the saved XML state into a new reader instance,
        //         then set the image before reading barcodes.
        // -------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML settings file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Barcode image file not found: {imagePath}");
            return;
        }

        // Import the reader configuration from XML; this returns a new BarCodeReader instance
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Assign the image to the imported reader (required before reading)
            importedReader.SetBarCodeImage(imagePath);

            // Read barcodes from the image and output results
            foreach (var result in importedReader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
            }
        }
    }
}
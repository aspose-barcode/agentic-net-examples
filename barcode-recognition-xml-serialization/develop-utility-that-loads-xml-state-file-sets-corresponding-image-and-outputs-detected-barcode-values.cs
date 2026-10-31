// Title: Load BarCodeReader state from XML, set image, and read barcodes
// Description: Demonstrates how to export a BarCodeReader configuration to XML, import it later, assign the corresponding image, and output detected barcode values.
// Category-Description: This example belongs to the Aspose.BarCode state management category, illustrating the use of BarcodeGenerator for image creation, BarCodeReader for barcode detection, and the ExportToXml / ImportFromXml methods for persisting and restoring reader settings. Developers working with batch processing, configuration sharing, or repeatable scanning scenarios commonly need to save reader configurations and reuse them across sessions.
// Prompt: Develop a utility that loads an XML state file, sets the corresponding image, and outputs detected barcode values.
// Tags: barcode symbology, state export, xml, barcode reading, aspose.barcode, qrcode, generation, detection

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR barcode, saves the reader state to XML,
/// reloads the state, assigns the image, and prints detected barcode information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeStateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated image and the exported XML state
        string imagePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // ------------------------------------------------------------
        // Step 1: Generate a sample QR barcode image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Save the barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Create a BarCodeReader, configure it, and export its state to XML
        // ------------------------------------------------------------
        using (var writerReader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Example setting: enable stripping of FNC characters from the result
            writerReader.BarcodeSettings.StripFNC = true;

            // Export the current reader configuration to an XML file
            writerReader.ExportToXml(xmlPath);
        }

        // Verify that the XML state file was created successfully
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to export reader state to XML.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Import the reader state from XML, assign the image, and read barcodes
        // ------------------------------------------------------------
        using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // The XML does not contain the image data, so we set the image source explicitly
            reader.SetBarCodeImage(imagePath);

            // Read all barcodes from the image using the imported settings
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Cleanup: delete temporary files and folder (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            File.Delete(xmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}
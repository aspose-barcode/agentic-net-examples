// Title: Import XML configuration into BarCodeReader and read barcode
// Description: Demonstrates exporting a barcode reader's configuration to XML, then importing it into a new BarCodeReader instance before assigning the image for recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code, exporting its reader settings via ExportToXml, and restoring those settings with BarCodeReader.ImportFromXml. Developers often need to persist reader configurations for reuse across sessions or environments, making this pattern essential for scalable barcode processing solutions.
// Prompt: Import a saved XML state file into a new reader instance before setting the image.
// Tags: barcode, xml, import, export, reader, generator, qrcode, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to export a barcode reader's configuration to XML,
/// import it into a new BarCodeReader instance, set the image, and read the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, saves its configuration,
    /// imports the configuration into a new reader, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the barcode image and the exported XML configuration
        string imagePath = Path.Combine(tempFolder, "barcode.png");
        string xmlPath = Path.Combine(tempFolder, "readerConfig.xml");

        // -----------------------------------------------------------------
        // Step 1: Generate a QR code, save the image, and export the reader XML state
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Save the generated barcode image as PNG
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Export the reader configuration to XML (image data is not included)
            generator.ExportToXml(xmlPath);
        }

        // Verify that the barcode image and XML configuration were created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create XML configuration.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Import the saved XML into a new BarCodeReader, assign the image, and read
        // -----------------------------------------------------------------
        using (var reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Assign the image source (ImportFromXml restores only settings, not the image)
            reader.SetBarCodeImage(imagePath);

            // Perform barcode recognition and output results
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }

        // Cleanup: optionally remove temporary files (comment out if inspection is needed)
        try
        {
            File.Delete(imagePath);
            File.Delete(xmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for the demo
        }
    }
}
// Title: Import BarCodeReader state from XML and decode a QR barcode
// Description: Demonstrates exporting a BarCodeReader configuration to an XML file, importing it into a new reader instance, and decoding a QR barcode image.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to persist and restore BarCodeReader settings using XML. It uses key API classes such as BarcodeGenerator for barcode creation, BarCodeReader for decoding, and the ExportToXml/ImportFromXml methods for state serialization. Typical use cases include saving reader configurations for later reuse, sharing settings across applications, or simplifying deployment scenarios where consistent decoding parameters are required.
// Prompt: Import a saved XML state file into a new reader instance before setting the image.
// Tags: barcode, qr, import, export, xml, state, reader, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a BarCodeReader's configuration to XML,
/// importing it into a new reader, and decoding a QR barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, saves the reader state,
    /// imports the state into a new reader, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for demo files
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the barcode image and the exported XML state
        string barcodePath = Path.Combine(workDir, "barcode.png");
        string xmlPath = Path.Combine(workDir, "readerState.xml");

        // ------------------------------------------------------------
        // Generate a sample QR barcode image
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Create a BarCodeReader, configure a setting, and export its state to XML
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Example setting: ignore FNC characters during decoding
            reader.BarcodeSettings.StripFNC = true;
            // Export the current reader configuration to an XML file
            reader.ExportToXml(xmlPath);
        }

        // Verify that the XML state file was created successfully
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to export reader state to XML.");
            return;
        }

        // ------------------------------------------------------------
        // Import the saved XML state into a new reader instance
        // ------------------------------------------------------------
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // The image source and decode type are not stored in the XML,
            // so they must be set explicitly after import.
            importedReader.SetBarCodeImage(barcodePath);
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform barcode detection and decoding
            BarCodeResult[] results = importedReader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            File.Delete(xmlPath);
            Directory.Delete(workDir);
        }
        catch
        {
            // Ignored - cleanup not critical for demo
        }
    }
}
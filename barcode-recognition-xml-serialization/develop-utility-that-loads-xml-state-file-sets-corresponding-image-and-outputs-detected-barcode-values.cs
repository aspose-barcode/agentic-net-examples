// Title: Load BarCodeReader state from XML and read barcodes
// Description: Demonstrates loading a previously exported BarCodeReader configuration from an XML file, assigning the barcode image, and extracting barcode values.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to export a BarCodeReader's settings to XML, import them later, and reuse the configuration for barcode recognition. It uses key API classes such as BarcodeGenerator, BarCodeReader, and related settings objects. Typical use cases include persisting reader configurations across sessions, sharing settings between applications, or debugging complex recognition setups. Developers often need to serialize/deserialize reader state to streamline deployment and maintain consistent decoding behavior.
// Prompt: Develop a utility that loads an XML state file, sets the corresponding image, and outputs detected barcode values.
// Tags: barcode, symbology, generation, recognition, xml, state, import, export, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, exports the reader state to XML,
/// imports the state back, assigns the image, and reads the barcode values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the full workflow from image creation
    /// to state import and barcode extraction.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary working folder for generated files
        // ------------------------------------------------------------
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeStateDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        string imagePath = Path.Combine(workFolder, "sample.png");
        string xmlPath = Path.Combine(workFolder, "readerState.xml");

        // ------------------------------------------------------------
        // Step 1: Generate a sample barcode image (Code128)
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Adjust X-dimension for better visual quality
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Step 2: Create a BarCodeReader, configure settings, and export its state to XML
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Example configuration – can be customized as needed
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Export the configured state (the image reference itself is NOT saved)
            reader.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Step 3: Verify that the exported files exist before proceeding
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"State file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Barcode image not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 4: Import the reader state from XML, assign the image, and read barcodes
        // ------------------------------------------------------------
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // After import, the image and read type must be set explicitly
            importedReader.SetBarCodeImage(imagePath);
            importedReader.SetBarCodeReadType(DecodeType.Code128);

            // Perform barcode detection
            BarCodeResult[] results = importedReader.ReadBarCodes();

            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Step 5: Clean up temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            File.Delete(xmlPath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}
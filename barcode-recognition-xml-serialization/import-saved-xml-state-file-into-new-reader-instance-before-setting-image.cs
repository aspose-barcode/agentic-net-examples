// Title: Import barcode reader state from XML and recognize a QR code
// Description: Demonstrates exporting a BarCodeReader's configuration to an XML file, importing it into a new reader, and decoding a QR barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist and restore BarCodeReader settings using ExportToXml and ImportFromXml. It covers key API classes such as BarcodeGenerator, BarCodeReader, and related settings objects, useful for developers who need to reuse reader configurations across sessions or applications.
// Prompt: Import a saved XML state file into a new reader instance before setting the image.
// Tags: qr, barcode, import, xml, state, configuration, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that shows how to export a BarCodeReader's settings to XML,
/// import those settings into a new reader instance, and then decode a QR code image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, saves reader settings,
    /// imports them, and reads the barcode from the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for generated files
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the barcode image and the exported XML state
        string barcodePath = Path.Combine(workDir, "sample.png");
        string xmlStatePath = Path.Combine(workDir, "readerState.xml");

        // 1. Generate a simple QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // 2. Create a BarCodeReader, configure its settings, and export the configuration to XML
        using (var writerReader = new BarCodeReader())
        {
            writerReader.SetBarCodeReadType(DecodeType.QR);
            writerReader.BarcodeSettings.StripFNC = true;
            writerReader.QualitySettings.XDimension = XDimensionMode.Small;
            writerReader.ExportToXml(xmlStatePath);
        }

        // 3. Import the saved XML state into a new reader instance
        using (var importedReader = BarCodeReader.ImportFromXml(xmlStatePath))
        {
            // Set the image to be recognized (the XML state does not contain the image)
            importedReader.SetBarCodeImage(barcodePath);

            // Ensure the decode type is set (this information is not stored in the XML)
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform barcode recognition
            BarCodeResult[] results = importedReader.ReadBarCodes();

            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup generated files and temporary directory (optional)
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(xmlStatePath)) File.Delete(xmlStatePath);
            if (Directory.Exists(workDir)) Directory.Delete(workDir, true);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program outcome
        }
    }
}
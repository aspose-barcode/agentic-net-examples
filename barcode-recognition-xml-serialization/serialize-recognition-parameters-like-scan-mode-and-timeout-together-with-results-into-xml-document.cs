// Title: Serialize barcode recognition parameters and results to XML
// Description: Demonstrates how to generate a QR barcode, read it with custom recognition settings, and export both the settings and detection results into an XML file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings classes to customize scan mode, timeout, and other parameters, then serialize the reader state. Developers working with barcode scanning automation often need to persist recognition configurations and outcomes for auditing or downstream processing; this snippet provides a concise reference for such tasks.
// Prompt: Serialize recognition parameters like scan mode and timeout together with results into an XML document.
// Tags: barcode, qr, recognition, xml, serialization, settings, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, customized recognition, and exporting the reader state to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads it with specific settings, and writes the reader state to an XML file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the barcode image and the exported XML document
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // Generate a simple QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the reader with the generated image and specify QR as the decode type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Configure recognition parameters
            reader.BarcodeSettings.StripFNC = true;          // Example setting: ignore FNC characters
            reader.QualitySettings.XDimension = XDimensionMode.Small; // Adjust X-dimension for quality
            reader.Timeout = 2000; // Set timeout to 2000 ms

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output recognition results to the console
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Serialize both the configured parameters and the recognition results to XML
            reader.ExportToXml(xmlPath);
        }

        // Confirm that the XML export succeeded
        if (File.Exists(xmlPath))
        {
            Console.WriteLine($"Reader state exported to: {xmlPath}");
        }
        else
        {
            Console.WriteLine("Failed to export reader state to XML.");
        }
    }
}
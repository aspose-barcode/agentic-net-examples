// Title: Demonstrate handling ImportFromXml errors when barcode image is missing
// Description: Shows how ImportFromXml throws an exception if SetBarCodeImage is not called, then resolves the issue by providing the image.
// Category-Description: This example belongs to the Aspose.BarCode reading operations category. It illustrates exporting a BarCodeReader configuration to XML, importing it back, and the necessity of setting the barcode image before reading. Key API classes include BarCodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and methods ExportToXml, ImportFromXml, SetBarCodeImage. Developers working with barcode scanning automation often need to persist and restore reader settings, and must handle missing image errors gracefully.
// Prompt: Write code to handle ImportFromXml errors when the required barcode image has not been provided via SetBarCodeImage.
// Tags: barcode symbology, import, export, xml, error handling, aspose.barcode, code128, reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that demonstrates error handling when importing a BarCodeReader from XML
/// without first providing the barcode image, and then correctly reading after setting the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, exports reader settings to XML,
    /// attempts to read without an image (capturing the error), then sets the image and reads successfully.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the barcode image and the exported XML
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string xmlPath = Path.Combine(tempFolder, "reader.xml");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a BarCodeReader, configure optional settings, and export its state to XML
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Example optional setting: strip FNC characters from the result
            reader.BarcodeSettings.StripFNC = true;
            reader.ExportToXml(xmlPath);
        }

        // Import the reader state from XML without setting the barcode image first
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            Console.WriteLine("Attempting to read without setting barcode image...");

            try
            {
                // This call is expected to fail because no image has been provided
                var results = importedReader.ReadBarCodes();
                Console.WriteLine($"Unexpectedly read {results.Length} barcodes.");
            }
            catch (Exception ex)
            {
                // Capture and display the error message for diagnostic purposes
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Provide the barcode image and specify the decode type before reading again
            importedReader.SetBarCodeImage(barcodePath);
            importedReader.SetBarCodeReadType(DecodeType.Code128);

            Console.WriteLine("Reading after setting barcode image:");
            var barcodes = importedReader.ReadBarCodes();
            foreach (var result in barcodes)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory; ignore any cleanup errors
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup failures are non‑critical; program can exit normally
        }
    }
}
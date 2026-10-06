// Title: Demonstrate handling ImportFromXml errors when barcode image is missing
// Description: Shows how ImportFromXml throws an exception if SetBarCodeImage is not called, and how to recover by providing the required image.
// Category-Description: This example belongs to the Aspose.BarCode reading and state management category. It demonstrates exporting a BarCodeReader state to XML, importing it back, and handling the common scenario where the barcode image is not included in the exported state. Key API classes include BarcodeGenerator, BarCodeReader, and methods ExportToXml, ImportFromXml, and SetBarCodeImage. Developers often need to persist reader settings, transfer them between processes, or debug recognition pipelines, and must manage missing image errors gracefully.
/// Prompt: Write code to handle ImportFromXml errors when the required barcode image has not been provided via SetBarCodeImage.
/// Tags: barcode symbology, import, export, xml, error handling, aspose.barcode, pdf417, reader, generator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that demonstrates error handling when importing a BarCodeReader state from XML
/// without first providing the barcode image, then correcting the issue by setting the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, exports reader state to XML,
    /// attempts to read without an image (expected failure), then sets the image and reads successfully.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated barcode image and the exported XML state
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // Generate a sample PDF417 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a BarCodeReader for the generated image and export its state to XML (image not included)
        using (var reader = new BarCodeReader(barcodePath))
        {
            // Optional: modify a reader setting to illustrate state persistence
            reader.BarcodeSettings.StripFNC = true;

            // Export the current recognition state (without embedding the image) to an XML file
            reader.ExportToXml(xmlPath);
        }

        // Import the reader state from XML without providing the barcode image
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            Console.WriteLine("Attempting to read without setting barcode image...");

            try
            {
                // This call is expected to fail because the image has not been set
                var results = importedReader.ReadBarCodes();
                Console.WriteLine("Unexpected success: read {0} barcodes.", results.Length);
            }
            catch (Exception ex)
            {
                // Expected error handling for missing image
                Console.WriteLine("Expected error: " + ex.Message);
            }

            // Provide the required barcode image to the imported reader
            importedReader.SetBarCodeImage(barcodePath);

            Console.WriteLine("Reading after setting barcode image...");

            try
            {
                // Now the read operation should succeed
                var results = importedReader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"Found barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors after setting the image
                Console.WriteLine("Error after setting image: " + ex.Message);
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program outcome
        }
    }
}
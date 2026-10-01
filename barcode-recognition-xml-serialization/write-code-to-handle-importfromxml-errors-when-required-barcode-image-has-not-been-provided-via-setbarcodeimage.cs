// Title: Demonstrate handling ImportFromXml errors when barcode image is missing
// Description: Shows how ImportFromXml throws an exception if SetBarCodeImage is not called, and how to correctly provide the image.
// Category-Description: This example belongs to the Aspose.BarCode XML configuration category, illustrating the use of BarcodeGenerator, BarCodeReader, and related classes to export settings to XML and import them for recognition. Developers often need to persist barcode generation settings, reuse them, and handle missing image scenarios gracefully. The snippet serves as a searchable reference for error handling with ImportFromXml.
// Prompt: Write code to handle ImportFromXml errors when the required barcode image has not been provided via SetBarCodeImage.
// Tags: code128, importfromxml, png, barcodegenerator, barcodereader, barcoderecognitionexception

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, exporting its configuration to XML,
/// importing the configuration for recognition, and handling the error that occurs
/// when the barcode image is not supplied before reading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Executes the generation, export, import, and
    /// error‑handling workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder for all demo files.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the generated barcode image and the exported XML configuration.
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "config.xml");

        // --------------------------------------------------------------------
        // 2. Generate a simple Code128 barcode and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
            // Export the generator settings to an XML file for later import.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // 3. Attempt to import the reader from XML without providing the image.
        //    This should raise a BarCodeRecognitionException.
        // --------------------------------------------------------------------
        Console.WriteLine("=== Attempt without SetBarCodeImage ===");
        try
        {
            using (var reader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Intentionally omit SetBarCodeImage to provoke an error.
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Unexpectedly read {results.Length} barcode(s).");
            }
        }
        catch (BarCodeRecognitionException ex)
        {
            // Expected exception when no image is set.
            Console.WriteLine($"Caught expected BarCodeRecognitionException: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other unexpected exception.
            Console.WriteLine($"Caught unexpected exception: {ex.GetType().Name} - {ex.Message}");
        }

        // --------------------------------------------------------------------
        // 4. Correct usage: import from XML, set the barcode image, then read.
        // --------------------------------------------------------------------
        Console.WriteLine("\n=== Correct usage with SetBarCodeImage ===");
        try
        {
            using (var reader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Provide the image source required for recognition.
                reader.SetBarCodeImage(barcodePath);

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected barcode: CodeText = {result.CodeText}, Symbology = {result.CodeTypeName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during correct read: {ex.GetType().Name} - {ex.Message}");
        }

        // --------------------------------------------------------------------
        // 5. Cleanup temporary files and folder.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for the demo.
        }
    }
}
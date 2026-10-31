// Title: Demonstrate ImportFromXml exception handling without prior SetBarCodeImage
// Description: Shows how importing a BarCodeReader state from XML and attempting to read without setting the barcode image throws an exception, then demonstrates correct usage.
// Category-Description: This example belongs to the Aspose.BarCode reading and state management category. It illustrates the use of BarCodeReader, ExportToXml, ImportFromXml, SetBarCodeImage, and DecodeType to manage barcode recognition state. Developers often need to persist reader configurations, reload them, and handle missing image scenarios, making this pattern useful for unit testing and error handling.
// Prompt: Create a unit test that ensures ImportFromXml throws an exception when called without prior SetBarCodeImage invocation.
// Tags: barcode symbology, importfromxml, exception handling, unit test, aspose.barcode, barcodereader, xml export, decode type

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates the behavior of BarCodeReader.ImportFromXml when the barcode image is not set prior to reading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, exports reader state to XML, imports it, and validates exception handling.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated barcode image and the exported XML state
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string xmlPath = Path.Combine(tempFolder, "reader.xml");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a reader for the generated image, set the decode type, and export its configuration to XML
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.ExportToXml(xmlPath);
        }

        // Import the reader configuration from XML without assigning the barcode image
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            try
            {
                // Attempt to read barcodes; this should fail because SetBarCodeImage was not called
                BarCodeResult[] results = importedReader.ReadBarCodes();
                Console.WriteLine("Unexpected success: read {0} barcode(s).", results.Length);
            }
            catch (Exception ex)
            {
                // Expected path: an exception is thrown due to missing image
                Console.WriteLine("Expected exception caught: {0} - {1}", ex.GetType().Name, ex.Message);
            }

            // Correct usage: set the image and read barcodes successfully
            importedReader.SetBarCodeImage(barcodePath);
            importedReader.SetBarCodeReadType(DecodeType.Code128);
            BarCodeResult[] successfulResults = importedReader.ReadBarCodes();
            Console.WriteLine("After setting image, read {0} barcode(s).", successfulResults.Length);
        }

        // Clean up temporary files and directory
        try { File.Delete(barcodePath); } catch { }
        try { File.Delete(xmlPath); } catch { }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}
// Title: ImportFromXml Exception Test
// Description: Demonstrates a unit‑style test that verifies ImportFromXml throws when no barcode image is set.
// Category-Description: This example belongs to the Aspose.BarCode reading and state‑management category. It shows how to generate a barcode, export a BarCodeReader’s state to XML, import that state, and handle the requirement to call SetBarCodeImage before reading. Developers working with barcode recognition often need to persist and restore reader configurations, and must ensure the image source is provided to avoid runtime errors.
// Prompt: Create a unit test that ensures ImportFromXml throws an exception when called without prior SetBarCodeImage invocation.
// Tags: barcode, importfromxml, exception, unit-test, code128, xml, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a test that ImportFromXml throws an exception if SetBarCodeImage is not called first.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the test scenario.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the generated barcode image and the exported reader state XML
        string barcodePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");

        // Generate a simple Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a BarCodeReader, set the image and read type, then export its state to XML
        using (BarCodeReader writer = new BarCodeReader())
        {
            writer.SetBarCodeImage(barcodePath);
            writer.SetBarCodeReadType(DecodeType.Code128);
            writer.ExportToXml(xmlPath);
        }

        // Attempt to import the reader state without calling SetBarCodeImage and read barcodes
        bool exceptionThrown = false;
        try
        {
            using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Intentionally omit SetBarCodeImage to trigger the expected exception
                BarCodeResult[] results = importedReader.ReadBarCodes();
                // If no exception occurs, the test has failed
                Console.WriteLine("FAILURE: No exception was thrown. Read {0} barcodes.", results?.Length ?? 0);
            }
        }
        catch (Exception ex)
        {
            // Expected path: an exception should be thrown
            exceptionThrown = true;
            Console.WriteLine("EXPECTED EXCEPTION: " + ex.Message);
        }

        // Report test outcome based on whether the exception was caught
        if (exceptionThrown)
        {
            Console.WriteLine("TEST PASSED: ImportFromXml without SetBarCodeImage throws an exception as expected.");
        }
        else
        {
            Console.WriteLine("TEST FAILED: No exception was thrown when calling ImportFromXml without SetBarCodeImage.");
        }

        // Clean up temporary files and directory, ignoring any errors during cleanup
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}
// Title: ImportFromXml exception test for Aspose.BarCode
// Description: Demonstrates a unit‑style test that verifies ImportFromXml throws an exception when called without a prior SetBarCodeImage invocation.
// Category-Description: This example belongs to the Aspose.BarCode reading and state management category. It shows how to export a BarCodeReader's state to XML, import it back, and handle error conditions when required initialization (setting the barcode image) is missing. Key API classes include BarCodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and BarCodeResult. Developers often need such patterns when persisting reader configurations across sessions or services.
// Prompt: Create a unit test that ensures ImportFromXml throws an exception when called without prior SetBarCodeImage invocation.
// Tags: barcode, import, xml, exception, aspose.barcode, code128, reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a self‑contained test that validates the behavior of <c>BarCodeReader.ImportFromXml</c>
/// when the barcode image has not been set before reading.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, exports reader state to XML,
    /// imports the state without setting an image, and confirms that an exception is thrown.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the generated barcode image and the exported XML state
        string imagePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode image and save it as PNG
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Create a BarCodeReader, configure it, and export its state to XML
        // ------------------------------------------------------------
        using (BarCodeReader writer = new BarCodeReader())
        {
            writer.SetBarCodeImage(imagePath);               // Load the generated image
            writer.SetBarCodeReadType(DecodeType.Code128);   // Restrict reading to Code128 symbology
            writer.ExportToXml(xmlPath);                     // Persist the reader configuration
        }

        // ------------------------------------------------------------
        // Import the reader state from XML without setting the image
        // ------------------------------------------------------------
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Set the read type (image is intentionally not set to trigger the error)
            importedReader.SetBarCodeReadType(DecodeType.Code128);

            bool exceptionThrown = false;
            try
            {
                // Attempt to read barcodes – this should fail because no image was provided
                BarCodeResult[] results = importedReader.ReadBarCodes();

                // If execution reaches here, the expected exception was not thrown
                Console.WriteLine("Test failed: No exception was thrown.");
            }
            catch (BarCodeRecognitionException ex)
            {
                // Expected exception type for missing image
                exceptionThrown = true;
                Console.WriteLine("Test passed: Expected exception caught.");
                Console.WriteLine("Exception message: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Any other exception also satisfies the test condition
                exceptionThrown = true;
                Console.WriteLine("Test passed: Expected exception caught (type: " + ex.GetType().Name + ").");
                Console.WriteLine("Exception message: " + ex.Message);
            }

            if (!exceptionThrown)
            {
                Console.WriteLine("Test failed: Expected exception was not thrown.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath)) File.Delete(imagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect test outcome
        }
    }
}
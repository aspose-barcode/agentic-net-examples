// Title: Demonstrate ExportToXml error handling with an uninitialized BarCodeReader
// Description: Shows how to catch exceptions when ExportToXml is invoked without first loading an image into BarCodeReader, then illustrates the correct usage with an initialized image.
// Category-Description: This example belongs to the Aspose.BarCode reading and configuration category. It demonstrates the BarCodeReader.ImportFromXml and ExportToXml APIs, common for persisting and restoring reader settings. Developers often need to export reader configurations to XML for diagnostics or reuse, and must handle cases where the reader lacks an associated image.
// Prompt: Implement error handling to catch exceptions when ExportToXml is called without initializing the reader with an image.
// Tags: barcode, error handling, export, xml, aspose.barcode, barcodereader, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a console demonstration of handling ExportToXml errors when the BarCodeReader
/// has not been initialized with an image, and shows the correct workflow with a valid image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes two scenarios:
    /// 1) Attempts to export a reader configuration without an image, catching the expected exception.
    /// 2) Generates a sample barcode, loads it into a reader, and successfully exports the configuration to XML.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Scenario 1: Export without initializing the reader with an image.
        // ------------------------------------------------------------

        // Prepare a minimal XML configuration for BarCodeReader.
        string minimalXml = "<BarCodeReader></BarCodeReader>";
        byte[] xmlBytes = Encoding.UTF8.GetBytes(minimalXml);
        string exportPathWithoutImage = Path.Combine(Path.GetTempPath(), "export_without_image.xml");

        using (var xmlStream = new MemoryStream(xmlBytes))
        {
            // Import the configuration. The returned reader has no image initialized.
            using (var reader = BarCodeReader.ImportFromXml(xmlStream))
            {
                try
                {
                    // Attempt to export the reader configuration to XML.
                    // This will throw because no image was set.
                    reader.ExportToXml(exportPathWithoutImage);
                    Console.WriteLine("Exported XML (unexpected success).");
                }
                catch (Exception ex)
                {
                    // Expected error handling for missing image.
                    Console.WriteLine("Error during ExportToXml (expected): " + ex.Message);
                }
            }
        }

        // Clean up the failed export file if it was created.
        if (File.Exists(exportPathWithoutImage))
        {
            File.Delete(exportPathWithoutImage);
        }

        // ------------------------------------------------------------
        // Scenario 2: Correct usage with an initialized image.
        // ------------------------------------------------------------

        // Generate a simple barcode image to use as a sample.
        string sampleImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
        GenerateSampleBarcode(sampleImagePath);

        string exportPathWithImage = Path.Combine(Path.GetTempPath(), "export_with_image.xml");

        // Load the image into a new reader, then export to XML successfully.
        using (var readerWithImage = new BarCodeReader(sampleImagePath, DecodeType.AllSupportedTypes))
        {
            try
            {
                readerWithImage.ExportToXml(exportPathWithImage);
                string exportedXml = File.ReadAllText(exportPathWithImage);
                Console.WriteLine("Successfully exported XML after initializing image:");
                Console.WriteLine(exportedXml);
            }
            catch (Exception ex)
            {
                // Unexpected error handling for the successful path.
                Console.WriteLine("Unexpected error during ExportToXml: " + ex.Message);
            }
        }

        // Clean up temporary files.
        if (File.Exists(sampleImagePath))
        {
            File.Delete(sampleImagePath);
        }
        if (File.Exists(exportPathWithImage))
        {
            File.Delete(exportPathWithImage);
        }
    }

    /// <summary>
    /// Helper method to generate a simple barcode image using Aspose.BarCode.
    /// </summary>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateSampleBarcode(string outputPath)
    {
        // Use Code128 as an example symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the barcode image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}
// Title: Demonstrate ExportToXml error handling with uninitialized BarCodeReader
// Description: Shows how to catch exceptions when ExportToXml is called before setting an image, then performs successful export after initialization.
// Prompt: Implement error handling to catch exceptions when ExportToXml is called without initializing the reader with an image.
// Tags: barcode symbology, export, xml, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates error handling for ExportToXml when the BarCodeReader is not initialized with an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the path for the sample barcode image
        string imagePath = "sample.png";

        // Generate a simple Code128 barcode and save it to a file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the sample barcode image.");
            return;
        }

        // Create a BarCodeReader without initializing it with an image
        using (BarCodeReader reader = new BarCodeReader())
        {
            // Attempt to export settings to XML without setting an image
            try
            {
                reader.ExportToXml("reader_without_image.xml");
                Console.WriteLine("Export succeeded unexpectedly (no image was set).");
            }
            catch (Exception ex)
            {
                // Expected exception handling
                Console.WriteLine("Caught expected exception when exporting without image:");
                Console.WriteLine(ex.Message);
            }

            // Initialize the reader with the generated barcode image
            reader.SetBarCodeImage(imagePath);

            // Export to XML after proper initialization
            try
            {
                reader.ExportToXml("reader_with_image.xml");
                Console.WriteLine("Export succeeded after initializing the reader with an image.");
            }
            catch (Exception ex)
            {
                // Unexpected exception handling
                Console.WriteLine("Unexpected exception after initializing the reader:");
                Console.WriteLine(ex.Message);
            }
        }

        // Clean up generated files (optional)
        try
        {
            if (File.Exists("reader_without_image.xml"))
                File.Delete("reader_without_image.xml");
            if (File.Exists("reader_with_image.xml"))
                File.Delete("reader_with_image.xml");
            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}
// Title: Demonstrate handling ImportFromXml errors when barcode image is missing
// Description: Shows how to catch errors from ImportFromXml and recover by loading the required barcode image via SetBarCodeImage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating the use of BarcodeGenerator, BarCodeReader, and related classes. Developers often need to export generator settings to XML, import them later, and handle cases where the barcode image is not yet available, requiring explicit image loading before decoding. The snippet provides a typical error‑handling pattern for such scenarios.
// Prompt: Write code to handle ImportFromXml errors when the required barcode image has not been provided via SetBarCodeImage.
// Tags: barcode generation, barcode recognition, importfromxml, setbarcodeimage, error handling, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that demonstrates how to handle ImportFromXml errors
/// when the required barcode image has not been provided via SetBarCodeImage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, exports settings to XML,
    /// attempts to import those settings without an image, handles the resulting error,
    /// and finally reads the barcode after loading the correct image.
    /// </summary>
    static void Main()
    {
        // Paths for temporary files
        string imagePath = "sample_barcode.png";
        string xmlPath = "generator_settings.xml";

        // -------------------------------------------------
        // Step 1: Generate a barcode image and export its settings to XML
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode image to a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Export generator settings to XML for later import
            generator.ExportToXml(xmlPath);
        }

        // -------------------------------------------------
        // Step 2: Import generator settings from XML (simulating a scenario where
        // the barcode image is not yet provided to the reader)
        // -------------------------------------------------
        BarcodeGenerator importedGenerator;
        try
        {
            importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to import generator from XML: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // Step 3: Attempt to read the barcode without setting the image.
        // This will raise an exception because the reader has no valid image.
        // -------------------------------------------------
        // Create a dummy 1x1 bitmap just to satisfy the constructor.
        using (var dummyBitmap = new Bitmap(1, 1))
        using (var reader = new BarCodeReader(dummyBitmap, DecodeType.Code128))
        {
            try
            {
                // This call will fail because the dummy image does not contain a barcode.
                var results = reader.ReadBarCodes();

                // If no exception, but no results, treat it as missing image.
                if (results.Length == 0)
                {
                    throw new BarCodeException("No barcode detected – likely because a proper image was not set.");
                }
            }
            catch (BarCodeException ex)
            {
                Console.WriteLine($"Reader error (expected): {ex.Message}");
                Console.WriteLine("Loading the required barcode image via SetBarCodeImage...");

                // -------------------------------------------------
                // Step 4: Load the actual barcode image and set it.
                // -------------------------------------------------
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"Barcode image file not found: {imagePath}");
                    return;
                }

                using (var barcodeImage = (Bitmap)Image.FromFile(imagePath))
                {
                    // Provide the correct image to the reader
                    reader.SetBarCodeImage(barcodeImage);

                    // Now attempt to read again
                    var finalResults = reader.ReadBarCodes();
                    foreach (var result in finalResults)
                    {
                        Console.WriteLine($"Detected Barcode Type: {result.CodeType}");
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    }
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            if (File.Exists(imagePath)) File.Delete(imagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program flow
        }
    }
}
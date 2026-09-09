// Title: Demonstrate handling ImportFromXml errors when barcode image is missing
// Description: Shows how to import a BarCodeReader state from XML without an image, catch the resulting error, then set the image and read successfully.
// Category-Description: This example belongs to the Aspose.BarCode reading and state management category. It illustrates using BarCodeReader.ExportToXml, BarCodeReader.ImportFromXml, and SetBarCodeImage to persist and restore reader configuration. Developers often need to serialize reader state for later processing or distributed scenarios, and must handle missing image errors gracefully.
// Prompt: Write code to handle ImportFromXml errors when the required barcode image has not been provided via SetBarCodeImage.
// Tags: barcode, importfromxml, error-handling, code128, xml, setbarcodeimage, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that demonstrates exporting a BarCodeReader state to XML,
/// importing it back without an image, handling the resulting error, and then
/// correctly setting the image to perform a successful read.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, export,
    /// import, error handling, and final read workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the barcode image and the exported XML state
        string imagePath = Path.Combine(tempFolder, "barcode.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode, export the reader's state to XML, then dispose the reader
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var initialResults = reader.ReadBarCodes();
            Console.WriteLine($"Initial read count: {initialResults.Length}");
            reader.ExportToXml(xmlPath);
        }

        // Import the reader state from XML without providing the barcode image
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Attempt to read; this should throw because no image has been set
            try
            {
                var resultsWithoutImage = importedReader.ReadBarCodes();
                Console.WriteLine($"Read without image count: {resultsWithoutImage.Length}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected error without image: {ex.Message}");
            }

            // Provide the barcode image and specify the decode type, then read successfully
            importedReader.SetBarCodeImage(imagePath);
            importedReader.SetBarCodeReadType(DecodeType.Code128);
            var finalResults = importedReader.ReadBarCodes();
            Console.WriteLine($"Read after setting image count: {finalResults.Length}");

            // Output each decoded result
            foreach (var result in finalResults)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder; ignore any errors during cleanup
        try
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}
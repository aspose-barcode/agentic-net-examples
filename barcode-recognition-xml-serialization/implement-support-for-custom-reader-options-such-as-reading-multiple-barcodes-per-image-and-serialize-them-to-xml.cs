// Title: Demonstrate barcode generation, custom reader options, and XML serialization of settings
// Description: This example generates a QR code, reads it using custom reader options, exports the reader configuration to XML, and reimports it to read the barcode again.
// Category-Description: Shows how to use Aspose.BarCode for both generation and recognition. Key API classes include BarcodeGenerator for creating barcodes, BarCodeReader for detecting multiple barcodes per image, and methods for exporting/importing reader settings to XML. Typical use cases cover custom detection settings, batch processing, and persisting configuration for reuse across applications.
// Prompt: Implement support for custom reader options, such as reading multiple barcodes per image, and serialize them to XML.
// Tags: barcode generation, barcode reading, custom reader options, xml serialization, qrcode, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a demo that generates a QR barcode, reads it with custom options,
/// and demonstrates exporting and importing reader settings via XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image.
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode with the text "Hello World" and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a reader that can detect all supported barcode types in the image.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Set a custom reader option: enable checksum validation.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            Console.WriteLine("Reading barcodes with original reader:");
            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}, Type: {result.CodeTypeName}");
            }

            // Export the current reader settings to an XML file for later reuse.
            string xmlPath = Path.Combine(tempFolder, "readerSettings.xml");
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Reader settings exported to XML at: {xmlPath}");

            // Import the previously saved settings into a new reader instance.
            using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // After importing, associate the image source with the new reader.
                importedReader.SetBarCodeImage(barcodePath);

                Console.WriteLine("Reading barcodes with imported settings:");
                // Read barcodes again using the imported configuration.
                foreach (var result in importedReader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeText: {result.CodeText}, Type: {result.CodeTypeName}");
                }
            }
        }

        // Optional cleanup: delete the temporary folder and its contents.
        // Uncomment the line below to remove generated files after execution.
        // Directory.Delete(tempFolder, true);
    }
}
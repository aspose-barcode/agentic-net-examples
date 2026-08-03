// Title: Save BarCodeReader State to XML MemoryStream
// Description: Demonstrates exporting a BarCodeReader's configuration to an XML memory stream and later importing it for barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image, BarCodeReader to read it, and the ExportToXml/ImportFromXml methods to serialize and deserialize reader settings. Developers often need to persist reader configurations across sessions or share them between services, making XML serialization a practical approach.
// Prompt: Save the reader state to a memory stream in XML format for later deserialization.
// Tags: code128, barcode generation, barcode recognition, xml serialization, memory stream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, exports the reader's state to an XML
/// memory stream, imports it back, and performs barcode recognition using the imported state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, state export/import,
    /// and recognition workflow.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a reader for the generated image.
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Export the reader's configuration (state) to an XML memory stream.
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        // Reset the stream position to the beginning before reading.
                        xmlStream.Position = 0;

                        // Import a new reader instance from the XML stream.
                        using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // Assign the same barcode image to the imported reader.
                            importedReader.SetBarCodeImage(barcodeImage);

                            // Perform barcode recognition using the imported reader.
                            foreach (var result in importedReader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected Code Type: {result.CodeType}");
                                Console.WriteLine($"Detected Code Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}
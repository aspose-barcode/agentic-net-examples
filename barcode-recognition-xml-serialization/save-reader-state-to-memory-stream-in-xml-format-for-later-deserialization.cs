// Title: Export and Import Barcode Reader State via XML
// Description: Demonstrates saving a BarCodeReader's state to an XML memory stream and later restoring it for reuse.
// Prompt: Save the reader state to a memory stream in XML format for later deserialization.
// Tags: code128, generation, recognition, xml, memorystream, export, import, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, exports the reader state to XML,
/// and then imports the state to verify that recognition still works.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, state export, and import.
    /// </summary>
    static void Main()
    {
        // Create a simple Code128 barcode and generate its image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a reader for the generated image.
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Perform a read to ensure the reader is initialized and display the result.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Original read: {result.CodeText}");
                    }

                    // Save the reader's state to a memory stream in XML format.
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        Console.WriteLine($"Reader state exported to XML (size: {xmlStream.Length} bytes).");

                        // Reset the stream position before deserialization.
                        xmlStream.Position = 0;

                        // Deserialize the reader state from the XML stream.
                        using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // The imported reader needs the image to perform recognition.
                            importedReader.SetBarCodeImage(barcodeImage);

                            // Verify that the imported reader works by reading the barcode again.
                            foreach (var importedResult in importedReader.ReadBarCodes())
                            {
                                Console.WriteLine($"Imported read: {importedResult.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}
// Title: Read barcode from memory, export/import reader state via XML
// Description: Demonstrates generating a barcode image in memory, reading it, exporting the reader state to an XML stream, and then importing that state to continue processing without reloading the image file.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to use BarCodeReader's ExportToXml and ImportFromXml methods. Developers working with barcode recognition often need to persist reader configuration or processing state, especially when handling images in memory or across application boundaries. Typical use cases include caching recognition results, transferring state between services, or resuming processing after a pause.
// Prompt: Show how to read an XML state from a MemoryStream and continue processing without reloading the image file.
// Tags: barcode, code128, memorystream, xml, state, export, import, aspose.barcode, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it, exports the reader state to XML,
/// imports the state, and reads the barcode again without reloading the image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, reading, state export,
    /// and state import workflow.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode
        string codeText = "1234567890";

        // Create a barcode generator for Code128 and configure its appearance
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode image into a memory stream (PNG format)
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                byte[] imageBytes = imageStream.ToArray();

                // -----------------------------------------------------------------
                // First read: use BarCodeReader directly on the in‑memory image
                // -----------------------------------------------------------------
                using (var reader = new BarCodeReader(new MemoryStream(imageBytes), DecodeType.AllSupportedTypes))
                {
                    var firstResults = reader.ReadBarCodes();
                    Console.WriteLine("First read results:");
                    foreach (var result in firstResults)
                    {
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }

                    // -------------------------------------------------------------
                    // Export the current reader state (including settings) to XML
                    // -------------------------------------------------------------
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        xmlStream.Position = 0; // Reset stream position for reading

                        // ---------------------------------------------------------
                        // Import a new BarCodeReader instance from the exported XML
                        // ---------------------------------------------------------
                        using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // Re‑assign the same barcode image without loading from disk
                            using (var imgStream = new MemoryStream(imageBytes))
                            {
                                importedReader.SetBarCodeImage(imgStream);

                                // Perform a second read using the imported reader state
                                var importedResults = importedReader.ReadBarCodes();
                                Console.WriteLine("Read after importing state:");
                                foreach (var result in importedResults)
                                {
                                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
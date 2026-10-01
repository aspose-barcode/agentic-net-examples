// Title: Deserialize BarCodeReader Settings from XML and Reapply Image
// Description: Demonstrates exporting a BarCodeReader's configuration to XML, then importing it and reusing the same barcode image for recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to persist and restore BarCodeReader settings via XML. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and methods ExportToXml/ImportFromXml. Developers often need to save reader configurations for later reuse or transfer across services, especially when processing the same barcode image multiple times.
// Prompt: Deserialize the reader state from an XML stream, then reapply the same barcode image for analysis.
// Tags: barcode, symbology, generation, recognition, xml, import, export, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to export a BarCodeReader's state to XML,
/// import it back, and reuse the same barcode image for decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, exports the reader settings to XML,
    /// reimports them, and performs recognition on the same image.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode
        string codeText = "Sample123";

        // Generate a barcode image in memory using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the generated barcode to a memory stream in PNG format
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream for reading

                // Create a BarCodeReader with default settings to read the image
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Export the reader's configuration to an XML stream
                    using (var xmlStream = new MemoryStream())
                    {
                        reader.ExportToXml(xmlStream);
                        xmlStream.Position = 0; // Reset XML stream for import

                        // Import a new BarCodeReader instance from the exported XML
                        using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                        {
                            // Reset the image stream position before reusing it
                            imageStream.Position = 0;

                            // Apply the same barcode image to the imported reader
                            importedReader.SetBarCodeImage(imageStream);

                            // Perform barcode recognition using the imported settings
                            BarCodeResult[] results = importedReader.ReadBarCodes();

                            // Output the recognition results to the console
                            if (results != null && results.Length > 0)
                            {
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"CodeText: {result.CodeText}");
                                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No barcode detected.");
                            }
                        }
                    }
                }
            }
        }
    }
}
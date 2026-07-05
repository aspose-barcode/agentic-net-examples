// Title: Deserialize and Reapply Barcode Image for Recognition
// Description: Demonstrates exporting a BarCodeReader's configuration to XML, then importing it into a new reader and applying the same barcode image for analysis.
// Prompt: Deserialize the reader state from an XML stream, then reapply the same barcode image for analysis.
// Tags: barcode, serialization, deserialization, xml, recognition, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to export a BarCodeReader's state to XML,
/// import it into a new reader instance, and reuse the same barcode image for detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, exports reader settings to XML,
    /// imports them into a new reader, and reads the barcode again.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a reader for the generated image
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    // Export the reader's state to an XML stream
                    using (var exportStream = new MemoryStream())
                    {
                        reader.ExportToXml(exportStream);
                        exportStream.Position = 0; // Reset stream position for reading

                        // Create a new reader instance without initial image
                        using (var newReader = new BarCodeReader())
                        {
                            // Import the previously exported settings into the new reader
                            BarCodeReader.ImportFromXml(exportStream);

                            // Apply the same barcode image to the new reader
                            newReader.SetBarCodeImage(barcodeImage);

                            // Perform recognition using the imported settings
                            foreach (var result in newReader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Detected Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}
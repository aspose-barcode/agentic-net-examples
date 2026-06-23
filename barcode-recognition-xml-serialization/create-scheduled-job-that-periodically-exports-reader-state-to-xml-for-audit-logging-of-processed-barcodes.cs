using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes, reading them, and exporting the reader state to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates sample barcodes, reads them, and logs detection results and reader state.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample barcodes (symbology type and associated text)
        var samples = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.EAN13, "1234567890128")
        };

        int index = 0; // Used to differentiate output files for each sample

        // Process each sample barcode definition
        foreach (var sample in samples)
        {
            // Create a memory stream to hold the generated barcode image
            using (var ms = new MemoryStream())
            {
                // Generate the barcode image and write it to the memory stream
                using (var generator = new BarcodeGenerator(sample.type, sample.text))
                {
                    // Save the barcode as a PNG image
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Initialize a barcode reader that supports all available symbologies
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Iterate over all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output detection details to the console
                        Console.WriteLine($"[{index}] Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    // Build a unique file name for exporting the reader's internal state
                    string xmlPath = $"ReaderState_{index}.xml";

                    // Attempt to export the reader state (settings and results) to an XML file
                    try
                    {
                        reader.ExportToXml(xmlPath);
                        Console.WriteLine($"Reader state exported to: {xmlPath}");
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur during export
                        Console.WriteLine($"Failed to export reader state: {ex.Message}");
                    }
                }
            }

            index++; // Increment index for the next sample
        }

        // Indicate that all processing is complete
        Console.WriteLine("Processing completed.");
    }
}
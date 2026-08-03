// Title: Recognize Barcode from Image Stream and Export XML State
// Description: Demonstrates how to read a barcode from an in‑memory image stream using Aspose.BarCode and return the reader’s XML state as a string.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows the use of BarCodeReader with DecodeType.AllSupportedTypes to detect any supported symbology, and how to export the reader configuration and results to XML via ExportToXml. Developers working on barcode scanning, automated data capture, or integration testing often need to programmatically obtain detailed recognition information in XML for logging or further processing.
// Prompt: Create a function that accepts an image stream, performs recognition, and returns the XML state as a string.
// Tags: barcode symbology, recognition, xml, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, recognizes it from a memory stream,
/// and returns the recognition results as an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Recognizes barcodes from the provided image stream and returns the reader's XML state.
    /// </summary>
    /// <param name="imageStream">A stream containing the barcode image.</param>
    /// <returns>XML string representing the reader configuration and detection results.</returns>
    static string RecognizeBarcodeXml(Stream imageStream)
    {
        // Ensure the stream is positioned at the beginning before reading.
        if (imageStream.CanSeek)
        {
            imageStream.Position = 0;
        }

        // Initialize the reader to detect all supported barcode types.
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition process.
            reader.ReadBarCodes();

            // Export the reader's configuration and results to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset position for reading.

                // Read the XML content as a UTF‑8 string.
                using (var sr = new StreamReader(xmlStream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, recognizes it,
    /// and writes the resulting XML to the console.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Store the generated barcode image in a memory stream.
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode as a PNG image.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream before recognition.

                // Recognize the barcode and obtain the XML representation.
                string xmlResult = RecognizeBarcodeXml(imageStream);

                // Output the XML result to the console.
                Console.WriteLine(xmlResult);
            }
        }
    }
}
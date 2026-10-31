// Title: Barcode Generation, Recognition, and XML Export Example
// Description: Demonstrates generating a QR code, recognizing it from a stream, and exporting the recognition result as XML.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to detect and decode them, and ExportToXml to obtain a detailed XML representation of the recognition state. Developers working with barcode imaging, automated scanning, or data interchange often need to generate barcodes, read them from various sources, and serialize results for logging or further processing.
// Prompt: Create a function that accepts an image stream, performs recognition, and returns the XML state as a string.
// Tags: barcode, qr, generation, recognition, xml, export, aspose.barcode, stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and XML export using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, recognizes it, and prints the XML state.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode image as PNG into the stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Recognize the barcode from the stream and obtain the XML state
                string xmlState = RecognizeAndExportXml(imageStream);
                Console.WriteLine("Recognition XML State:");
                Console.WriteLine(xmlState);
            }
        }
    }

    /// <summary>
    /// Recognizes a barcode from the provided image stream and returns the recognition result as an XML string.
    /// </summary>
    /// <param name="imageStream">A stream containing the barcode image.</param>
    /// <returns>XML representation of the barcode recognition state.</returns>
    static string RecognizeAndExportXml(Stream imageStream)
    {
        // Ensure the input stream is seekable for the BarCodeReader
        using (var seekableStream = new MemoryStream())
        {
            // Copy the original stream into a seekable memory stream
            imageStream.CopyTo(seekableStream);
            // Reset position to the beginning before reading
            seekableStream.Position = 0;

            // Configure the reader to decode all supported barcode types
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (var reader = new BarCodeReader(seekableStream, decodeType))
            {
                // Export the recognition result to an XML memory stream
                using (var xmlStream = new MemoryStream())
                {
                    reader.ExportToXml(xmlStream);
                    // Reset position to read the XML content
                    xmlStream.Position = 0;
                    using (var sr = new StreamReader(xmlStream))
                    {
                        // Return the entire XML as a string
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
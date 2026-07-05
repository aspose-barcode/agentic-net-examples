// Title: Barcode recognition with XML state export
// Description: Demonstrates how to read barcodes from an image stream using Aspose.BarCode and export the reader's internal state as XML.
// Prompt: Create a function that accepts an image stream, performs recognition, and returns the XML state as a string.
// Tags: barcode, recognition, xml, aspose.barcode, stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a barcode, recognizes it from a stream,
/// and outputs the reader's XML state.
/// </summary>
class Program
{
    /// <summary>
    /// Recognizes barcodes from an image stream and returns the reader's XML state.
    /// </summary>
    /// <param name="imageStream">Stream containing the barcode image.</param>
    /// <returns>XML string representing the reader's configuration and results.</returns>
    static string RecognizeAndExportXml(Stream imageStream)
    {
        if (imageStream == null)
            throw new ArgumentNullException(nameof(imageStream));

        // Initialize the reader with all supported decode types.
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Perform the actual recognition.
            reader.ReadBarCodes();

            // Export the reader's configuration/state to XML.
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading.

                using (var sr = new StreamReader(xmlStream))
                {
                    // Return the entire XML content as a string.
                    return sr.ReadToEnd();
                }
            }
        }
    }

    /// <summary>
    /// Entry point that generates a sample barcode, runs recognition, and prints the XML state.
    /// </summary>
    static void Main()
    {
        // Create a sample barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    // Save the bitmap to a PNG stream.
                    bitmap.Save(imageStream, ImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Recognize and obtain XML state.
                    string xml = RecognizeAndExportXml(imageStream);
                    Console.WriteLine("Reader XML State:");
                    Console.WriteLine(xml);
                }
            }
        }
    }
}
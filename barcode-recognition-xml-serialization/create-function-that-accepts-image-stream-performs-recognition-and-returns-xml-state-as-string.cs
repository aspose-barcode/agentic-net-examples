// Title: Generate QR barcode, recognize it, and export XML state
// Description: Demonstrates creating a QR code, reading it from a memory stream, and obtaining the recognition result as an XML string.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include generating barcodes for display or printing, scanning images to extract data, and exporting the recognition state for logging or further processing. Developers often need to work with streams and export results in XML for integration with other systems.
// Prompt: Create a function that accepts an image stream, performs recognition, and returns the XML state as a string.
// Tags: qr, barcode, generation, recognition, xml, aspose.barcode, stream

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, recognition, and XML export using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, recognizes it, and prints the XML state.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and obtain its image as a stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a QR code with the text "Hello Aspose"
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
            {
                // Save the generated barcode to the memory stream in PNG format
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning before reading
            barcodeStream.Position = 0;

            // Perform recognition and get XML state
            string xmlState = RecognizeAndExportXml(barcodeStream);
            Console.WriteLine("Recognition XML State:");
            Console.WriteLine(xmlState);
        }
    }

    /// <summary>
    /// Recognizes barcodes from the provided image stream and returns the recognition state as an XML string.
    /// </summary>
    /// <param name="imageStream">Stream containing the barcode image.</param>
    /// <returns>XML representation of the recognition state.</returns>
    static string RecognizeAndExportXml(Stream imageStream)
    {
        // Ensure the stream is positioned at the beginning
        if (imageStream.CanSeek)
        {
            imageStream.Position = 0;
        }

        // Create a reader that can decode all supported barcode types
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Perform recognition to populate the internal state
            reader.ReadBarCodes();

            // Export the recognition state to an XML stream
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0;

                // Read the XML content as a string and return it
                using (var readerStream = new StreamReader(xmlStream))
                {
                    return readerStream.ReadToEnd();
                }
            }
        }
    }
}
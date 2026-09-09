// Title: Generate QR barcode, recognize it, and export XML state
// Description: This example creates a QR barcode in memory, reads it using Aspose.BarCode's BarCodeReader, and returns the recognition result as an XML string.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and ExportToXml for obtaining detailed recognition data. Typical scenarios include barcode verification, automated data extraction, and integration testing where developers need programmatic access to barcode metadata.
// Prompt: Create a function that accepts an image stream, performs recognition, and returns the XML state as a string.
// Tags: qr, barcode, generation, recognition, xml, export, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a QR barcode, recognizing it, and exporting the recognition state as XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, recognizes it, and prints the XML state.
    /// </summary>
    static void Main()
    {
        // Create a sample QR barcode and store it in a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode image to the stream in PNG format
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Recognize the barcode and obtain the XML state
                string xmlState = RecognizeAndExportState(imageStream);
                Console.WriteLine("Recognition XML State:");
                Console.WriteLine(xmlState);
            }
        }
    }

    /// <summary>
    /// Recognizes a barcode from the provided image stream and returns the recognition state as an XML string.
    /// </summary>
    /// <param name="imageStream">Stream containing the barcode image.</param>
    /// <returns>XML representation of the recognition state.</returns>
    static string RecognizeAndExportState(Stream imageStream)
    {
        if (imageStream == null)
            throw new ArgumentNullException(nameof(imageStream));

        // Ensure the stream is positioned at the beginning before reading
        if (imageStream.CanSeek)
            imageStream.Position = 0;

        // Initialize the barcode reader for all supported types
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Perform recognition (optional, but ensures state reflects read results)
            reader.ReadBarCodes();

            // Export the recognition state to an XML stream
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                // Reset XML stream position to read its contents
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
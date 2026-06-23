using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, recognition, and exporting the reader's state as XML.
/// </summary>
class Program
{
    /// <summary>
    /// Recognizes barcodes from an image stream and returns the reader's XML state.
    /// </summary>
    /// <param name="imageStream">Stream containing the barcode image.</param>
    /// <returns>XML representation of the reader's state after recognition.</returns>
    static string RecognizeAndExportXml(Stream imageStream)
    {
        if (imageStream == null)
            throw new ArgumentException("Image stream cannot be null.");

        // Ensure the stream is positioned at the beginning before reading.
        imageStream.Position = 0;

        // Create a reader that checks all supported symbologies.
        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
        {
            // Perform recognition (optional, but ensures results are populated).
            reader.ReadBarCodes();

            // Export the reader's state to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset to beginning for reading.

                // Read the XML content as a string.
                using (var readerStream = new StreamReader(xmlStream))
                {
                    return readerStream.ReadToEnd();
                }
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a sample barcode, recognizes it, and prints the XML state.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode as PNG into the stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);

                // Reset stream position before recognition.
                imageStream.Position = 0;

                // Recognize the barcode and obtain the XML state.
                string xmlState = RecognizeAndExportXml(imageStream);

                // Output the XML state to the console.
                Console.WriteLine("Reader XML State:");
                Console.WriteLine(xmlState);
            }
        }
    }
}
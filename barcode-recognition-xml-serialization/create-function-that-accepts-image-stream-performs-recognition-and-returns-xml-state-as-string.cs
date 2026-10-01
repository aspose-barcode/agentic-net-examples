// Title: Barcode Recognition to XML from Image Stream
// Description: Demonstrates how to read barcodes from an image stream using Aspose.BarCode and return the results as an XML string.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the BarCodeReader class for detecting all supported barcode symbologies, extracting key properties such as code text, type, and reading quality, and formatting the output as XML. Developers working with barcode scanning, inventory systems, or document processing often need to programmatically retrieve barcode data and serialize it for further analysis or integration.
/// Prompt: Create a function that accepts an image stream, performs recognition, and returns the XML state as a string.
/// Tags: barcode, recognition, xml, aspose.barcode, csharp

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of generating a QR code, recognizing it from a stream,
/// and returning the recognition results as an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Recognizes barcodes from the supplied image stream and returns the results formatted as XML.
    /// </summary>
    /// <param name="imageStream">A stream containing the barcode image.</param>
    /// <returns>An XML string describing each detected barcode.</returns>
    static string RecognizeBarcodeXml(Stream imageStream)
    {
        // Ensure the stream is positioned at the beginning for reading.
        if (imageStream.CanSeek)
        {
            imageStream.Position = 0;
        }

        // Configure the reader to detect all supported barcode types.
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Initialize the BarCodeReader with the image stream and decode settings.
        using (var reader = new BarCodeReader(imageStream, decodeType))
        {
            // Execute the recognition process and collect all results.
            var results = reader.ReadBarCodes();

            // Construct an XML document that lists each barcode's details.
            var doc = new XDocument(
                new XElement("BarCodeResults",
                    results.Select(r =>
                        new XElement("BarCodeResult",
                            new XElement("CodeText", r.CodeText ?? string.Empty),
                            new XElement("CodeTypeName", r.CodeTypeName ?? string.Empty),
                            new XElement("ReadingQuality", r.ReadingQuality.ToString())
                        )
                    )
                )
            );

            // Return the XML as a formatted string.
            return doc.ToString();
        }
    }

    /// <summary>
    /// Generates a sample QR code, recognizes it using <see cref="RecognizeBarcodeXml"/>,
    /// and writes the resulting XML to the console.
    /// </summary>
    static void Main()
    {
        // Create a QR code generator with the text "Hello World".
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Store the generated barcode image in a memory stream.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);

                // Reset the stream position before passing it to the recognizer.
                barcodeStream.Position = 0;

                // Perform recognition and obtain the XML representation.
                string xmlResult = RecognizeBarcodeXml(barcodeStream);

                // Output the XML result to the console.
                Console.WriteLine("Recognition XML:");
                Console.WriteLine(xmlResult);
            }
        }
    }
}
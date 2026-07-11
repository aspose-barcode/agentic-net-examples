// Title: Verify custom CustomerInformationDecoder receives raw barcode bytes
// Description: Demonstrates how to attach a custom CustomerInformationDecoder to an Australia Post barcode reader and confirm it receives the raw data before interpretation.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on custom decoding of Australia Post customer information. It showcases the use of BarcodeGenerator, BarCodeReader, and the AustraliaPostCustomerInformationDecoder API to customize data handling, a common need when integrating barcode data with legacy systems or performing raw data validation. Developers can adapt this pattern for other symbologies and custom decoders.
// Prompt: Write a unit test verifying custom CustomerInformationDecoder receives raw barcode bytes before interpretation.
// Tags: australia post, customer information, decoder, custom decoder, barcode generation, barcode recognition, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Custom decoder that captures the raw data passed from the barcode reader.
/// Inherits from <see cref="AustraliaPostCustomerInformationDecoder"/> to integrate with the Australia Post decoding pipeline.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Gets the raw data received by the decoder.
    /// </summary>
    public string ReceivedData { get; private set; }

    /// <summary>
    /// Stores the incoming data and returns it unchanged for testing purposes.
    /// </summary>
    /// <param name="data">Raw data string supplied by the barcode reader.</param>
    /// <returns>The same data string that was received.</returns>
    public string Decode(string data)
    {
        ReceivedData = data;
        // Return the raw data unchanged for this test
        return data;
    }
}

/// <summary>
/// Entry point for the example that generates an Australia Post barcode,
/// reads it with a custom decoder, and verifies the decoder receives the raw bytes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode, reads it using a custom <see cref="MyDecoder"/>,
    /// and checks that the decoder was invoked with the expected raw data.
    /// </summary>
    static void Main()
    {
        // Generate an Australia Post barcode with sample data and store it in a memory stream
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678ABCde"))
            {
                // Use CTable interpreting type for customer information
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;
                // Save the generated barcode image to the stream in PNG format
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            imageStream.Position = 0;

            // Prepare the custom decoder instance
            var customDecoder = new MyDecoder();

            // Create a barcode reader configured for Australia Post symbology
            using (var reader = new BarCodeReader(imageStream, DecodeType.AustraliaPost))
            {
                // Assign the custom decoder to the AustraliaPost settings
                reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = customDecoder;

                // Perform recognition and output detected code texts
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                }
            }

            // Verify that the decoder received the raw barcode bytes
            if (!string.IsNullOrEmpty(customDecoder.ReceivedData))
            {
                Console.WriteLine("PASS: Custom decoder received raw barcode data.");
                Console.WriteLine($"Raw data passed to decoder: {customDecoder.ReceivedData}");
            }
            else
            {
                Console.WriteLine("FAIL: Custom decoder did not receive raw barcode data.");
            }
        }
    }
}
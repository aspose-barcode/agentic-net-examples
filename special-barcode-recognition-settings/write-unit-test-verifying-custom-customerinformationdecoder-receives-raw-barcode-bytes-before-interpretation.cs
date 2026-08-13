// Title: Unit test for custom CustomerInformationDecoder in Australia Post barcode
// Description: Demonstrates how to generate an Australia Post barcode, apply a custom CustomerInformationDecoder, and verify that the decoder receives the raw customer information field.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and the AustraliaPostCustomerInformationDecoder API classes. Developers often need to customize decoding of specific barcode fields, such as the customer information segment in Australia Post barcodes, to access raw data before standard interpretation. The pattern shown here is common for unit testing custom decoders in automated pipelines.
// Prompt: Write a unit test verifying custom CustomerInformationDecoder receives raw barcode bytes before interpretation.
// Tags: australia post, custom decoder, barcode generation, barcode recognition, unit test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point that demonstrates a unit‑test‑style verification of a custom
/// <c>AustraliaPostCustomerInformationDecoder</c>. The program generates a barcode, reads it back,
/// and checks that the decoder receives the raw customer information field.
/// </summary>
class Program
{
    /// <summary>
    /// Generates an Australia Post barcode, applies a custom decoder, and validates that the decoder
    /// was invoked with the raw data. Results are written to the console.
    /// </summary>
    static void Main()
    {
        // Instantiate the custom decoder that will capture the raw customer information field.
        var decoder = new TestCustomerInformationDecoder();

        // Create a barcode generator for the Australia Post symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678ABCde"))
        {
            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize a barcode reader for the generated image and configure it to use the custom decoder.
                using (var reader = new BarCodeReader(ms, DecodeType.AustraliaPost))
                {
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = decoder;

                    // Perform barcode recognition.
                    var results = reader.ReadBarCodes();

                    // Determine whether at least one barcode was detected.
                    bool barcodeFound = results != null && results.Length > 0;

                    // Verify that the custom decoder received non‑empty raw data.
                    bool decoderInvoked = !string.IsNullOrEmpty(decoder.ReceivedRawData);

                    // Output test outcome.
                    if (barcodeFound && decoderInvoked)
                    {
                        Console.WriteLine("PASS: Barcode recognized and custom decoder received raw data.");
                        Console.WriteLine($"Decoder raw data: {decoder.ReceivedRawData}");
                    }
                    else
                    {
                        Console.WriteLine("FAIL: Test conditions not met.");
                        Console.WriteLine($"Barcode found: {barcodeFound}");
                        Console.WriteLine($"Decoder invoked: {decoderInvoked}");
                    }
                }
            }
        }
    }
}

/// <summary>
/// Custom implementation of <c>AustraliaPostCustomerInformationDecoder</c> used for testing.
/// It records the raw customer information field passed during decoding.
/// </summary>
class TestCustomerInformationDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Gets the raw customer information data received from the barcode reader.
    /// </summary>
    public string ReceivedRawData { get; private set; }

    /// <summary>
    /// Called by the barcode reader with the raw customer information field.
    /// Stores the raw data and returns a placeholder decoded string.
    /// </summary>
    /// <param name="customerInformationField">The raw customer information field extracted from the barcode.</param>
    /// <returns>A placeholder decoded string.</returns>
    public string Decode(string customerInformationField)
    {
        ReceivedRawData = customerInformationField;
        // For testing purposes, return a simple placeholder.
        return "DecodedInfo";
    }
}
// Title: Verify custom CustomerInformationDecoder receives raw barcode data
// Description: Demonstrates generating an Australia Post barcode, applying a custom decoder, and confirming the decoder receives the original field string.
// Category-Description: This example belongs to Aspose.BarCode barcode generation and recognition. It shows how to use BarcodeGenerator, BarCodeReader, and the AustraliaPostCustomerInformationDecoder to customize interpretation of customer information fields. Developers often need to plug in custom logic for decoding barcode data, especially for postal services, and this snippet illustrates that pattern.
// Prompt: Write a unit test verifying custom CustomerInformationDecoder receives raw barcode bytes before interpretation.
// Tags: australia post, barcode generation, barcode recognition, custom decoder, customer information, unit test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder that captures the raw customer information field passed during decoding.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Gets the last input string received by the decoder.
    /// </summary>
    public static string LastInput { get; private set; }

    /// <summary>
    /// Stores the provided customer information field and returns it unchanged.
    /// </summary>
    /// <param name="customerInformationField">The raw field string from the barcode.</param>
    /// <returns>The same field string.</returns>
    public string Decode(string customerInformationField)
    {
        LastInput = customerInformationField;
        // Return the field unchanged for this test
        return customerInformationField;
    }
}

/// <summary>
/// Example program that generates an Australia Post barcode, reads it with a custom decoder,
/// and verifies that the decoder receives the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, reading, and verification.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "AustraliaPost.png");

        // Original data to encode in the barcode
        string originalCodeText = "620123456701234";

        // Generate an Australia Post barcode with specific parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, originalCodeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode using a custom decoder
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Assign the custom decoder to capture raw field data
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyDecoder();

            // Ensure the interpreting type matches the generation settings
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

            // Iterate through detected barcodes (expecting one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Result CodeText: {result.CodeText}");
            }
        }

        // Verify that the custom decoder was invoked and received the expected data
        if (MyDecoder.LastInput == null)
        {
            Console.WriteLine("FAILED: Custom decoder was not invoked.");
        }
        else if (MyDecoder.LastInput == originalCodeText)
        {
            Console.WriteLine("PASSED: Decoder received raw barcode bytes (as string) correctly.");
        }
        else
        {
            Console.WriteLine($"FAILED: Decoder received unexpected data. Expected '{originalCodeText}', got '{MyDecoder.LastInput}'.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}
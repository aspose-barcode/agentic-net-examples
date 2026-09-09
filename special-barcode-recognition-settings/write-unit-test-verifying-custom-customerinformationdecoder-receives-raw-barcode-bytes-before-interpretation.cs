// Title: Custom CustomerInformationDecoder with Raw Barcode Byte Verification
// Description: Demonstrates generating an Australia Post barcode, reading it with a custom CustomerInformationDecoder, and verifying that raw barcode bytes are available before interpretation.
// Category-Description: Shows Aspose.BarCode barcode generation and recognition for Australia Post symbology, focusing on the CustomerInformationDecoder API. Typical use cases include customizing how customer information fields are parsed and accessing raw code bytes for validation. Developers working with barcode decoding often need to inject custom decoders and compare raw byte data with interpreted text.
// Prompt: Write a unit test verifying custom CustomerInformationDecoder receives raw barcode bytes before interpretation.
// Tags: australia post,barcode generation,barcode recognition,customerinformationdecoder,raw bytes,unit test,aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder that captures the last decoded customer information field.
/// Inherits from <see cref="AustraliaPostCustomerInformationDecoder"/> to integrate with Aspose.BarCode.
/// </summary>
public class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Gets the most recent decoded customer information string.
    /// </summary>
    public string LastDecoded { get; private set; }

    /// <summary>
    /// Stores the provided customer information field and returns it unchanged.
    /// </summary>
    /// <param name="customerInformationField">The raw customer information string extracted from the barcode.</param>
    /// <returns>The same string that was passed in.</returns>
    public string Decode(string customerInformationField)
    {
        LastDecoded = customerInformationField;
        return customerInformationField;
    }
}

/// <summary>
/// Example program that generates an Australia Post barcode, reads it using a custom decoder,
/// and validates that raw barcode bytes match the interpreted text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, reading, validation, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts.
        string tempDir = Path.Combine(Path.GetTempPath(), "AustraliaPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // ------------------------------------------------------------
        // Generate an Australia Post barcode with specific parameters.
        // ------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4f;
            gen.Parameters.Barcode.BarHeight.Pixels = 50f;
            gen.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Set up the custom decoder and read the generated barcode.
        // ------------------------------------------------------------
        var decoder = new MyDecoder();
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Attach the custom decoder to the reader settings.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = decoder;

            // Iterate over all detected barcodes (should be one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // --------------------------------------------------------
                // Verify that raw bytes are available and match the CodeText.
                // --------------------------------------------------------
                byte[] rawBytes = result.CodeBytes;
                byte[] expectedBytes = Encoding.ASCII.GetBytes(result.CodeText);
                bool bytesMatch = rawBytes != null && rawBytes.Length == expectedBytes.Length;

                if (bytesMatch)
                {
                    for (int i = 0; i < rawBytes.Length; i++)
                    {
                        if (rawBytes[i] != expectedBytes[i])
                        {
                            bytesMatch = false;
                            break;
                        }
                    }
                }

                // Output verification results.
                Console.WriteLine(bytesMatch
                    ? "PASS: Raw barcode bytes match CodeText."
                    : "FAIL: Raw barcode bytes do not match CodeText.");

                Console.WriteLine(decoder.LastDecoded != null
                    ? "PASS: Custom CustomerInformationDecoder was invoked."
                    : "FAIL: Custom CustomerInformationDecoder was not invoked.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result.
        }
    }
}
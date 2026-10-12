// Title: Custom CustomerInformationDecoder verification for Australia Post barcode
// Description: Demonstrates how to attach a custom CustomerInformationDecoder to an Australia Post barcode reader and verify it receives the raw customer information string and bytes.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, focusing on custom decoding of Australia Post barcodes. It showcases the use of BarcodeGenerator, BarCodeReader, and the AustraliaPostCustomerInformationDecoder class to customize interpretation of customer information fields. Developers often need to plug in their own logic for parsing encoded data, and this snippet illustrates typical setup, generation, reading, and validation steps.
// Prompt: Write a unit test verifying custom CustomerInformationDecoder receives raw barcode bytes before interpretation.
// Tags: australia post, barcode decoding, custom decoder, customer information, aspose.barcode, unit test

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder that records the last decoded customer information field.
/// Inherits from <see cref="AustraliaPostCustomerInformationDecoder"/> to integrate with Aspose.BarCode.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Gets the most recent customer information field passed to <see cref="Decode(string)"/>.
    /// </summary>
    public string LastDecodedField { get; private set; }

    /// <summary>
    /// Stores the incoming field and returns it unchanged.
    /// </summary>
    /// <param name="customerInformationField">Raw customer information string extracted from the barcode.</param>
    /// <returns>The same string that was received.</returns>
    public string Decode(string customerInformationField)
    {
        LastDecodedField = customerInformationField;
        return customerInformationField;
    }
}

/// <summary>
/// Example program that generates an Australia Post barcode, reads it with a custom decoder,
/// and validates that the decoder receives the correct raw data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it using a custom decoder,
    /// and prints verification results to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working directory for the barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AusPostTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "auspost.png");

        // Sample Australia Post code:
        // FCC=59, DPID=01234567, customer info "12345" (NTable digits)
        string codeText = "590123456712345";

        // --------------------------------------------------------------------
        // Generate the barcode image using Aspose.BarCode.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // --------------------------------------------------------------------
        // Set up the custom decoder instance.
        // --------------------------------------------------------------------
        MyDecoder decoder = new MyDecoder();

        // --------------------------------------------------------------------
        // Read the barcode and attach the custom decoder.
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = decoder;

            BarCodeResult result = null;

            // Expect only a single barcode in the image.
            foreach (BarCodeResult r in reader.ReadBarCodes())
            {
                result = r;
                break;
            }

            if (result == null)
            {
                Console.WriteLine("FAILED: No barcode detected.");
                return;
            }

            // --------------------------------------------------------------------
            // Extract the customer information part (characters after the first 10).
            // --------------------------------------------------------------------
            string expectedCustomerInfo = result.CodeText.Length > 10 ? result.CodeText.Substring(10) : string.Empty;

            // Verify that the custom decoder received the exact string.
            if (decoder.LastDecodedField == expectedCustomerInfo)
            {
                Console.WriteLine("PASS: Decoder received correct raw customer information.");
            }
            else
            {
                Console.WriteLine("FAILED: Decoder input mismatch.");
                Console.WriteLine($"Expected: '{expectedCustomerInfo}'");
                Console.WriteLine($"Actual:   '{decoder.LastDecodedField}'");
            }

            // --------------------------------------------------------------------
            // Optional: verify that the raw bytes match the full code text.
            // --------------------------------------------------------------------
            byte[] codeBytes = result.CodeBytes;
            string bytesAsString = Encoding.ASCII.GetString(codeBytes);
            if (bytesAsString == result.CodeText)
            {
                Console.WriteLine("PASS: CodeBytes match CodeText.");
            }
            else
            {
                Console.WriteLine("FAILED: CodeBytes do not match CodeText.");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directory.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup exceptions.
        }
    }
}
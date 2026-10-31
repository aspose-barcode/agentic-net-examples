// Title: Custom Australia Post Barcode Decoding with Reverse Customer Information
// Description: Demonstrates how to generate an Australia Post barcode, configure custom decoding settings, and apply a user‑defined customer information decoder to reverse the data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for reading them, and AustraliaPostSettings for customizing decoding behavior. Developers often need to tailor Australia Post barcode interpretation, such as handling custom encoding tables or providing a custom CustomerInformationDecoder, to meet specific business requirements.
// Prompt: Instantiate AustraliaPostSettings and assign it to BarCodeReader.RecognitionSettings for custom Australia Post decoding.
// Tags: australia post, barcode, decoding, custom decoder, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Custom decoder that reverses the customer information field string.
/// </summary>
class ReverseDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Reverses the supplied customer information field.
    /// </summary>
    /// <param name="customerInformationField">The original customer information string.</param>
    /// <returns>The reversed string, or the original if null/empty.</returns>
    public string Decode(string customerInformationField)
    {
        if (string.IsNullOrEmpty(customerInformationField))
            return customerInformationField;

        char[] chars = customerInformationField.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}

/// <summary>
/// Demonstrates custom decoding of Australia Post barcodes using a user‑defined decoder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for the barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeAustraliaPostDemo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPost.png");

        // Generate a sample Australia Post barcode with specific parameters.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567ASPOSE"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using custom Australia Post settings.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Configure Australia Post specific decoding options.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;
            reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;
            // Assign the custom decoder that reverses the customer information field.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new ReverseDecoder();

            // Perform the barcode reading operation.
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Output the decoded barcode information.
            foreach (var result in results)
            {
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}
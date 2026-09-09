// Title: Custom Customer Information Decoder for Australia Post Barcodes
// Description: Demonstrates how to implement a custom CustomerInformationDecoder and assign it to AustraliaPostSettings to decode the customer information field of an Australia Post barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and AustraliaPostSettings to create a barcode, apply a custom decoder (derived from CustomerInformationDecoder), and read the decoded data. Developers working with postal services often need to customize how customer information is interpreted; this pattern provides a reusable approach for such scenarios.
// Prompt: Implement a custom class inheriting CustomerInformationDecoder and assign it to AustraliaPostSettings.CustomDecoder.
// Tags: australia post, custom decoder, barcode generation, barcode recognition, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder that reverses the customer information field.
/// Inherits from <see cref="AustraliaPostCustomerInformationDecoder"/> to integrate with Australia Post settings.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Decodes the supplied customer information field.
    /// This simple implementation returns the reversed string.
    /// </summary>
    /// <param name="customerInformationField">The raw customer information field extracted from the barcode.</param>
    /// <returns>The decoded string.</returns>
    public string Decode(string customerInformationField)
    {
        // Return an empty string if the input is null or empty.
        if (string.IsNullOrEmpty(customerInformationField))
            return string.Empty;

        // Build the reversed string using a StringBuilder for efficiency.
        var sb = new StringBuilder(customerInformationField.Length);
        for (int i = customerInformationField.Length - 1; i >= 0; i--)
            sb.Append(customerInformationField[i]);

        return sb.ToString();
    }
}

/// <summary>
/// Demonstrates generating an Australia Post barcode, applying a custom decoder,
/// reading the barcode, and cleaning up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AusPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "auspost.png");

        // Generate an Australia Post barcode with specific dimensions and encoding table.
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode using a custom customer information decoder.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Assign the custom decoder to the Australia Post settings.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyDecoder();

            // Ensure the interpreting type matches the generation settings.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Iterate through all detected barcodes and output basic information.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                // The custom decoder processes the customer information internally; extended data can be accessed via result.Extended if needed.
            }
        }

        // Attempt to delete temporary files and folder; ignore any errors.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions.
        }
    }
}
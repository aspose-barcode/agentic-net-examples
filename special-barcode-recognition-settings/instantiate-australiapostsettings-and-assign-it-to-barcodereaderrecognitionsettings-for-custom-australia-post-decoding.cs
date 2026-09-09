// Title: Custom Australia Post Barcode Decoding with Aspose.BarCode
// Description: Demonstrates how to generate an Australia Post barcode, assign a custom customer information decoder, and read the barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and AustraliaPostSettings for configuring Australia Post specific options. Developers often need to customize decoding of Australia Post barcodes, such as interpreting customer information fields, which this sample illustrates.
// Prompt: Instantiate AustraliaPostSettings and assign it to BarCodeReader.RecognitionSettings for custom Australia Post decoding.
// Tags: australiapost, barcode, decoding, custom decoder, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Custom decoder for Australia Post customer information fields.
/// Implements a simple pass‑through decoding logic.
/// </summary>
class MyDecoder : AustraliaPostCustomerInformationDecoder
{
    /// <summary>
    /// Decodes the supplied customer information field.
    /// </summary>
    /// <param name="customerInformationField">Raw field data from the barcode.</param>
    /// <returns>Decoded string (unchanged in this example).</returns>
    public string Decode(string customerInformationField)
    {
        // Simple custom decoding: return the field unchanged
        return customerInformationField;
    }
}

/// <summary>
/// Entry point for the Australia Post barcode generation and custom decoding demo.
/// </summary>
class Program
{
    /// <summary>
    /// Generates an Australia Post barcode, configures a custom decoder, reads the barcode, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPost.png");

        // Generate an Australia Post barcode with specific dimensions and encoding table
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "620123456701234"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using a custom Australia Post decoder
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Configure Australia Post specific settings
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;
            reader.BarcodeSettings.AustraliaPost.CustomerInformationDecoder = new MyDecoder();

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the recognition results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }

        // Optional cleanup: delete the temporary directory and its contents
        // Directory.Delete(tempDir, true);
    }
}
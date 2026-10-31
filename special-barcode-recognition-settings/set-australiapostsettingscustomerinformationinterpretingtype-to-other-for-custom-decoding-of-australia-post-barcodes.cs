// Title: Custom decoding of Australia Post barcodes using CustomerInformationInterpretingType.Other
// Description: Demonstrates how to generate an Australia Post barcode and decode it with the CustomerInformationInterpretingType set to Other, enabling custom interpretation of customer information.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include handling Australia Post barcodes where the customer information segment requires custom parsing. Developers often work with EncodeTypes, DecodeType, and settings such as AustraliaPost.CustomerInformationInterpretingType to control encoding/decoding behavior.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to Other for custom decoding of Australia Post barcodes.
// Tags: barcode symbology, australia post, custom decoding, customerinformationinterpretingtype, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates an Australia Post barcode with a custom CustomerInformationInterpretingType
/// and then reads it back using the same custom setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary barcode image, decodes it,
    /// prints the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeAustraliaPostDemo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPostOther.png");

        // ------------------------------------------------------------
        // Generate an Australia Post barcode with CustomerInformationInterpretingType set to Other.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "6201234567"))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Apply the custom decoding interpretation type.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.Other;

            // Save the barcode image to the temporary path.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode and configure the reader to use the same custom interpretation.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            // Set the decoding format to match the generation setting.
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;

            // Iterate through all detected barcodes (only one in this case).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
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
            // Suppress any exceptions during cleanup to avoid breaking the example flow.
        }
    }
}
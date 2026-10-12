// Title: Custom Decoding of Australia Post Barcodes Using CustomerInformationInterpretingType.Other
// Description: Demonstrates how to generate an Australia Post barcode and decode it with the CustomerInformationInterpretingType set to Other, enabling custom interpretation of customer information.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and the AustraliaPostSettings.CustomerInformationInterpretingType property to customize encoding and decoding. Developers working with postal barcode standards often need to adjust interpretation settings for custom data formats, and this snippet illustrates the typical workflow.
// Prompt: Set AustraliaPostSettings.CustomerInformationInterpretingType to Other for custom decoding of Australia Post barcodes.
// Tags: barcode, australia post, custom decoding, customerinformationinterpretingtype, generation, recognition, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates an Australia Post barcode with a custom
/// CustomerInformationInterpretingType and then reads it back using the same setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it with custom settings,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary directory and file path for the barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeAustraliaPostDemo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "AustraliaPostOther.png");

        // --------------------------------------------------------------------
        // Generate an Australia Post barcode with CustomerInformationInterpretingType.Other
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "62012345670123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.Other;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using the same custom decoding type (Other)
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AustraliaPost))
        {
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;
            BarCodeResult[] results = reader.ReadBarCodes();

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

        // --------------------------------------------------------------------
        // Clean up temporary files and directory (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}
// Title: Automatic barcode size adjustment based on CodeText length
// Description: Demonstrates enabling auto‑size mode and dynamically setting image dimensions so the barcode adapts to the length of the supplied CodeText.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, AutoSizeMode, and image dimension parameters. Developers often need to create barcodes that automatically fit varying data lengths for labels, receipts, or inventory tags. The snippet shows typical usage of EncodeTypes, BarcodeGenerator.Parameters, and saving the output image.
// Prompt: Enable automatic size adjustment so dimensions adapt based on the length of CodeText.
// Tags: barcode, autosize, code128, image-dimensions, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode with automatic size adjustment based on the length of the provided CodeText.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures auto‑size mode, calculates dimensions, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample code text; in real scenarios this could come from any source.
        string codeText = "Sample12345";

        // Initialize a barcode generator for Code128 with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable automatic size adjustment using interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Calculate image dimensions based on the length of the code text.
            // Width grows with the number of characters; height remains constant.
            float width = 100f + codeText.Length * 10f; // points
            float height = 50f;                         // points

            // Apply the calculated dimensions to the generator.
            generator.Parameters.ImageWidth.Point = width;
            generator.Parameters.ImageHeight.Point = height;

            // Save the generated barcode image to a file.
            generator.Save("auto_sized_barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as 'auto_sized_barcode.png'.");
    }
}
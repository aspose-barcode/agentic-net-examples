// Title: Generate Code 16K barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a Code 16K barcode with the maximum 77‑character payload and exporting it to a 300 DPI TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code16K. It shows typical steps such as setting resolution, configuring symbology‑specific parameters, and saving the result in a high‑resolution raster format. Developers working with barcode creation, especially for Code 16K, can reference this pattern for custom payloads and image output requirements.
// Prompt: Generate Code 16K barcode with maximum 77 characters, save high‑resolution TIFF.
// Tags: code16k, barcode, generation, tiff, highresolution, aspose.barcode, encode-types, imageformat

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code 16K barcode with the maximum allowed
/// 77‑character text and saves it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the application’s working directory)
        const string outputPath = "code16k.tiff";

        // Sample payload that uses the full 77‑character limit for Code 16K
        string codeText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMN";

        // Validate length to avoid runtime errors from the barcode generator
        if (codeText.Length > 77)
        {
            throw new ArgumentException("Code text exceeds the maximum length of 77 characters.");
        }

        // Initialize the barcode generator with Code 16K symbology and the prepared text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300;

            // Configure optional Code 16K‑specific settings
            generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;          // Default aspect ratio
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;    // Default left quiet zone coefficient
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;    // Default right quiet zone coefficient

            // Disable filled bars to keep the visual style consistent
            generator.Parameters.Barcode.FilledBars = false;

            // Suppress exceptions for incorrect code text (not required for valid Code 16K data)
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode as a TIFF image with the specified resolution
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Code16K barcode saved to '{Path.GetFullPath(outputPath)}'.");
    }
}
// Title: Decode Swiss Post Parcel Domestic Barcode and Validate Identifier
// Description: Generates a Swiss Post Parcel domestic barcode, saves it as a PNG, then reads and decodes it to confirm the identifier matches the original value.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for Swiss Post Parcel barcodes. Uses BarcodeGenerator to create the image and BarCodeReader to decode it. Typical for logistics applications that need to produce and verify parcel identifiers. Developers often work with EncodeTypes, DecodeType, BarCodeImageFormat, and related parameter classes when handling barcode workflows.
// Prompt: Decode a Swiss Post Parcel domestic barcode from a PNG file and confirm identifier validity.
// Tags: swisspost, parcel, barcode, decode, validation, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Swiss Post Parcel domestic barcode, saves it as a PNG,
/// reads the barcode back, and validates that the decoded identifier matches the original.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, decoding, validation, and cleanup steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodeFile = Path.Combine(tempFolder, "Domestic.png");

        // Original Swiss Post Parcel Domestic identifier to encode.
        string originalIdentifier = "98.34.123456.12345678";

        // --------------------------------------------------------------------
        // Generate the barcode image using BarcodeGenerator.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, originalIdentifier))
        {
            // Set visual parameters: X-dimension and bar height in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.BarHeight.Pixels = 40;

            // Save the generated barcode as a PNG file.
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read and decode the barcode from the PNG file.
        // --------------------------------------------------------------------
        bool found = false;
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode data: {result.CodeText}");

                // Compare the decoded text with the original identifier.
                if (string.Equals(result.CodeText, originalIdentifier, StringComparison.Ordinal))
                {
                    Console.WriteLine("Identifier is valid.");
                }
                else
                {
                    Console.WriteLine("Identifier is invalid.");
                }
            }
        }

        // If no barcode was detected, inform the user.
        if (!found)
        {
            Console.WriteLine("No Swiss Post Parcel barcode detected in the image.");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodeFile);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome.
        }
    }
}
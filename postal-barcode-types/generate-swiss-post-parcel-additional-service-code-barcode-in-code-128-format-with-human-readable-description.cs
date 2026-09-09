// Title: Generate Swiss Post Parcel Additional Service Code Barcode (Code 128)
// Description: Creates a Code 128 barcode for a Swiss Post parcel additional service code and adds a human‑readable caption.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create a Code 128 barcode, customizing visual parameters such as X‑dimension, bar height, and caption settings, and then using BarCodeReader to decode and verify the barcode. Developers working with postal services, logistics, or any scenario requiring custom service codes will find this pattern useful for producing machine‑readable images with clear human‑readable annotations.
// Prompt: Generate a Swiss Post Parcel additional service code barcode in Code 128 format with human‑readable description.
// Tags: barcode symbology, generation, recognition, code128, swisspost, caption, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss Post parcel additional service barcode (Code 128) with a human‑readable caption and verifying it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and reads it back to confirm the encoded data.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissPostAdditionalService.png");

        // Service code (e.g., "0327" for Return receipt) and its human‑readable description.
        string serviceCode = "0327"; // Return receipt (AR)
        string description = "AR";

        // Create a barcode generator for Code128 with the service code as data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, serviceCode))
        {
            // Set visual dimensions: X‑dimension and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Hide the default code text (the raw data) to avoid duplication.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure a caption above the barcode to display the human‑readable description.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionAbove.Text = description;
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            generator.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

            // Save the generated barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Verify the saved barcode by reading it back if the file exists.
        if (File.Exists(outputPath))
        {
            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(outputPath, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected type: {result.CodeTypeName}, Data: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to create barcode image.");
        }
    }
}
// Title: Generate Swiss Post Parcel Additional Service Barcode (Code 128) with Caption
// Description: Creates a Swiss Post Parcel additional service barcode in Code 128 format, adds a human‑readable caption, saves it as PNG, and demonstrates decoding the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating Code 128 barcodes, configuring visual parameters such as X‑dimension, bar height, and captions, and uses BarCodeReader to decode Swiss Post Parcel barcodes. Developers often need to generate postal service barcodes with custom text and verify them programmatically.
// Prompt: Generate a Swiss Post Parcel additional service code barcode in Code 128 format with human‑readable description.
// Tags: barcode, symbology, generation, recognition, code128, swisspost, png, caption, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Swiss Post Parcel additional service barcode (Code 128) with a caption,
/// save it as an image, and then read it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and decodes it.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary output directory for the generated image
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissPostAdditionalService_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "AdditionalService.png");

        // ------------------------------------------------------------
        // Generate Swiss Post Parcel Additional Service barcode (Code 128) with a human‑readable caption
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "0327"))
        {
            // Set basic barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the caption displayed above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionAbove.Text = "AR";
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            generator.Parameters.CaptionAbove.Font.Style = Aspose.Drawing.FontStyle.Bold;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // ------------------------------------------------------------
        // Read and decode the generated barcode to verify its content
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "0327"))
        {
            // Reapply the same visual settings used during generation
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionAbove.Text = "AR";
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            generator.Parameters.CaptionAbove.Font.Style = Aspose.Drawing.FontStyle.Bold;

            // Generate the barcode image in memory
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Initialize a reader for Swiss Post Parcel barcodes
                using (var reader = new BarCodeReader(image, DecodeType.SwissPostParcel))
                {
                    // Iterate through all detected barcodes and output their type and data
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected type: {result.CodeTypeName}, Data: {result.CodeText}");
                    }
                }
            }
        }
    }
}
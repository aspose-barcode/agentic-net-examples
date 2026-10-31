// Title: Generate Swiss Post Parcel Additional Service Barcode with Metadata
// Description: Demonstrates creating a Swiss Post Parcel barcode that encodes an additional service code and attaches a human‑readable service description as a caption.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel to produce parcel service barcodes. Typical use cases include logistics and shipping applications where additional service codes (e.g., insurance, cash on delivery) must be encoded and displayed alongside the barcode. Developers often need to customize dimensions, hide the code text, and add caption metadata, which this sample shows.
// Prompt: Generate a Swiss Post Parcel additional service code barcode and attach the service description as metadata.
// Tags: swisspost, parcel, additionalservice, barcode, generation, png, caption, metadata

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode for an additional service
/// and adds the service description as a caption metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissPostAdditionalService");
        Directory.CreateDirectory(outputDir);

        // Service code to encode and its human‑readable description
        string serviceCode = "0327";
        string description = "AR";

        // Full file path for the generated PNG image
        string filePath = Path.Combine(outputDir, "AdditionalService.png");

        // Create a barcode generator for Swiss Post Parcel with the specified service code
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, serviceCode))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;               // Width of a single module
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;              // Height of the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide the encoded text

            // Configure the caption that will hold the service description
            generator.Parameters.CaptionAbove.Visible = true;                 // Show caption above the barcode
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left; // Align caption to the left
            generator.Parameters.CaptionAbove.Text = description;            // Set caption text
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;        // Font size for the caption

            // Save the barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}
// Title: Generate Swiss Post Parcel Additional Service Code128 Barcode with Caption
// Description: Creates a Code 128 barcode representing a Swiss Post parcel additional service code and adds a human‑readable caption above it. The resulting image is saved as a PNG file.
// Category-Description: This example demonstrates Aspose.BarCode barcode generation for shipping and logistics scenarios. It uses the BarcodeGenerator class with EncodeTypes.Code128, configures barcode dimensions, hides the default code text, and adds a custom caption. Typical use cases include creating parcel labels, service codes, and other logistics barcodes where a readable description is required alongside the machine‑readable symbol.
// Prompt: Generate a Swiss Post Parcel additional service code barcode in Code 128 format with human‑readable description.
// Tags: swisspost, parcel, additional-service, code128, barcode, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Swiss Post parcel additional service barcode (Code 128) with a caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Define the service code and its human‑readable description.
        string serviceCode = "0327"; // Return receipt (AR)
        string description = "AR";

        // Prepare the output folder in the system temporary directory.
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostAdditionalService");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "AdditionalService_Code128.png");

        // Create a BarcodeGenerator for Code 128 using the service code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, serviceCode))
        {
            // Set basic barcode appearance: module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Hide the default code text that would appear below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure a caption above the barcode to show the human‑readable description.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionAbove.Text = description;
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            generator.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

            // Save the generated barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
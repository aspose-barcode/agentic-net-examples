// Title: Generate Swiss Post Parcel Additional Service Barcode with Caption
// Description: Demonstrates how to create a Swiss Post Parcel barcode that encodes an additional service code and attaches a human‑readable service description as a caption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.SwissPostParcel, configure visual parameters such as X‑dimension, bar height, hide the code text, and add a caption. Typical use cases include generating shipping labels for Swiss Post parcels where additional service codes (e.g., return receipt) must be encoded and displayed. Developers often need to customize barcode appearance and attach metadata for downstream processing.
// Prompt: Generate a Swiss Post Parcel additional service code barcode and attach the service description as metadata.
// Tags: swisspost, parcel, additionalservice, barcode generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss Post Parcel barcode with an additional service code
/// and adds a human‑readable service description as a caption. The barcode is saved as a PNG
/// and then read back to verify the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, generates the barcode,
    /// saves it, and reads it back for verification.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Service code and description (metadata)
        string serviceCode = "0327"; // Return receipt (AR)
        string serviceDescription = "AR";

        // Generate Swiss Post Parcel Additional Service barcode
        string barcodePath = Path.Combine(outputDir, "SwissPostAdditionalService.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, serviceCode))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;      // Width of a single module
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;    // Height of the bars

            // Hide the encoded text (we will show description above)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure caption above the barcode to display the service description
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionAbove.Text = serviceDescription;
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            generator.Parameters.CaptionAbove.Font.Style = Aspose.Drawing.FontStyle.Bold;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Read back the barcode to verify the encoded data
        using (var reader = new BarCodeReader(barcodePath, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
            }
        }
    }
}
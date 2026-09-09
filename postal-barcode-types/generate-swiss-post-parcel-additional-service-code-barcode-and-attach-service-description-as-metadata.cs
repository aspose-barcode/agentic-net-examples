// Title: Generate Swiss Post Parcel Additional Service Barcode with Metadata
// Description: Demonstrates how to create a Swiss Post Parcel additional service code barcode and embed the service description as a caption.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It shows setting barcode dimensions, hiding the code text, adding a custom caption, saving to PNG, and verifying the barcode using BarCodeReader. Developers working with postal barcode standards often need to generate service-specific barcodes and attach human‑readable metadata for printing and scanning workflows.
// Prompt: Generate a Swiss Post Parcel additional service code barcode and attach the service description as metadata.
// Tags: swisspostparcel, barcode generation, metadata caption, png output, aspose.barcode, encode types, barcode verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Swiss Post Parcel additional service barcode,
/// adds a service description as a caption, saves the image, and verifies the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and reads it back for verification.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the barcode image will be saved.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Define the additional service code and its human‑readable description.
        string serviceCode = "0327";
        string serviceDescription = "AR";

        // Create a BarcodeGenerator for the Swiss Post Parcel symbology using the service code.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, serviceCode))
        {
            // Configure visual appearance of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;               // Width of a single barcode module.
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;              // Height of the barcode bars.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide the encoded text.

            // Add a caption above the barcode to display the service description.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
            generator.Parameters.CaptionAbove.Text = serviceDescription;
            generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
            generator.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

            // Save the generated barcode as a PNG file.
            string imagePath = Path.Combine(outputDir, "SwissPostAdditionalService.png");
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {imagePath}");

            // Verify the barcode by reading it back from the generated image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                using (var reader = new BarCodeReader(bitmap, DecodeType.SwissPostParcel))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Read barcode type: {result.CodeTypeName}, Data: {result.CodeText}");
                    }
                }
            }
        }
    }
}
// Title: Generate Swiss Post Parcel Additional Service Code Barcode (Code 128)
// Description: Creates a Code 128 barcode for a Swiss Post parcel additional service code and adds a human‑readable caption.
// Category-Description: This example demonstrates Aspose.BarCode generation of a Code 128 barcode with a caption, a common task when encoding Swiss Post parcel additional service codes. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and caption configuration properties. Developers often need to produce printable barcodes with readable text for logistics and shipping applications.
// Prompt: Generate a Swiss Post Parcel additional service code barcode in Code 128 format with human‑readable description.
// Tags: barcode, code128, swisspost, additional service, caption, image, aspose.barcode, generation

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Swiss Post parcel additional service code barcode
/// in Code 128 format with a human‑readable caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel additional service code (12 digits)
        const string parcelCode = "123456789012";

        // Human‑readable description that will appear below the barcode
        const string description = "Additional Service";

        // Initialise the barcode generator for Code128 with the parcel code as data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, parcelCode))
        {
            // Set the X dimension (module width) to improve visual clarity
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Enable and configure the caption displayed below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";
            generator.Parameters.CaptionBelow.Font.Size.Point = 10f;
            generator.Parameters.CaptionBelow.TextColor = Color.Black;
            generator.Parameters.CaptionBelow.Text = description;

            // Define the output file path and save the generated barcode image
            const string outputPath = "SwissPostParcel.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode with a caption and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel additional service code (replace with actual code as needed)
        string codeText = "1234567890123";

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcel.png");

        // Create a barcode generator for Code128 using the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure the caption that appears above the barcode
            generator.Parameters.CaptionAbove.Visible = true;                     // Make the caption visible
            generator.Parameters.CaptionAbove.Text = "Swiss Post Parcel Service"; // Set caption text
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center; // Center-align the caption
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";          // Use Arial font
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;             // Set font size to 12 points

            // Ensure the encoded text is displayed below the barcode (default behavior)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image as a PNG file at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
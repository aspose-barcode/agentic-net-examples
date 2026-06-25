using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Swiss Post Parcel barcode with a caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel barcode data (example code text)
        string codeText = "1234567890123";

        // Service description to be attached as a caption (metadata)
        string serviceDescription = "Additional Service: Express Delivery";

        // Initialize the barcode generator for Swiss Post Parcel symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure the caption that appears above the barcode
            generator.Parameters.CaptionAbove.Text = serviceDescription;          // Set caption text
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;   // Center align the caption
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";          // Use Arial font
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;              // Font size 10 points
            generator.Parameters.CaptionAbove.Visible = true;                     // Make caption visible

            // Optional: set barcode and background colors
            generator.Parameters.Barcode.BarColor = Color.Black; // Barcode bars in black
            generator.Parameters.BackColor = Color.White;       // Background in white

            // Define output file path and save the barcode as a PNG image
            string outputPath = "SwissPostParcel.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}
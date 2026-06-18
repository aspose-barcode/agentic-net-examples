using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a UPC-A barcode with a caption and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a UPC-A barcode, adds a caption, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for UPC-A format with a 12‑digit sample code.
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
        {
            // Enable and set the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample UPC-A";

            // Set the caption text color to purple using the named color.
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Render the barcode and save it as a PNG file named "upc.png".
            generator.Save("upc.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Barcode generated and saved as upc.png");
    }
}
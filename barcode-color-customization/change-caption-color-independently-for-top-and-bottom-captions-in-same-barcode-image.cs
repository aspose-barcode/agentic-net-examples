// Title: Independent Top and Bottom Caption Colors in a Barcode Image
// Description: Demonstrates how to set different colors for the top and bottom captions of a Code128 barcode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, Caption parameters, and Aspose.Drawing to customize barcode appearance. Developers often need to tailor caption text and styling for branding or informational purposes when creating barcode images for print or digital media.
// Prompt: Change the caption color independently for top and bottom captions in the same barcode image.
// Tags: code128, caption color, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with distinct colors for the top and bottom captions,
/// then saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Configure the top caption text and set its color to blue.
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;

            // Configure the bottom caption text and set its color to green.
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Green;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Create a file stream to write the PNG file.
                using (FileStream fileStream = new FileStream("barcode.png", FileMode.Create, FileAccess.Write))
                {
                    // Save the bitmap to the stream in PNG format.
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }

        // Inform the user that the image has been saved.
        Console.WriteLine("Barcode image with independent caption colors saved as 'barcode.png'.");
    }
}
// Title: Generate a MaxiCode barcode with custom foreground and background colors
// Description: Demonstrates how to set the bar (foreground) and background colors of a MaxiCode complex barcode using Aspose.BarCode before rendering it to an image file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the ComplexBarcodeGenerator class and its Parameters property to customize visual aspects such as bar color and background color. Typical use cases include branding, visual integration, and accessibility where barcode colors need to match design guidelines. Developers often need to adjust these properties when generating PNG, JPEG, or other image formats for web or print.
// Prompt: Customize barcode foreground and background colors via ComplexBarcodeGenerator properties before generating the image.
// Tags: maxicode, complex barcode, color customization, image generation, aspose.barcode, c#

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a MaxiCode complex barcode with custom colors and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares the MaxiCode codetext, sets colors, generates the image, and saves it.
    /// </summary>
    static void Main()
    {
        // Prepare a MaxiCode codetext (Mode 2) with a standard second message
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Create and assign the second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Initialize the ComplexBarcodeGenerator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to yellow
            generator.Parameters.BackColor = Color.Yellow;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a PNG file
                bitmap.Save("maxicode.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("Complex barcode generated with custom colors: maxicode.png");
    }
}
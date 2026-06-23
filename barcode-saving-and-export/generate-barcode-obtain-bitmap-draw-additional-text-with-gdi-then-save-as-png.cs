using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, adding custom text, and saving as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, draws extra text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample data "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Create the barcode image as a Bitmap
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Obtain a Graphics object to draw on the bitmap
                using (var graphics = Graphics.FromImage(barcodeImage))
                {
                    // Text that will be drawn onto the barcode image
                    string extraText = "Sample Text";

                    // Set up the font (Arial, 12pt) and brush (red) for drawing the text
                    using (var font = new Font("Arial", 12f))
                    using (var brush = new SolidBrush(Color.Red))
                    {
                        // Calculate position near the bottom‑left corner of the image
                        var position = new PointF(10f, barcodeImage.Height - 30f);

                        // Render the text onto the image
                        graphics.DrawString(extraText, font, brush, position);
                    }
                }

                // Save the modified bitmap as a PNG file
                barcodeImage.Save("barcode_with_text.png", ImageFormat.Png);
            }
        }

        // Inform the user that the file has been saved
        Console.WriteLine("Barcode image saved as 'barcode_with_text.png'.");
    }
}
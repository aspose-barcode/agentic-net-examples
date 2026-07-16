// Title: Generate MaxiCode Mode 5 barcode with embedded logo
// Description: Demonstrates creating a MaxiCode barcode in mode five and placing a custom company logo at its center.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeStandardCodetext, and image manipulation via Aspose.Drawing to embed graphics into a barcode. Developers often need to combine barcodes with branding elements for packaging or shipping labels, and this snippet provides a clear pattern for doing so.
// Prompt: Generate a MaxiCode barcode with mode five and embed a custom company logo at the center.
// Tags: maxicode, barcode generation, image embedding, complexbarcode, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode barcode (mode 5) and embeds a custom logo at its center.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, draws a placeholder logo, embeds it, and saves the result.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext for mode 5 with a sample message.
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode5,
            Message = "Sample MaxiCode Mode5"
        };

        // Use ComplexBarcodeGenerator to create the MaxiCode image.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var barcodeImage = complexGenerator.GenerateBarCodeImage())
            {
                // Create a simple placeholder logo (100x100) with the word "Logo".
                using (var logo = new Bitmap(100, 100))
                {
                    using (var logoGraphics = Graphics.FromImage(logo))
                    {
                        // Fill the logo background with white.
                        logoGraphics.Clear(Color.White);

                        // Draw the text "Logo" centered in the placeholder.
                        using (var font = new Font("Arial", 20))
                        {
                            const string text = "Logo";
                            var textSize = logoGraphics.MeasureString(text, font);
                            var textRect = new RectangleF(
                                (logo.Width - textSize.Width) / 2,
                                (logo.Height - textSize.Height) / 2,
                                textSize.Width,
                                textSize.Height);
                            logoGraphics.DrawString(text, font, new SolidBrush(Color.Black), textRect);
                        }
                    }

                    // Determine the coordinates to place the logo at the center of the barcode.
                    int posX = (barcodeImage.Width - logo.Width) / 2;
                    int posY = (barcodeImage.Height - logo.Height) / 2;

                    // Draw the logo onto the barcode image.
                    using (var barcodeGraphics = Graphics.FromImage(barcodeImage))
                    {
                        barcodeGraphics.DrawImage(logo, posX, posY, logo.Width, logo.Height);
                    }
                }

                // Save the final image with the embedded logo.
                barcodeImage.Save("MaxiCodeMode5_WithLogo.png", ImageFormat.Png);
                Console.WriteLine("MaxiCode barcode with embedded logo saved as 'MaxiCodeMode5_WithLogo.png'.");
            }
        }
    }
}
// Title: Generate a Code128 barcode with a two‑color gradient background
// Description: Demonstrates how to create a Code128 barcode and overlay it on a simple two‑color gradient background, then save as PNG. Useful for adding visual appeal to barcodes in reports or UI.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class and Aspose.Drawing graphics. Developers often need to modify background colors, apply gradients, or combine barcode images with custom graphics for branding or design purposes. The snippet shows typical steps: generate barcode, create a bitmap, draw custom background, and render the barcode on top.
// Prompt: Apply a gradient background using two colors to create a visually appealing barcode.
// Tags: code128, gradient background, barcode generation, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a two‑color gradient background and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, draws a simple gradient background, composites the barcode, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gradient_barcode.png");

        // Initialize the barcode generator for Code128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the barcode bars to black
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // Make the generator's background transparent; we'll draw our own gradient later
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Generate the barcode image as a bitmap
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Create a new bitmap that will hold the gradient background and the barcode
                using (Bitmap finalBmp = new Bitmap(barcodeBmp.Width, barcodeBmp.Height, PixelFormat.Format32bppArgb))
                {
                    // Obtain a graphics object for drawing onto the final bitmap
                    using (Graphics g = Graphics.FromImage(finalBmp))
                    {
                        // Draw the top half of the gradient (light blue)
                        using (SolidBrush topBrush = new SolidBrush(Aspose.Drawing.Color.LightBlue))
                        {
                            Rectangle topRect = new Rectangle(0, 0, finalBmp.Width, finalBmp.Height / 2);
                            g.FillRectangle(topBrush, topRect);
                        }

                        // Draw the bottom half of the gradient (light green)
                        using (SolidBrush bottomBrush = new SolidBrush(Aspose.Drawing.Color.LightGreen))
                        {
                            Rectangle bottomRect = new Rectangle(0, finalBmp.Height / 2, finalBmp.Width, finalBmp.Height - finalBmp.Height / 2);
                            g.FillRectangle(bottomBrush, bottomRect);
                        }

                        // Overlay the generated barcode on top of the gradient background
                        g.DrawImage(barcodeBmp, 0, 0);
                    }

                    // Save the composed image as PNG
                    finalBmp.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode with gradient background saved to: {outputPath}");
    }
}
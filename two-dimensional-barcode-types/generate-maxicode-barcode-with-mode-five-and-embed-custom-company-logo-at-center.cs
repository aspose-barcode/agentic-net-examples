// Title: Generate MaxiCode barcode (mode 5) with embedded logo
// Description: Demonstrates creating a MaxiCode barcode in mode five and overlaying a custom company logo at its center, then saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology and image compositing. It shows how to configure MaxiCode mode, customize visual properties, generate the barcode image, and combine it with a logo using Aspose.Drawing. Developers working with shipping labels, logistics, or product packaging often need to embed branding within MaxiCode barcodes, making this pattern a common requirement.
// Prompt: Generate a MaxiCode barcode with mode five and embed a custom company logo at the center.
// Tags: maxicode, barcode generation, logo overlay, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode (mode 5) with an embedded logo.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode with logo and writes the output path.
    /// </summary>
    static void Main()
    {
        try
        {
            // Define the output file path in the current directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode5_WithLogo.png");

            // Create the barcode image with the logo and save it
            GenerateMaxiCodeWithLogo(outputPath);

            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode in mode 5, overlays a custom logo at its center, and saves the image.
    /// </summary>
    /// <param name="outputFilePath">Full path where the final PNG image will be saved.</param>
    static void GenerateMaxiCodeWithLogo(string outputFilePath)
    {
        // Create a simple logo bitmap programmatically (100x100 pixels)
        int logoWidth = 100;
        int logoHeight = 100;
        using (Bitmap logoBmp = new Bitmap(logoWidth, logoHeight, PixelFormat.Format32bppArgb))
        {
            // Draw the logo graphics
            using (Graphics gLogo = Graphics.FromImage(logoBmp))
            {
                // Transparent background
                gLogo.Clear(Color.Transparent);

                // Draw a blue ellipse as the logo background
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 0, 120, 215)))
                {
                    gLogo.FillEllipse(brush, 0, 0, logoWidth, logoHeight);
                }

                // Draw the word "Logo" centered inside the ellipse
                using (Font font = new Font("Arial", 20f, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    gLogo.DrawString("Logo", font, textBrush, new RectangleF(0, 0, logoWidth, logoHeight), sf);
                }
            }

            // Initialize the MaxiCode barcode generator with sample data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "HelloWorld"))
            {
                // Set MaxiCode mode to 5 (used for specific data structures)
                generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;

                // Optional visual customizations
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Generate the barcode image
                using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                {
                    // Compute coordinates to center the logo on the barcode
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Overlay the logo onto the barcode image
                    using (Graphics gBarcode = Graphics.FromImage(barcodeBmp))
                    {
                        gBarcode.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image as PNG
                    using (FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        barcodeBmp.Save(fs, ImageFormat.Png);
                    }
                }
            }
        }
    }
}
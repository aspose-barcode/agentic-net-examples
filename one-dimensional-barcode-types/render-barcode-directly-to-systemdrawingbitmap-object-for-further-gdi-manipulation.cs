// Title: Render Barcode to Bitmap for GDI+ Manipulation
// Description: Demonstrates generating a Code128 barcode and rendering it directly to a System.Drawing.Bitmap for further GDI+ operations.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, BarcodeParameters, and Aspose.Drawing classes to create, customize, and manipulate barcode images. Typical use cases include on‑the‑fly image processing, overlaying graphics, or integrating barcodes into custom UI components. Developers often need to render barcodes to Bitmap objects for GDI+ drawing, resizing, or compositing with other graphics.
// Prompt: Render barcode directly to a System.Drawing.Bitmap object for further GDI+ manipulation.
// Tags: code128, barcode generation, bitmap, gdi+, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, renders it to a Bitmap,
/// applies GDI+ drawing operations, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates and manipulates a barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: customize barcode appearance (color and font).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Generate the barcode as an Aspose.Drawing.Bitmap.
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Perform GDI+ manipulation: draw a red rectangle around the barcode.
                using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                {
                    using (Pen redPen = new Pen(Aspose.Drawing.Color.Red, 2f))
                    {
                        // Draw rectangle covering the entire image.
                        graphics.DrawRectangle(redPen, 0, 0, barcodeBitmap.Width - 1, barcodeBitmap.Height - 1);
                    }
                }

                // Save the manipulated bitmap to a PNG file.
                barcodeBitmap.Save("barcode.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("Barcode image generated and saved as 'barcode.png'.");
    }
}
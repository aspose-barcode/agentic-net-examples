// Title: Render Barcode to System.Drawing.Bitmap and Apply GDI+ Manipulation
// Description: Demonstrates generating a Code128 barcode, rendering it directly to an Aspose.Drawing.Bitmap, drawing a red border using GDI+, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation and rendering category, illustrating how to use BarcodeGenerator to create barcodes, obtain a Bitmap for further GDI+ processing, and save the image. Typical use cases include custom graphics overlays, watermarking, or integrating barcodes into existing .NET drawing workflows. Developers often work with BarcodeGenerator, Bitmap, Graphics, Pen, and ImageFormat classes to achieve these tasks.
// Prompt: Render barcode directly to a System.Drawing.Bitmap object for further GDI+ manipulation.
// Tags: barcode, code128, generation, bitmap, gdi+, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the barcode rendering example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode, draws a red rectangle around it using GDI+, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final PNG image
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Generate the barcode as an Aspose.Drawing.Bitmap for direct GDI+ manipulation
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Perform GDI+ drawing: add a red border around the entire barcode image
                using (Graphics graphics = Graphics.FromImage(barcodeBitmap))
                {
                    // Create a red pen with a thickness of 3 pixels
                    using (Pen redPen = new Pen(Color.Red, 3f))
                    {
                        // Draw the rectangle; subtract 1 to stay within image bounds
                        graphics.DrawRectangle(redPen, 0, 0, barcodeBitmap.Width - 1, barcodeBitmap.Height - 1);
                    }
                }

                // Save the manipulated bitmap to a PNG file using a FileStream
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeBitmap.Save(fileStream, ImageFormat.Png);
                }

                // Inform the user where the file was saved
                Console.WriteLine($"Barcode image saved to '{Path.GetFullPath(outputPath)}'.");
            }
        }
    }
}
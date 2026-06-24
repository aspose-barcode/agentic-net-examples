using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A GS1 DataBar coupon barcode,
/// compositing it onto a simple product label, and saving the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, draws a product label, merges them, and writes the output image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Sample UPC‑A with DataBar coupon code text
        string couponCode = "514141100906(8110)106141416543213500110000310123196000";

        // Generate barcode image into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for the specified encode type and data
            using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, couponCode))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for subsequent reading
            barcodeStream.Position = 0;

            // Load the generated barcode image from the stream into a bitmap
            using (var barcodeBitmap = new Bitmap(barcodeStream))
            {
                // Define dimensions for the placeholder product label
                int labelWidth = 600;
                int labelHeight = 400;

                // Create a blank label bitmap with a 32‑bit ARGB pixel format
                using (var labelBitmap = new Bitmap(labelWidth, labelHeight, PixelFormat.Format32bppArgb))
                {
                    // Obtain a graphics object for drawing on the label bitmap
                    using (var graphics = Graphics.FromImage(labelBitmap))
                    {
                        // Fill the label background with white color
                        graphics.Clear(Color.White);

                        // Draw the product name near the top-left corner
                        graphics.DrawString(
                            "Sample Product",
                            new Font("Arial", 24f),
                            new SolidBrush(Color.Black),
                            new PointF(20f, 20f));

                        // Draw a short product description below the name
                        graphics.DrawString(
                            "Description: High‑quality item",
                            new Font("Arial", 14f),
                            new SolidBrush(Color.DarkGray),
                            new PointF(20f, 60f));

                        // Calculate position to center the barcode at the bottom of the label
                        int barcodeX = (labelWidth - barcodeBitmap.Width) / 2;
                        int barcodeY = labelHeight - barcodeBitmap.Height - 20; // 20 px margin from bottom

                        // Render the barcode onto the label bitmap
                        graphics.DrawImage(barcodeBitmap, barcodeX, barcodeY, barcodeBitmap.Width, barcodeBitmap.Height);
                    }

                    // Determine output file path in the system's temporary directory
                    string outputPath = Path.Combine(Path.GetTempPath(), "ProductLabelWithBarcode.png");

                    // Save the combined label and barcode image as PNG
                    labelBitmap.Save(outputPath, ImageFormat.Png);

                    // Inform the user where the file was saved
                    Console.WriteLine($"Merged image saved to: {outputPath}");
                }
            }
        }
    }
}
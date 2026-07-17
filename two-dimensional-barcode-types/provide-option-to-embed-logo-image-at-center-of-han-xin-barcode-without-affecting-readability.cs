// Title: Embed Logo into Han Xin Barcode using Aspose.BarCode
// Description: Demonstrates how to generate a Han Xin barcode and embed a custom logo at its center while preserving readability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It showcases the use of BarcodeGenerator, EncodeTypes, and Aspose.Drawing classes to create a barcode, adjust error correction, and overlay graphics. Developers often need to combine barcodes with branding elements such as logos, requiring careful placement to maintain scanability.
// Prompt: Provide option to embed logo image at center of Han Xin barcode without affecting readability.
// Tags: hanxin, barcode, logo, embedding, png, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Han Xin barcode, creates a simple logo, and embeds the logo at the center of the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, overlays the logo, and saves the final image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate barcode image and the final image with logo.
        string barcodePath = "hanxin_barcode.png";
        string finalPath = "hanxin_with_logo.png";

        // Create a simple logo image (red square with white "LOGO" text).
        using (var logoBitmap = new Bitmap(100, 100))
        {
            using (var g = Graphics.FromImage(logoBitmap))
            {
                // Fill background with red.
                g.Clear(Color.Red);
                // Draw white "LOGO" text centered in the bitmap.
                using (var font = new Font("Arial", 20f, FontStyle.Bold))
                {
                    var textSize = g.MeasureString("LOGO", font);
                    var textPos = new PointF((logoBitmap.Width - textSize.Width) / 2f,
                                             (logoBitmap.Height - textSize.Height) / 2f);
                    g.DrawString("LOGO", font, new SolidBrush(Color.White), textPos);
                }
            }

            // Generate Han Xin barcode and store it in a memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "Sample Han Xin Code"))
            {
                // Set error correction level to improve readability after logo overlay.
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
                // Use interpolation auto-size mode for better scaling.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                using (var barcodeStream = new MemoryStream())
                {
                    // Save barcode image as PNG into the stream.
                    generator.Save(barcodeStream, BarCodeImageFormat.Png);
                    barcodeStream.Position = 0; // Reset stream position for reading.

                    // Load the barcode image using Aspose.Drawing.
                    using (var barcodeImage = Image.FromStream(barcodeStream))
                    {
                        // Determine center coordinates for the logo placement.
                        int logoWidth = logoBitmap.Width;
                        int logoHeight = logoBitmap.Height;
                        int barcodeWidth = barcodeImage.Width;
                        int barcodeHeight = barcodeImage.Height;

                        int x = (barcodeWidth - logoWidth) / 2;
                        int y = (barcodeHeight - logoHeight) / 2;

                        // Draw the logo onto the barcode at the calculated position.
                        using (var graphics = Graphics.FromImage(barcodeImage))
                        {
                            graphics.DrawImage(logoBitmap, new Rectangle(x, y, logoWidth, logoHeight));
                        }

                        // Save the final image with the embedded logo to disk.
                        barcodeImage.Save(finalPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Output the full path of the saved image for verification.
        Console.WriteLine($"Barcode with embedded logo saved to: {Path.GetFullPath(finalPath)}");
    }
}
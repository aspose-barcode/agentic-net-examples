using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Demonstrates generating a Han Xin barcode, creating a simple logo,
/// and overlaying the logo onto the barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, creates a red circular logo, merges them,
    /// and saves the final image to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for the barcode and the final image with logo.
        string barcodePath = Path.Combine(Path.GetTempPath(), "hanxin_barcode.png");
        string finalPath   = Path.Combine(Path.GetTempPath(), "hanxin_barcode_with_logo.png");
        string codeText    = "HanXin Sample Text";

        // Generate the Han Xin barcode and save it as a PNG file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set error correction level to L2.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            // Use interpolation for auto-sizing to improve image quality.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            // Save the generated barcode image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a simple red circular logo of size 80x80 pixels.
        using (Bitmap logo = new Bitmap(80, 80))
        {
            using (Graphics gLogo = Graphics.FromImage(logo))
            {
                // Ensure the background is transparent.
                gLogo.Clear(Color.Transparent);
                // Draw a solid red ellipse (circle) filling the bitmap.
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    gLogo.FillEllipse(brush, 0, 0, 80, 80);
                }
            }

            // Load the previously saved barcode image.
            using (Bitmap barcodeImage = new Bitmap(barcodePath))
            {
                using (Graphics graphics = Graphics.FromImage(barcodeImage))
                {
                    // Calculate coordinates to center the logo on the barcode.
                    int x = (barcodeImage.Width  - logo.Width)  / 2;
                    int y = (barcodeImage.Height - logo.Height) / 2;
                    // Use high-quality bicubic interpolation for smoother rendering.
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    // Draw the logo onto the barcode image at the calculated position.
                    graphics.DrawImage(logo, x, y, logo.Width, logo.Height);
                }

                // Save the combined image (barcode with logo) as a PNG file.
                barcodeImage.Save(finalPath, ImageFormat.Png);
            }
        }

        // Output the location of the final image.
        Console.WriteLine($"Barcode with logo saved to: {finalPath}");
    }
}
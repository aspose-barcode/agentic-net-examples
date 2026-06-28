using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with an optional embedded logo using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode, optionally embeds a logo, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file paths for the output barcode image and the optional logo image.
        string outputPath = "maxicode_mode5_with_logo.png";
        string logoPath = "logo.png";

        // Create a MaxiCode standard codetext object and configure it for Mode5.
        var maxiCode = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode5,
            Message = "Sample MaxiCode with logo"
        };

        // Generate the barcode image and store it in a memory stream.
        using (var barcodeStream = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to create the barcode based on the MaxiCode settings.
            using (var complexGenerator = new ComplexBarcodeGenerator(maxiCode))
            {
                // Save the generated barcode as PNG into the memory stream.
                complexGenerator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for subsequent reading.
            barcodeStream.Position = 0;

            // Load the barcode image from the stream into a Bitmap object.
            using (var barcodeBitmap = new Bitmap(barcodeStream))
            {
                // Check if the logo file exists before attempting to embed it.
                if (File.Exists(logoPath))
                {
                    // Load the logo image from file.
                    using (var logoImage = (Bitmap)Image.FromFile(logoPath))
                    {
                        // Calculate logo width as 20% of the barcode width.
                        int logoWidth = (int)(barcodeBitmap.Width * 0.2f);
                        // Preserve the logo's aspect ratio when calculating height.
                        int logoHeight = (int)((float)logoImage.Height / logoImage.Width * logoWidth);

                        // Resize the logo to the calculated dimensions.
                        using (var resizedLogo = new Bitmap(logoImage, new Size(logoWidth, logoHeight)))
                        {
                            // Create a graphics object to draw onto the barcode bitmap.
                            using (var graphics = Graphics.FromImage(barcodeBitmap))
                            {
                                // Determine the top-left coordinates to center the logo.
                                int x = (barcodeBitmap.Width - logoWidth) / 2;
                                int y = (barcodeBitmap.Height - logoHeight) / 2;

                                // Draw the resized logo onto the barcode bitmap.
                                graphics.DrawImage(resizedLogo, x, y, logoWidth, logoHeight);
                            }
                        }
                    }
                }
                else
                {
                    // Inform the user that the logo file was not found; continue without embedding a logo.
                    Console.WriteLine($"Logo file not found at '{logoPath}'. Barcode will be saved without a logo.");
                }

                // Save the final barcode image (with or without logo) to the specified output file.
                barcodeBitmap.Save(outputPath, ImageFormat.Png);
                Console.WriteLine($"MaxiCode barcode saved to '{outputPath}'.");
            }
        }
    }
}
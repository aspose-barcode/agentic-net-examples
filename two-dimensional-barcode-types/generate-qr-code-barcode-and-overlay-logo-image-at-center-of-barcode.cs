using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with an embedded logo using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, embeds a logo, and saves the result.
    /// </summary>
    static void Main()
    {
        // Paths for the logo image and the output QR code image
        string logoPath = "logo.png";
        string outputPath = "qr_with_logo.png";

        // Text to encode in the QR code
        string codeText = "https://example.com";

        // Verify that the logo file exists before proceeding
        if (!File.Exists(logoPath))
        {
            Console.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        // Create a QR code generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set a high error correction level to allow for logo overlay
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optional: set image resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Generate the QR code image
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Load the logo image from file
                using (var logoImage = new Bitmap(logoPath))
                {
                    // Calculate maximum logo dimensions (20% of QR code size)
                    int logoMaxWidth = (int)(barcodeImage.Width * 0.2f);
                    int logoMaxHeight = (int)(barcodeImage.Height * 0.2f);

                    // Compute scaling ratio to preserve logo aspect ratio
                    double ratio = Math.Min((double)logoMaxWidth / logoImage.Width,
                                            (double)logoMaxHeight / logoImage.Height);
                    int logoWidth = (int)(logoImage.Width * ratio);
                    int logoHeight = (int)(logoImage.Height * ratio);

                    // Determine top-left coordinates to center the logo on the QR code
                    int posX = (barcodeImage.Width - logoWidth) / 2;
                    int posY = (barcodeImage.Height - logoHeight) / 2;

                    // Draw the resized logo onto the QR code image
                    using (var graphics = Graphics.FromImage(barcodeImage))
                    {
                        graphics.DrawImage(logoImage, posX, posY, logoWidth, logoHeight);
                    }

                    // Save the combined image to the specified output path in PNG format
                    barcodeImage.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user that the process completed successfully
        Console.WriteLine($"QR code with logo saved to {outputPath}");
    }
}
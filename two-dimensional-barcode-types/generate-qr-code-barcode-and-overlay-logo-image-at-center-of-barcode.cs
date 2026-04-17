using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main()
    {
        // Text to encode in the QR code
        string qrText = "https://example.com";

        // Path to the logo image that will be placed at the center of the QR code
        string logoPath = "logo.png";

        // Output file for the final image
        string outputPath = "qr_with_logo.png";

        // Create a QR code generator with high error correction level
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Use Level H error correction to keep the QR readable after adding a logo
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the QR code image
            using (var qrImage = generator.GenerateBarCodeImage())
            {
                // If the logo file exists, overlay it onto the QR code
                if (File.Exists(logoPath))
                {
                    using (var logoImage = new Bitmap(logoPath))
                    {
                        // Define the maximum size of the logo (20% of QR dimensions)
                        int maxLogoWidth = (int)(qrImage.Width * 0.2f);
                        int maxLogoHeight = (int)(qrImage.Height * 0.2f);

                        // Preserve the logo's aspect ratio
                        int logoWidth = logoImage.Width;
                        int logoHeight = logoImage.Height;
                        float scale = Math.Min((float)maxLogoWidth / logoWidth, (float)maxLogoHeight / logoHeight);
                        logoWidth = (int)(logoWidth * scale);
                        logoHeight = (int)(logoHeight * scale);

                        // Calculate position to center the logo
                        int posX = (qrImage.Width - logoWidth) / 2;
                        int posY = (qrImage.Height - logoHeight) / 2;

                        // Draw the logo onto the QR code image
                        using (var graphics = Graphics.FromImage(qrImage))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(logoImage, posX, posY, logoWidth, logoHeight);
                        }
                    }
                }

                // Save the final image with the logo (or just the QR code if logo missing)
                qrImage.Save(outputPath, ImageFormat.Png);
            }
        }
    }
}
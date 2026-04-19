using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Expected image dimensions in pixels
        const int expectedWidth = 300;
        const int expectedHeight = 300;

        // Create a QR Code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the image size explicitly in pixels
            generator.Parameters.ImageWidth.Pixels = expectedWidth;
            generator.Parameters.ImageHeight.Pixels = expectedHeight;

            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to a file
                string outputPath = "qr.png";
                bitmap.Save(outputPath, ImageFormat.Png);

                // Validate the generated image dimensions
                if (bitmap.Width == expectedWidth && bitmap.Height == expectedHeight)
                {
                    Console.WriteLine($"QR code generated successfully with expected size {expectedWidth}x{expectedHeight}.");
                }
                else
                {
                    Console.WriteLine($"Unexpected image size: {bitmap.Width}x{bitmap.Height}. Expected {expectedWidth}x{expectedHeight}.");
                }
            }
        }
    }
}
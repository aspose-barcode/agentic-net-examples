// Title: Generate QR Code with maximum PNG compression
// Description: Demonstrates creating a QR Code barcode and saving it as a PNG file with compression level 9 to achieve minimal file size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set QR error correction, and customize image output using Aspose.Drawing.Imaging. Developers often need to generate barcodes for web links or product information and optimize the resulting image size for faster loading or storage constraints.
// Prompt: Generate QR Code barcode and apply compression level 9 to PNG output for minimal file size.
// Tags: qr code, barcode generation, png compression, aspose.barcode, aspose.drawing, image encoding

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a PNG image
/// with the highest compression level (9) to minimize file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, applies high error correction,
    /// and writes the image to disk using PNG compression level 9.
    /// </summary>
    static void Main()
    {
        // Initialize the QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the highest error correction level (Level H) for robustness.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the barcode image as a Bitmap object.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve the PNG encoder needed to specify compression parameters.
                ImageCodecInfo pngEncoder = GetEncoder(ImageFormat.Png);
                if (pngEncoder == null)
                {
                    Console.WriteLine("PNG encoder not found.");
                    return;
                }

                // Configure encoder parameters: Compression = 9 (maximum compression).
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, 9L);

                    // Save the bitmap to a file using the PNG encoder and compression settings.
                    using (FileStream fs = new FileStream("qr.png", FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fs, pngEncoder, encoderParams);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Retrieves the <see cref="ImageCodecInfo"/> for the specified image format.
    /// </summary>
    /// <param name="format">The image format for which to find the encoder.</param>
    /// <returns>The matching <see cref="ImageCodecInfo"/>, or <c>null</c> if not found.</returns>
    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
                return codec;
        }
        return null;
    }
}
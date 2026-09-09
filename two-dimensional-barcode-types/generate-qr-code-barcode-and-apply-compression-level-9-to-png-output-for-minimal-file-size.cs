// Title: Generate QR Code and Save as Highly Compressed PNG
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, setting the highest error correction level, and saving the image as a PNG with maximum compression to minimize file size.
// Category-Description: Shows how to generate a barcode image using Aspose.BarCode's BarcodeGenerator and manipulate the resulting Aspose.Drawing.Bitmap for output. This example belongs to the barcode generation and image export category, where developers use BarcodeGenerator, BarcodeParameters, and Aspose.Drawing.Imaging classes to create various symbologies and control image format settings such as compression, resolution, and color depth. Useful for scenarios requiring optimized barcode images for web or mobile applications.
// Prompt: Generate QR Code barcode and apply compression level 9 to PNG output for minimal file size.
// Tags: qr code, barcode generation, png compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a PNG with maximum compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, applies compression, and writes the file to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_compressed.png");

        // Initialize the QR Code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the error correction level to the highest (LevelH) for maximum robustness.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the PNG encoder from the available image encoders.
                ImageCodecInfo pngEncoder = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(enc => enc.FormatID == ImageFormat.Png.Guid);

                if (pngEncoder == null)
                {
                    Console.WriteLine("PNG encoder not found.");
                    return;
                }

                // Configure encoder parameters to set compression level to 9 (maximum).
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    // Create a compression parameter (value type long) with the desired level.
                    EncoderParameter compressionParam = new EncoderParameter(Encoder.Compression, 9L);
                    encoderParams.Param[0] = compressionParam;

                    // Save the bitmap to the file system using the PNG encoder and compression settings.
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fs, pngEncoder, encoderParams);
                    }
                }
            }
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}
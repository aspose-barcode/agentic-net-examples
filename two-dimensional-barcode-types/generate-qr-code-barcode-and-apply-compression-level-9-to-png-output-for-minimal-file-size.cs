// Title: Generate QR Code and Save as Highly Compressed PNG
// Description: Creates a QR Code barcode, renders it to PNG, and applies maximum compression (level 9) to minimize file size.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It demonstrates how to use the BarcodeGenerator class to create a QR Code, then leverages Aspose.Drawing to manipulate the resulting image and apply encoder parameters for PNG compression. Typical use cases include generating compact QR Code images for web or mobile applications where bandwidth or storage is limited. Developers often need to control image quality and file size when exporting barcodes, making this pattern a common reference for barcode-to-image workflows.
// Prompt: Generate QR Code barcode and apply compression level 9 to PNG output for minimal file size.
// Tags: qr code, barcode generation, png, compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a PNG image with maximum compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, compresses the PNG, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the compressed PNG.
        string outputPath = "qr_compressed.png";

        // Initialize the QR Code generator with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "Sample QR Code";

            // Render the barcode to a memory stream in PNG format.
            using (MemoryStream tempStream = new MemoryStream())
            {
                generator.Save(tempStream, BarCodeImageFormat.Png);
                tempStream.Position = 0; // Reset stream position for reading.

                // Load the PNG image from the memory stream for further processing.
                using (Bitmap bitmap = new Bitmap(tempStream))
                {
                    // Locate the PNG encoder from the system's available image encoders.
                    ImageCodecInfo pngEncoder = null;
                    foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                    {
                        if (codec.FormatID == ImageFormat.Png.Guid)
                        {
                            pngEncoder = codec;
                            break;
                        }
                    }

                    // If the PNG encoder cannot be found, abort the operation.
                    if (pngEncoder == null)
                    {
                        Console.WriteLine("PNG encoder not found.");
                        return;
                    }

                    // Configure encoder parameters to set compression level to 9 (maximum).
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    EncoderParameter compressionParam = new EncoderParameter(Encoder.Compression, 9L);
                    encoderParams.Param[0] = compressionParam;

                    // Save the bitmap to the final file using the PNG encoder and compression settings.
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(outStream, pngEncoder, encoderParams);
                    }
                }
            }
        }

        Console.WriteLine($"QR Code saved to '{outputPath}' with compression level 9.");
    }
}
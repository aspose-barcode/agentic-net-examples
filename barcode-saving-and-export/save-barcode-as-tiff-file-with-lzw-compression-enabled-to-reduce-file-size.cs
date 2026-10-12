// Title: Save barcode as TIFF with LZW compression
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using LZW compression to reduce file size.
// Category-Description: This example belongs to the Aspose.BarCode image generation and export category. It shows how to use BarcodeGenerator, Bitmap, and System.Drawing.Imaging classes to create a barcode, select the TIFF encoder, and apply LZW compression. Developers often need to export barcodes to compressed image formats for storage or transmission, and this snippet illustrates the typical steps required.
// Prompt: Save a barcode as a TIFF file with LZW compression enabled to reduce file size.
// Tags: barcode, code128, tiff, lzw, compression, image generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode and saves it as a TIFF image with LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, selects the TIFF codec,
    /// configures LZW compression, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_lzw.tiff");

        // Initialize the barcode generator with Code128 symbology and sample data.
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

        // Generate the barcode image as a Bitmap.
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Locate the TIFF image encoder from the installed codecs.
            ImageCodecInfo tiffCodec = null;
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
            {
                if (codec.FormatID == ImageFormat.Tiff.Guid)
                {
                    tiffCodec = codec;
                    break;
                }
            }

            // If the TIFF codec is not found, report and exit.
            if (tiffCodec == null)
            {
                Console.WriteLine("TIFF codec not found.");
                return;
            }

            // Set up encoder parameters to enable LZW compression.
            using (EncoderParameters encoderParams = new EncoderParameters(1))
            {
                encoderParams.Param[0] = new EncoderParameter(
                    Encoder.Compression,
                    (long)EncoderValue.CompressionLZW);

                // Save the bitmap to the specified path using the TIFF codec and compression settings.
                bitmap.Save(outputPath, tiffCodec, encoderParams);
            }
        }

        // Inform the user where the compressed barcode image was saved.
        Console.WriteLine($"Barcode saved with LZW compression to: {outputPath}");
    }
}
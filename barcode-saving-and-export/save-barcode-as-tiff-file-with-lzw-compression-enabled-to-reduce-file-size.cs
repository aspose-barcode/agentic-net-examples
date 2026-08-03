// Title: Save Barcode as TIFF with LZW Compression
// Description: Demonstrates saving a Code128 barcode to a TIFF file using LZW compression to reduce file size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category. It shows how to use the BarcodeGenerator class to create a barcode, render it as a bitmap, and then save the image with specific encoder settings (LZW compression) via Aspose.Drawing.Imaging. Developers often need to export barcodes to various formats with optimized file sizes for storage or transmission.
// Prompt: Save a barcode as a TIFF file with LZW compression enabled to reduce file size.
// Tags: code128, barcode generation, tiff, lzw compression, aspose.barcode, image saving

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a TIFF file
/// using LZW compression to minimize the output file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the compressed TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the TIFF image
        string outputPath = "barcode.tiff";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Render the barcode to a bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec from the installed encoders
                ImageCodecInfo tiffCodec = null;
                foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                {
                    if (codec.FormatID == ImageFormat.Tiff.Guid)
                    {
                        tiffCodec = codec;
                        break;
                    }
                }

                // If the TIFF codec is not found, abort with a message
                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found. Cannot save with LZW compression.");
                    return;
                }

                // Configure encoder parameters to enable LZW compression
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                    // Save the bitmap as a TIFF file using the selected codec and compression settings
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user that the barcode has been saved successfully
        Console.WriteLine($"Barcode saved to {outputPath} with LZW compression.");
    }
}
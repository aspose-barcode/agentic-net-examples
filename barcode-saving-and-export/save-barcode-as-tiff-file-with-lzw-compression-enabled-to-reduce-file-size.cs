// Title: Save barcode as TIFF with LZW compression
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using LZW compression to reduce file size.
// Category-Description: This example belongs to the Aspose.BarCode image export category. It shows how to use BarcodeGenerator to create a barcode, retrieve a bitmap, and employ Aspose.Drawing.Imaging codecs to save the image in TIFF format with LZW compression. Developers working with barcode generation and needing optimized image output for storage or transmission will find this pattern useful.
// Prompt: Save a barcode as a TIFF file with LZW compression enabled to reduce file size.
// Tags: barcode, code128, tiff, lzw, compression, image export, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode and saves it as a TIFF image using LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, generates the barcode,
    /// selects the TIFF codec, applies LZW compression, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary output folder and ensure it exists.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting TIFF file.
        string tiffPath = Path.Combine(outputDir, "barcode_lzw.tiff");

        // Create a barcode generator for Code128 with the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec among the installed encoders.
                ImageCodecInfo tiffCodec = null;
                foreach (var codec in ImageCodecInfo.GetImageEncoders())
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

                // Configure encoder parameters to use LZW compression.
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                    // Save the bitmap as a TIFF file with the specified codec and compression.
                    bitmap.Save(tiffPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved with LZW compression to: {tiffPath}");
    }
}
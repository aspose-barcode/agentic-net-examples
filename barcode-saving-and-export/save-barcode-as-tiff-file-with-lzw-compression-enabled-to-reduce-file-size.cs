// Title: Save a Code128 barcode as a TIFF file with LZW compression
// Description: Demonstrates generating a Code128 barcode image and saving it as a TIFF file using LZW compression to reduce file size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, Bitmap, and image encoding classes to create barcodes and export them in compressed TIFF format. Typical use cases include creating high‑resolution barcode assets for printing or archival where file size matters. Developers often need to select appropriate encoders and codecs to meet storage or transmission constraints.
/// Prompt: Save a barcode as a TIFF file with LZW compression enabled to reduce file size.
/// Tags: code128, barcode generation, tiff, lzw compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a TIFF image with LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, applies LZW compression, and writes the file to a temporary directory.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting TIFF file
        string outputPath = Path.Combine(outputDir, "barcode.tiff");

        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Generate the barcode image as an Aspose.Drawing.Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Set up LZW compression parameters for the TIFF encoder
                Encoder compressionEncoder = Encoder.Compression;
                EncoderParameter compressionParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionLZW);
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = compressionParam;

                    // Locate the TIFF codec among the installed image encoders
                    ImageCodecInfo tiffCodec = null;
                    foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                    {
                        if (codec.FormatID == ImageFormat.Tiff.Guid)
                        {
                            tiffCodec = codec;
                            break;
                        }
                    }

                    // If the TIFF codec is not found, abort the operation
                    if (tiffCodec == null)
                    {
                        Console.WriteLine("TIFF codec not found.");
                        return;
                    }

                    // Save the bitmap as a TIFF file using the LZW compression settings
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
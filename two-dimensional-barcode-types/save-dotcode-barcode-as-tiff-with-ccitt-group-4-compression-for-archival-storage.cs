// Title: Save DotCode barcode as TIFF with CCITT Group 4 compression
// Description: Demonstrates generating a DotCode barcode and saving it as a TIFF image using CCITT Group 4 compression for archival purposes.
// Category-Description: This example belongs to the Aspose.BarCode image generation and export category. It showcases the BarcodeGenerator class to create a DotCode symbology, configures barcode parameters, and uses Aspose.Drawing to encode the resulting bitmap as a TIFF file with CCITT Group 4 compression. Developers working with barcode imaging, archival storage, or document management often need to produce high‑compression, lossless image formats for long‑term retention.
// Prompt: Save DotCode barcode as TIFF with CCITT Group 4 compression for archival storage.
// Tags: dotcode, barcode, tiff, ccitt4, compression, aspose.barcode, image-saving

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DotCode barcode and saves it as a TIFF image using CCITT Group 4 compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, generates the barcode,
    /// and writes the compressed TIFF file to disk.
    /// </summary>
    static void Main()
    {
        // Define output folder in the temporary directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeTiff");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting TIFF file
        string outputPath = Path.Combine(outputDir, "DotCode_CCITT4.tiff");

        // Initialize the barcode generator for DotCode symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleText"))
        {
            // Optional: set the number of columns; rows are chosen automatically by the encoder
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec
                ImageCodecInfo tiffCodec = Array.Find(
                    ImageCodecInfo.GetImageEncoders(),
                    c => c.FormatID == ImageFormat.Tiff.Guid);

                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Set encoder parameters to use CCITT Group 4 compression
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(
                        Encoder.Compression,
                        (long)EncoderValue.CompressionCCITT4);

                    // Save the bitmap as a compressed TIFF file
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }
}
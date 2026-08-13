// Title: Save DotCode barcode as TIFF with CCITT Group 4 compression
// Description: Demonstrates generating a DotCode barcode and saving it as a TIFF image using CCITT Group 4 compression for archival storage.
// Category-Description: This example belongs to the Aspose.BarCode image generation and export category. It showcases the BarcodeGenerator class to create a DotCode symbology, the Aspose.Drawing.Bitmap for image handling, and the use of ImageCodecInfo with EncoderParameters to apply TIFF-specific compression. Developers working with barcode imaging often need to produce compact, lossless files for long‑term storage or printing, and this pattern illustrates the typical workflow for such scenarios.
// Prompt: Save DotCode barcode as TIFF with CCITT Group 4 compression for archival storage.
// Tags: dotcode, barcode, tiff, ccitt4, compression, aspose.barcode, image-saving

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DotCode barcode and saves it as a TIFF file using CCITT Group 4 compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures compression, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "dotcode_g4.tiff");

        // Initialize the barcode generator for DotCode with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "Sample DotCode"))
        {
            // Configure the number of columns; rows are auto‑determined.
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image encoder from the installed codecs.
                ImageCodecInfo tiffCodec = Array.Find(
                    ImageCodecInfo.GetImageEncoders(),
                    c => c.FormatID == ImageFormat.Tiff.Guid);

                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Prepare encoder parameters to apply CCITT Group 4 compression.
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(
                        Encoder.Compression,
                        (long)EncoderValue.CompressionCCITT4);

                    // Save the bitmap to the specified path using the TIFF codec and compression settings.
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }
}
// Title: Save DotCode barcode as TIFF with CCITT Group 4 compression
// Description: Demonstrates generating a DotCode barcode and saving it as a TIFF image using CCITT Group 4 compression for archival purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator, BarcodeParameters, and image encoding classes to produce high‑density, lossless TIFF files. Typical use cases include creating archival‑ready barcodes for documents, shipping labels, or legal records where storage efficiency and fidelity are critical. Developers often need to select appropriate image codecs and compression settings when exporting barcodes to various formats.
// Prompt: Save DotCode barcode as TIFF with CCITT Group 4 compression for archival storage.
// Tags: dotcode, barcode, tiff, ccitt4, compression, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DotCode barcode and saving it as a TIFF image with CCITT Group 4 compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures compression, and writes the TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "DotCodeCCITT4.tiff");

        // Ensure the target directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a DotCode barcode generator with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            // Optional: configure DotCode-specific parameters
            generator.Parameters.Barcode.DotCode.Columns = 20;
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec
                ImageCodecInfo tiffCodec = null;
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == ImageFormat.Tiff.Guid)
                    {
                        tiffCodec = codec;
                        break;
                    }
                }

                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Set CCITT Group 4 compression via encoder parameters
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionCCITT4);
                    // Save the bitmap using the TIFF codec and the specified compression
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        Console.WriteLine($"DotCode barcode saved with CCITT Group 4 compression to: {outputPath}");
    }
}
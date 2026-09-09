// Title: Save Barcode as TIFF with LZW Compression
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using LZW compression to reduce file size.
// Category-Description: This example belongs to the Aspose.BarCode image export category, showcasing how to generate barcodes with the BarcodeGenerator class, render them to a Bitmap, and persist the image using Aspose.Drawing.Imaging with specific encoder parameters. Typical use cases include creating high‑density barcode images for archival or printing where file size matters. Developers often need to select image formats and compression options to meet storage or transmission constraints.
// Prompt: Save a barcode as a TIFF file with LZW compression enabled to reduce file size.
// Tags: barcode, symbology, generation, tiff, lzw, compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
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
    /// Entry point of the example. Creates a temporary output folder, generates the barcode,
    /// applies LZW compression, and writes the TIFF file to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeTiffLzw_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode_lzw.tiff");

        // Generate a Code128 barcode with the specified value
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Render the barcode to a bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec required for saving
                ImageCodecInfo tiffCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);
                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Configure encoder parameters to use LZW compression
                using (EncoderParameters encParams = new EncoderParameters(1))
                {
                    encParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                    // Save the bitmap as a TIFF file with the specified compression
                    bitmap.Save(outputPath, tiffCodec, encParams);
                }
            }
        }

        Console.WriteLine($"Barcode saved with LZW compression to: {outputPath}");
    }
}
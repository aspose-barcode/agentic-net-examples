// Title: Save DotCode barcode as TIFF with CCITT Group 4 compression
// Description: Demonstrates generating a DotCode barcode and saving it as a TIFF image using CCITT Group 4 compression for archival purposes.
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It showcases the BarcodeGenerator class to create a DotCode symbology, the Aspose.Drawing Bitmap for image handling, and the use of ImageCodecInfo with EncoderParameters to apply CCITT Group 4 compression. Typical scenarios include creating high‑density, lossless barcode images for long‑term storage or document archiving where TIFF with CCITT compression is required.
// Prompt: Save DotCode barcode as TIFF with CCITT Group 4 compression for archival storage.
// Tags: dotcode, barcode, tiff, ccitt4, compression, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DotCode barcode and saves it as a TIFF file using CCITT Group 4 compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies compression, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DotCode_CCITT_G4.tiff");

        // Initialize the barcode generator for DotCode symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            // Optional: adjust the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec from the installed encoders.
                ImageCodecInfo tiffCodec = Array.Find(
                    ImageCodecInfo.GetImageEncoders(),
                    c => c.FormatID == ImageFormat.Tiff.Guid);

                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Set up encoder parameters to use CCITT Group 4 compression.
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(
                        Encoder.Compression,
                        (long)EncoderValue.CompressionCCITT4);

                    // Save the bitmap to the specified path with the chosen codec and parameters.
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }
}
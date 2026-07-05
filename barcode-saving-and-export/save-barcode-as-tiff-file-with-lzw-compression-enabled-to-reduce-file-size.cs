// Title: Save Barcode as TIFF with LZW Compression
// Description: Generates a Code128 barcode and saves it as a TIFF image using LZW compression to reduce file size.
// Prompt: Save a barcode as a TIFF file with LZW compression enabled to reduce file size.
// Tags: barcode, code128, tiff, lzw, compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode and save it as a TIFF file with LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode and writes it to a compressed TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the TIFF image
        string outputPath = "barcode.tiff";

        // Ensure the target directory exists; create it if necessary
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode as a bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec from the installed encoders
                ImageCodecInfo tiffCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);
                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Configure encoder parameters to use LZW compression
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                    // Save the bitmap as a TIFF file with the specified compression
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}
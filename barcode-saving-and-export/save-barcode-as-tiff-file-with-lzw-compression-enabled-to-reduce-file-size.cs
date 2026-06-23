using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to PNG,
/// and then saving it as a TIFF file with LZW compression using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode and saves it as a compressed TIFF image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final TIFF image.
        string outputPath = "barcode.tiff";

        // Initialize a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optionally set the barcode height (in points) to improve readability.
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Render the barcode to a memory stream in PNG format.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning before reading.
                memoryStream.Position = 0;

                // Load the PNG data into an Aspose.Drawing.Bitmap for further processing.
                using (var bitmap = new Bitmap(memoryStream))
                {
                    // Locate the TIFF image codec required for saving the bitmap as TIFF.
                    var tiffCodec = ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);

                    // If the TIFF codec is unavailable, inform the user and exit.
                    if (tiffCodec == null)
                    {
                        Console.WriteLine("TIFF codec not found. Cannot save image.");
                        return;
                    }

                    // Configure encoder parameters to use LZW compression for the TIFF output.
                    using (var encoderParams = new EncoderParameters(1))
                    {
                        encoderParams.Param[0] = new EncoderParameter(
                            Encoder.Compression,
                            (long)EncoderValue.CompressionLZW);

                        // Save the bitmap as a TIFF file with the specified compression.
                        bitmap.Save(outputPath, tiffCodec, encoderParams);
                    }
                }
            }
        }

        // Notify the user that the barcode image has been saved successfully.
        Console.WriteLine($"Barcode saved successfully to '{outputPath}'.");
    }
}
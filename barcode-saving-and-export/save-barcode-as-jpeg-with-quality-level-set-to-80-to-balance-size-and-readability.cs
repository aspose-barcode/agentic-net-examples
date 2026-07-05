// Title: Save Barcode as JPEG with Specified Quality
// Description: Generates a Code128 barcode and saves it as a JPEG image with quality level 80 to balance file size and readability.
// Prompt: Save a barcode as a JPEG with quality level set to 80 to balance size and readability.
// Tags: code128, barcode, jpeg, quality, aspose.barcode, aspose.drawing

using System;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Code128 barcode and save it as a JPEG
/// with a specific quality setting using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Render the barcode to a bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the JPEG image codec from the installed encoders
                ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                // If the JPEG codec cannot be found, fall back to default saving
                if (jpegCodec == null)
                {
                    Console.WriteLine("JPEG codec not found. Saving with default settings.");
                    bitmap.Save("barcode.jpg", ImageFormat.Jpeg);
                    return;
                }

                // Prepare encoder parameters to set JPEG quality to 80 (value must be a long)
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);

                // Save the bitmap as a JPEG file using the specified codec and quality settings
                bitmap.Save("barcode.jpg", jpegCodec, encoderParams);
            }
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine("Barcode saved as barcode.jpg with quality 80.");
    }
}
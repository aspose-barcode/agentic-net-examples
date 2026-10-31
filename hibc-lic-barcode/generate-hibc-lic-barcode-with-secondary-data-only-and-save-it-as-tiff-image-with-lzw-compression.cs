// Title: Generate HIBC LIC barcode with secondary data only and save as TIFF (LZW)
// Description: Demonstrates creating a HIBC LIC barcode that contains only secondary data, then saving the image as a TIFF file using LZW compression.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the ComplexBarcodeGenerator class together with HIBCLICSecondaryAndAdditionalDataCodetext to encode secondary information such as expiry date, lot number, and serial number. Typical use cases include pharmaceutical labeling and inventory tracking where only supplemental data is required. Developers often need to customize image dimensions and compression settings for downstream systems.
// Prompt: Generate a HIBC LIC barcode with secondary data only and save it as a TIFF image with LZW compression.
// Tags: hibc lic barcode, secondary data, tiff, lzw compression, aspose.barcode, complexbarcodegenerator, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBC LIC barcode with secondary data only
/// and saves it as a TIFF image using LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures image parameters,
    /// and writes the result to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICSecondary.tif");

        // Prepare secondary data for the HIBC LIC barcode.
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                DateOfManufacture = DateTime.Now.AddMonths(-1)
            },
            LinkCharacter = '+'
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            // Set the X-dimension (module width) to 5 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Produce the bitmap representation of the barcode.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image codec required for saving.
                var codecs = ImageCodecInfo.GetImageEncoders();
                var tiffCodec = Array.Find(codecs, c => c.FormatID == ImageFormat.Tiff.Guid);
                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Configure encoder parameters to use LZW compression.
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                    // Save the bitmap as a TIFF file with the specified compression.
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        Console.WriteLine($"HIBC LIC secondary data barcode saved to: {outputPath}");
    }
}
// Title: Generate HIBC LIC barcode with secondary data and save as LZW‑compressed TIFF
// Description: Demonstrates creating a HIBC LIC barcode that contains only secondary and additional data, then saving the image as a TIFF file using LZW compression.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It shows how to use the ComplexBarcodeGenerator with HIBCLICSecondaryAndAdditionalDataCodetext, configure barcode parameters, and export the result to a TIFF image with LZW compression. Developers working with healthcare or logistics barcodes often need to embed secondary data (expiry, lot, serial) and produce high‑quality compressed images for printing or archival.
// Prompt: Generate a HIBC LIC barcode with secondary data only and save it as a TIFF image with LZW compression.
// Tags: hibc, lic, barcode, generation, tiff, lzw, compression, secondary data, complexbarcode, aspnet

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC LIC barcode containing secondary data and saving it as a LZW‑compressed TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICSecondary.tiff");

        // Prepare secondary and additional data for the HIBC LIC barcode.
        var secondaryData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Now,
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            Quantity = 30,
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            DateOfManufacture = DateTime.Now
        };

        // Create the complex codetext object that includes the secondary data.
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = secondaryData,
            LinkCharacter = '+'
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the X-dimension (module width) of the barcode in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Render the barcode to a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image encoder.
                var tiffCodec = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);

                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF codec not found.");
                    return;
                }

                // Configure encoder parameters to use LZW compression.
                using (var encoderParams = new EncoderParameters(1))
                {
                    var compressionEncoder = Encoder.Compression;
                    var compressionParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionLZW);
                    encoderParams.Param[0] = compressionParam;

                    // Save the bitmap as a TIFF file with the specified compression.
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}
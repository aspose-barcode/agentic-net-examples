// Title: Generate HIBC LIC barcode with secondary data and save as LZW‑compressed TIFF
// Description: Demonstrates creating a HIBC LIC barcode that contains only secondary and additional data, then saving the image as a TIFF file using LZW compression.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use ComplexBarcodeGenerator with HIBCLICSecondaryAndAdditionalDataCodetext to encode HIBC LIC symbology, a common requirement in healthcare and logistics for encoding lot and serial numbers. Developers often need to generate such barcodes and export them to lossless image formats like TIFF with specific compression settings.
// Prompt: Generate a HIBC LIC barcode with secondary data only and save it as a TIFF image with LZW compression.
// Tags: hibc, lic, secondary-data, tiff, lzw, complexbarcode, generation

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a HIBC LIC barcode containing only secondary data
/// and saves it as a TIFF image using LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data (lot number and serial number) for the HIBC LIC barcode
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN456"
        };

        // Build the codetext object that represents HIBC LIC secondary‑and‑additional‑data
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC, // HIBC LIC Code128 symbology
            LinkCharacter = '+',                     // Required link character for HIBC
            Data = secondaryData
        };

        // Generate the barcode image using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF encoder from the installed image codecs
                var tiffEncoder = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(enc => enc.FormatID == ImageFormat.Tiff.Guid);
                if (tiffEncoder == null)
                {
                    Console.WriteLine("TIFF encoder not found.");
                    return;
                }

                // Configure encoder parameters to use LZW compression
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(
                        Encoder.Compression,
                        (long)EncoderValue.CompressionLZW);

                    // Save the bitmap to a memory stream with the specified encoder settings
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, tiffEncoder, encoderParams);
                        ms.Position = 0;

                        // Write the resulting TIFF file to disk
                        File.WriteAllBytes("hibc_lic_secondary.tif", ms.ToArray());
                    }
                }
            }
        }

        Console.WriteLine("HIBC LIC barcode with secondary data saved as TIFF (LZW compression).");
    }
}
// Title: Generate HIBC LIC barcode with secondary data and save as LZW‑compressed TIFF
// Description: Demonstrates creating a HIBC LIC barcode that contains only secondary data (lot and serial numbers) and saving the result as a TIFF image using LZW compression.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and image encoding classes to produce high‑quality TIFF output. Developers needing to embed secondary information in HIBC barcodes for healthcare or logistics can follow this pattern.
// Prompt: Generate a HIBC LIC barcode with secondary data only and save it as a TIFF image with LZW compression.
// Tags: hibc, lic, barcode, generation, tiff, lzw, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program demonstrating generation of a HIBC LIC barcode with secondary data only and saving it as an LZW‑compressed TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, encodes it, and writes the TIFF file.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data for the HIBC LIC barcode
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SN456"
            }
        };

        // Define the output file path
        string outputPath = "hibc_secondary.tiff";

        // Generate the barcode image and save it as an LZW‑compressed TIFF
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        using (Image image = generator.GenerateBarCodeImage())
        using (var bitmap = new Bitmap(image))
        {
            // Retrieve the TIFF codec information
            var tiffCodec = ImageCodecInfo.GetImageEncoders()
                .First(c => c.FormatID == ImageFormat.Tiff.Guid);

            // Set encoder parameters to use LZW compression
            using (var encoderParams = new EncoderParameters(1))
            {
                encoderParams.Param[0] = new EncoderParameter(
                    Encoder.Compression, (long)EncoderValue.CompressionLZW);

                // Save the bitmap to the specified path with the chosen codec and parameters
                bitmap.Save(outputPath, tiffCodec, encoderParams);
            }
        }

        // Output the full path of the saved barcode image
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}
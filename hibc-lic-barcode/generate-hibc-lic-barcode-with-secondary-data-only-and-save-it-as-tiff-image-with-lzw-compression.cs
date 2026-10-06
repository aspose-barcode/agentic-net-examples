// Title: Generate HIBC LIC barcode with secondary data and save as LZW‑compressed TIFF
// Description: Demonstrates creating a HIBC LIC barcode that contains only secondary data, then saving the image as a TIFF file using LZW compression.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It shows how to use the ComplexBarcodeGenerator with HIBCLICSecondaryAndAdditionalDataCodetext, configure barcode parameters, and export the result to a TIFF image with specific encoder settings. Developers working with healthcare or logistics labeling often need to embed secondary information (lot, serial, dates) in HIBC barcodes and require lossless compression for archival storage.
// Prompt: Generate a HIBC LIC barcode with secondary data only and save it as a TIFF image with LZW compression.
// Tags: hibc, lic, barcode, secondary-data, tiff, lzw, compression, aspnet, barcodelib, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a HIBC LIC barcode containing only secondary data
/// and saves it as a TIFF image with LZW compression.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures image encoding,
    /// and writes the output file to the current directory.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICSecondary.tif");

        // Create a complex codetext object for HIBC LIC with secondary and additional data.
        HIBCLICSecondaryAndAdditionalDataCodetext complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC, // QR‑based HIBC LIC symbology
            LinkCharacter = '+' // Required link character for HIBC
        };

        // Populate the secondary data fields (lot, serial, quantity, dates).
        complexCodetext.Data = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            Quantity = 30,
            ExpiryDate = DateTime.Now,
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            DateOfManufacture = DateTime.Now
        };

        // Initialize the complex barcode generator with the prepared codetext.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the X‑dimension (module width) of the barcode in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Locate the TIFF image encoder from the system's available encoders.
                ImageCodecInfo tiffCodec = null;
                foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
                {
                    if (codec.FormatID == ImageFormat.Tiff.Guid)
                    {
                        tiffCodec = codec;
                        break;
                    }
                }

                // If the TIFF encoder is not found, abort with a message.
                if (tiffCodec == null)
                {
                    Console.WriteLine("TIFF encoder not found.");
                    return;
                }

                // Configure LZW compression for the TIFF output.
                Encoder compressionEncoder = Encoder.Compression;
                EncoderParameter compressionParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionLZW);
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = compressionParam;

                    // Save the bitmap to the specified path using the TIFF encoder and compression settings.
                    bitmap.Save(outputPath, tiffCodec, encoderParams);
                }
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
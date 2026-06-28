using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC LIC barcode with secondary and additional data,
/// saved as an LZW‑compressed TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode and writes it to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated TIFF image.
        string outputPath = "hibc_secondary.tiff";

        // Prepare secondary and additional data (lot number and serial number) for the HIBC LIC barcode.
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN001"
        };

        // Configure the complex codetext to include only the secondary data.
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC, // Select the HIBC Code 128 LIC symbology.
            LinkCharacter = '+',                     // Set the required link character.
            Data = secondaryData                     // Attach the secondary data object.
        };

        // Generate the barcode image and save it as an LZW‑compressed TIFF.
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        using (Image image = generator.GenerateBarCodeImage())
        using (var bitmap = new Bitmap(image))
        {
            // Retrieve the TIFF encoder from the list of available image encoders.
            var tiffCodec = ImageCodecInfo.GetImageEncoders()
                .First(c => c.FormatID == ImageFormat.Tiff.Guid);

            // Set up encoder parameters to specify LZW compression.
            using (var encoderParams = new EncoderParameters(1))
            {
                encoderParams.Param[0] = new EncoderParameter(
                    Encoder.Compression,
                    (long)EncoderValue.CompressionLZW);

                // Save the bitmap to the specified path using the TIFF codec and compression settings.
                bitmap.Save(outputPath, tiffCodec, encoderParams);
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"HIBC LIC barcode saved to: {outputPath}");
    }
}
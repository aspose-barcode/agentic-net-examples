using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare secondary data for HIBC LIC barcode
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SER123",
            Quantity = 10,
            ExpiryDate = DateTime.Today.AddDays(30),
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY
        };

        // Configure complex codetext with required link character
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Generate the barcode image
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Find TIFF encoder
            var tiffEncoder = ImageCodecInfo.GetImageEncoders()
                .FirstOrDefault(enc => enc.FormatID == ImageFormat.Tiff.Guid);

            if (tiffEncoder == null)
                throw new InvalidOperationException("TIFF encoder not found.");

            // Set LZW compression
            using (var encoderParams = new EncoderParameters(1))
            {
                encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                // Save to file with LZW compression
                bitmap.Save("hibc_lic.tif", tiffEncoder, encoderParams);
            }
        }
    }
}
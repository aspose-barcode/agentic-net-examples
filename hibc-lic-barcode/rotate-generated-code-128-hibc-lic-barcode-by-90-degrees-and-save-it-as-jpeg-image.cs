// Title: Rotate Code 128 HIBC LIC barcode and save as JPEG
// Description: Generates a HIBC Code 128 LIC barcode with secondary data, rotates the image 90 degrees, and saves it as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and SecondaryAndAdditionalData classes to create HIBC‑based barcodes, apply image transformations, and export to common image formats. Developers working with healthcare or logistics barcodes often need to embed additional data, rotate barcodes for label orientation, and produce JPEG outputs for web or print workflows.
// Prompt: Rotate the generated Code 128 HIBC LIC barcode by 90 degrees and save it as a JPEG image.
// Tags: barcode, code128, hibc, rotation, jpeg, aspose.barcode, complexbarcode, generation

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a HIBC Code 128 LIC barcode with secondary data,
/// rotate the resulting image by 90 degrees, and save it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, rotation, and saving.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data (lot number and serial number) for the HIBC LIC barcode.
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SER123"
        };

        // Configure the complex codetext, specifying the barcode type, link character, and secondary data.
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Create a generator for the complex barcode using the configured codetext.
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Rotate the bitmap 90 degrees clockwise without flipping.
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Save the rotated bitmap to a JPEG file.
                bitmap.Save("hibc_code128_lic_rotated.jpg", ImageFormat.Jpeg);
            }
        }
    }
}
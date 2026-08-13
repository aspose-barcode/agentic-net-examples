// Title: Barcode checksum validation for multiple symbologies in a single read
// Description: Demonstrates enabling checksum validation for both mandatory and optional checksum symbologies while reading multiple barcodes in one image.
// Category-Description: This example belongs to the Aspose.BarCode reading and validation category. It shows how to use BarCodeReader with BarcodeSettings.ChecksumValidation to enforce checksum checks across all supported symbologies, a common requirement when processing 1D barcodes such as EAN13 (mandatory checksum) and Code39 (optional checksum). Developers often need to validate data integrity in batch scanning scenarios, and this snippet illustrates the typical API usage for combined image generation and validation.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for both obligatory and optional checksum symbologies in a single read operation.
// Tags: barcode symbology, checksum validation, read operation, aspose.barcode, generation, recognition

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates two barcodes, combines them into a single image,
/// and reads them back with checksum validation enabled for both mandatory and optional checksum symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates EAN13 and Code39 barcodes, merges them, and reads them with checksum validation turned on.
    /// </summary>
    static void Main()
    {
        // Generate an EAN13 barcode (checksum is mandatory)
        using (var eanGenerator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        using (var eanImage = eanGenerator.GenerateBarCodeImage())
        // Generate a Code39 barcode (checksum is optional)
        using (var code39Generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        using (var code39Image = code39Generator.GenerateBarCodeImage())
        // Combine both images side by side into a single bitmap
        using (var combined = new Bitmap(eanImage.Width + code39Image.Width,
                                         Math.Max(eanImage.Height, code39Image.Height)))
        {
            // Draw the two barcode images onto the combined bitmap
            using (var graphics = Graphics.FromImage(combined))
            {
                graphics.DrawImage(eanImage, 0, 0);
                graphics.DrawImage(code39Image, eanImage.Width, 0);
            }

            // Read both barcodes in a single operation with checksum validation enabled
            using (var reader = new BarCodeReader(combined, DecodeType.AllSupportedTypes))
            {
                // Enable checksum validation for all symbologies (mandatory and optional)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Iterate through all detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // For 1D barcodes, also output the checksum if available
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
// Title: Demonstrate checksum validation for mandatory and optional symbologies in a single read
// Description: This example generates an EAN13 barcode (mandatory checksum) and a Code39 barcode (optional checksum), combines them, and reads both with checksum validation turned on.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for both obligatory and optional checksum symbologies in a single read operation.
// Tags: barcode symbology, checksum validation, read operation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates two barcodes, merges them into a single image,
/// and reads them back with checksum validation enabled for both mandatory
/// and optional checksum symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, combines them, and performs a single read
    /// operation with <c>ChecksumValidation</c> set to <c>On</c>.
    /// </summary>
    static void Main()
    {
        // Generate first barcode (EAN13) – checksum is mandatory
        using (var eanGenerator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            using (var eanStream = new MemoryStream())
            {
                eanGenerator.Save(eanStream, BarCodeImageFormat.Png);
                eanStream.Position = 0;

                // Generate second barcode (Code39) – checksum is optional
                using (var code39Generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
                {
                    using (var code39Stream = new MemoryStream())
                    {
                        code39Generator.Save(code39Stream, BarCodeImageFormat.Png);
                        code39Stream.Position = 0;

                        // Load both images from the memory streams
                        using (var eanImage = new Bitmap(eanStream))
                        using (var code39Image = new Bitmap(code39Stream))
                        {
                            // Create a combined image (side by side)
                            int combinedWidth = eanImage.Width + code39Image.Width;
                            int combinedHeight = Math.Max(eanImage.Height, code39Image.Height);
                            using (var combinedImage = new Bitmap(combinedWidth, combinedHeight))
                            {
                                using (var graphics = Graphics.FromImage(combinedImage))
                                {
                                    graphics.DrawImage(eanImage, 0, 0);
                                    graphics.DrawImage(code39Image, eanImage.Width, 0);
                                }

                                // Read both barcodes in a single operation with checksum validation enabled
                                using (var reader = new BarCodeReader(combinedImage, DecodeType.EAN13, DecodeType.Code39))
                                {
                                    // Enable checksum validation for all symbologies (mandatory and optional)
                                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                                    // Iterate through detected barcodes and output details
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine("Detected Type: " + result.CodeTypeName);
                                        Console.WriteLine("CodeText: " + result.CodeText);

                                        // For 1D barcodes, checksum info is available in Extended.OneD
                                        if (result.Extended.OneD != null)
                                        {
                                            Console.WriteLine("Value: " + result.Extended.OneD.Value);
                                            Console.WriteLine("Checksum: " + result.Extended.OneD.CheckSum);
                                        }

                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating two different barcodes, combining them into a single image,
/// and then reading back the barcodes from the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate the first barcode (EAN13) which requires a checksum.
        // ------------------------------------------------------------
        using (var generatorEan = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            using (var eanImage = generatorEan.GenerateBarCodeImage())
            {
                // ------------------------------------------------------------
                // 2. Generate the second barcode (Code39) where the checksum is optional.
                // ------------------------------------------------------------
                using (var generatorCode39 = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
                {
                    using (var code39Image = generatorCode39.GenerateBarCodeImage())
                    {
                        // ------------------------------------------------------------
                        // 3. Combine the two barcode images side by side with padding.
                        // ------------------------------------------------------------
                        int padding = 20;
                        int combinedWidth = eanImage.Width + code39Image.Width + padding;
                        int combinedHeight = Math.Max(eanImage.Height, code39Image.Height);

                        using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (var graphics = Graphics.FromImage(combinedBitmap))
                            {
                                // Fill background with white.
                                graphics.Clear(Color.White);
                                // Draw the first barcode at the left.
                                graphics.DrawImage(eanImage, 0, 0);
                                // Draw the second barcode to the right of the first, separated by padding.
                                graphics.DrawImage(code39Image, eanImage.Width + padding, 0);
                            }

                            // ------------------------------------------------------------
                            // 4. Save the combined image to a memory stream (optional, for demo purposes).
                            // ------------------------------------------------------------
                            using (var ms = new MemoryStream())
                            {
                                combinedBitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                                ms.Position = 0; // Reset stream position for reading.

                                // ------------------------------------------------------------
                                // 5. Read barcodes from the combined image.
                                // ------------------------------------------------------------
                                using (var reader = new BarCodeReader(new Bitmap(ms), DecodeType.AllSupportedTypes))
                                {
                                    // Enable checksum validation for all supported symbologies.
                                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                                    // Iterate through all detected barcodes.
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                        Console.WriteLine($"CodeText: {result.CodeText}");

                                        // For 1D barcodes, display the numeric value and checksum if available.
                                        if (result.Extended?.OneD != null)
                                        {
                                            Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                                            Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                                        }

                                        Console.WriteLine(new string('-', 40));
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
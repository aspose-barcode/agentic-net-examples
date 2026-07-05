// Title: Multi-format barcode generation and recognition demo
// Description: Demonstrates generating Code128 and DataMatrix barcodes, combining them into a single image, and recognizing both types using MultiDecodeType.
// Prompt: Configure MultyDecodeType with Code128 and DataMatrix, then recognize both types in a single image.
// Tags: barcode, code128, datamatrix, multidecode, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a combined image containing a Code128 and a DataMatrix barcode,
/// then reads both barcodes from the single image using multi‑decode functionality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcodes, merges them, saves the combined image, and prints detected results.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode image
        using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128"))
        {
            using (Bitmap code128Image = code128Generator.GenerateBarCodeImage())
            {
                // Generate a DataMatrix barcode image
                using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM12345"))
                {
                    using (Bitmap dmImage = dmGenerator.GenerateBarCodeImage())
                    {
                        // Determine combined image size (place side by side with a 20‑pixel gap)
                        int combinedWidth = code128Image.Width + dmImage.Width + 20;
                        int combinedHeight = Math.Max(code128Image.Height, dmImage.Height);

                        // Create a new bitmap that will hold both barcodes
                        using (Bitmap combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (Graphics graphics = Graphics.FromImage(combinedBitmap))
                            {
                                // Fill the background with white for better contrast
                                graphics.Clear(Color.White);

                                // Draw the Code128 barcode on the left, vertically centered
                                graphics.DrawImage(code128Image, 0, (combinedHeight - code128Image.Height) / 2);

                                // Draw the DataMatrix barcode on the right, leaving a 20‑pixel gap, vertically centered
                                graphics.DrawImage(dmImage, code128Image.Width + 20, (combinedHeight - dmImage.Height) / 2);
                            }

                            // Save the combined image to a file (optional, useful for visual verification)
                            string combinedPath = "combined.png";
                            combinedBitmap.Save(combinedPath, ImageFormat.Png);

                            // Recognize both barcode types from the combined image using MultiDecodeType
                            using (var reader = new BarCodeReader(combinedBitmap, DecodeType.Code128, DecodeType.DataMatrix))
                            {
                                foreach (BarCodeResult result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine("Detected Type: " + result.CodeTypeName);
                                    Console.WriteLine("Decoded Text: " + result.CodeText);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
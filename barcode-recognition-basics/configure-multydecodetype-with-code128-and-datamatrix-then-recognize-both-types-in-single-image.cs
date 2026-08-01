// Title: Multi-Decode of Code128 and DataMatrix in a Single Image
// Description: Demonstrates generating Code128 and DataMatrix barcodes, combining them into one PNG image, and recognizing both types using MultiDecodeType.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes, the Bitmap and Graphics classes for image composition, and the BarCodeReader with multiple DecodeType parameters for simultaneous detection. Developers often need to process mixed-symbology images, making multi‑decode a common requirement in inventory, logistics, and retail applications.
// Prompt: Configure MultyDecodeType with Code128 and DataMatrix, then recognize both types in a single image.
// Tags: barcode symbology, multi-decode, png, barcodegenerator, barcodereader, aspnet.barcode, aspnet.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 and DataMatrix barcodes, merges them into a single image,
/// and reads both barcode types using multi‑decode functionality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates two barcodes, combines them, and decodes them.
    /// </summary>
    static void Main()
    {
        // Sample texts for each barcode type
        const string code128Text = "CODE128_SAMPLE";
        const string dataMatrixText = "DATAMATRIX_SAMPLE";

        // Generate Code128 barcode and store it in a memory stream
        using (var code128Stream = new MemoryStream())
        {
            using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, code128Text))
            {
                code128Generator.Save(code128Stream, BarCodeImageFormat.Png);
            }
            code128Stream.Position = 0; // Reset stream position for reading

            // Generate DataMatrix barcode and store it in a separate memory stream
            using (var dataMatrixStream = new MemoryStream())
            {
                using (var dataMatrixGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, dataMatrixText))
                {
                    dataMatrixGenerator.Save(dataMatrixStream, BarCodeImageFormat.Png);
                }
                dataMatrixStream.Position = 0; // Reset stream position for reading

                // Load both barcode images as Bitmap objects
                using (var code128Bitmap = new Bitmap(code128Stream))
                using (var dataMatrixBitmap = new Bitmap(dataMatrixStream))
                {
                    // Determine dimensions for the combined image (side‑by‑side with a gap)
                    const int gap = 20;
                    int combinedWidth = code128Bitmap.Width + dataMatrixBitmap.Width + gap;
                    int combinedHeight = Math.Max(code128Bitmap.Height, dataMatrixBitmap.Height);

                    // Create a new bitmap to hold the combined image
                    using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                    {
                        // Draw the two barcodes onto the combined bitmap
                        using (var graphics = Graphics.FromImage(combinedBitmap))
                        {
                            graphics.Clear(Color.White); // Set background to white
                            graphics.DrawImage(code128Bitmap, 0, (combinedHeight - code128Bitmap.Height) / 2);
                            graphics.DrawImage(dataMatrixBitmap, code128Bitmap.Width + gap, (combinedHeight - dataMatrixBitmap.Height) / 2);
                        }

                        // Save the combined image to disk
                        const string combinedPath = "combined.png";
                        combinedBitmap.Save(combinedPath, ImageFormat.Png);

                        // Ensure the file was created before attempting to read it
                        if (!File.Exists(combinedPath))
                        {
                            Console.WriteLine("Failed to create the combined barcode image.");
                            return;
                        }

                        // Initialize the reader with both DecodeType values (multi‑decode)
                        using (var reader = new BarCodeReader(combinedPath, DecodeType.Code128, DecodeType.DataMatrix))
                        {
                            // Iterate through all detected barcodes and output their details
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                Console.WriteLine($"Decoded Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}
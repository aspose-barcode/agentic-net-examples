using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, reading it, and then re‑reading after
/// replacing the image source in the same <see cref="BarCodeReader"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Store the generated image in a memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the generated image into a Bitmap object.
                using (var originalBitmap = new Bitmap(ms))
                {
                    // Create a BarCodeReader that can decode all supported barcode types.
                    using (var reader = new BarCodeReader(originalBitmap, DecodeType.AllSupportedTypes))
                    {
                        Console.WriteLine("Reading barcode from original image:");

                        // Iterate through all detected barcodes and output their type and text.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }

                        // Create a new in‑memory bitmap with a white background,
                        // matching the size of the original bitmap.
                        using (var newBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height))
                        {
                            using (var graphics = Graphics.FromImage(newBitmap))
                            {
                                graphics.Clear(Color.White); // Fill the bitmap with white.
                                // Optionally draw additional graphics here.
                            }

                            // Replace the image source inside the existing reader with the new bitmap.
                            reader.SetBarCodeImage(newBitmap);

                            Console.WriteLine("Reading barcode after SetBarCodeImage:");

                            // Read barcodes again from the new image source.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}
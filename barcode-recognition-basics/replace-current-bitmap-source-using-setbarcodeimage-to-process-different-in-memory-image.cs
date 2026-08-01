// Title: Replace barcode image source using SetBarCodeImage
// Description: Demonstrates how to replace the image source of a BarCodeReader with a different in‑memory bitmap.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode, BarCodeReader to decode it, and the SetBarCodeImage method to swap the source image at runtime. Developers working with dynamic image streams, in‑memory processing, or custom image pipelines commonly need these APIs to read barcodes without persisting intermediate files.
// Prompt: Replace the current bitmap source using SetBarCodeImage to process a different in‑memory image.
// Tags: barcode, setbarcodeimage, in-memory image, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, reads it, then replaces the
/// source image with a new in‑memory bitmap using <c>SetBarCodeImage</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, swaps the image,
    /// and attempts to read again.
    /// </summary>
    static void Main()
    {
        // Generate a simple Code128 barcode and keep it in memory
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the original image to a file (optional, just for visual verification)
                const string originalPath = "original.png";
                originalBitmap.Save(originalPath, ImageFormat.Png);
                Console.WriteLine($"Original barcode saved to {originalPath}");

                // Create a BarCodeReader using the generated bitmap
                using (var reader = new BarCodeReader(originalBitmap, DecodeType.Code128))
                {
                    // Read and display the first detected barcode from the original image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected barcode (original image): Type={result.CodeTypeName}, Text={result.CodeText}");
                    }

                    // Create a different in‑memory image (blank white bitmap)
                    using (Bitmap newBitmap = new Bitmap(200, 100, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics graphics = Graphics.FromImage(newBitmap))
                        {
                            // Fill the bitmap with white background
                            graphics.Clear(Color.White);
                        }

                        // Replace the bitmap source of the reader with the new image
                        reader.SetBarCodeImage(newBitmap);

                        // Attempt to read barcodes from the new image
                        bool anyFound = false;
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            anyFound = true;
                            Console.WriteLine($"Detected barcode (new image): Type={result.CodeTypeName}, Text={result.CodeText}");
                        }

                        if (!anyFound)
                        {
                            Console.WriteLine("No barcode detected in the new image after SetBarCodeImage.");
                        }
                    }
                }
            }
        }
    }
}
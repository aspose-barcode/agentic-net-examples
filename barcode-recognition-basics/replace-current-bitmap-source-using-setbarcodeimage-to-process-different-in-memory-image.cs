// Title: Replace barcode image source with a new in‑memory bitmap
// Description: Demonstrates how to generate a barcode, save it to a memory stream, load a new bitmap from that stream, and replace the BarCodeReader's image source using SetBarCodeImage.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation and recognition category. It shows how to work with in‑memory images using the BarcodeGenerator, Bitmap, MemoryStream, and BarCodeReader classes. Typical use cases include processing dynamically created or transformed barcode images without writing to disk, a common requirement for web services and automated CI pipelines.
// Prompt: Replace the current bitmap source using SetBarCodeImage to process a different in‑memory image.
// Tags: barcode, code128, setbarcodeimage, in-memory, bitmap, memorystream, aspnet, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates replacing the BarCodeReader's image source with a different in‑memory bitmap.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, creates a new bitmap from a memory stream,
    /// and reads the barcode using the replaced image source.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode image as a bitmap
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Save the generated bitmap to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    originalBitmap.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Load a new bitmap from the memory stream (represents a different in‑memory image)
                    using (Bitmap newBitmap = new Bitmap(ms))
                    {
                        // Initialize a BarCodeReader without an initial image source
                        using (BarCodeReader reader = new BarCodeReader())
                        {
                            // Replace the reader's source with the new bitmap
                            reader.SetBarCodeImage(newBitmap);
                            // Configure the reader to look for Code128 barcodes only
                            reader.SetBarCodeReadType(DecodeType.Code128);

                            Console.WriteLine("Reading barcode from replaced bitmap source:");
                            // Iterate through all detected barcodes and output their type and text
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}
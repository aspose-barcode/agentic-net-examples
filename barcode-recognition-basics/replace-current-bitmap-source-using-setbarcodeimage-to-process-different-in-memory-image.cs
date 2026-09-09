// Title: SetBarCodeImage with an in‑memory bitmap for barcode recognition
// Description: Demonstrates how to replace the source image of a BarCodeReader using SetBarCodeImage, processing a bitmap generated in memory.
// Category-Description: This example belongs to the Aspose.BarCode image handling category, illustrating the use of BarcodeGenerator to create barcodes and BarCodeReader to recognize them without saving to disk. Key API classes include BarcodeGenerator, BarCodeReader, and SetBarCodeImage. Developers often need to work with in‑memory images for performance or when file I/O is restricted.
// Prompt: Replace the current bitmap source using SetBarCodeImage to process a different in‑memory image.
// Tags: code128, setbarcodeimage, inmemory, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates barcodes in memory and reads them using
/// Aspose.BarCode's SetBarCodeImage method.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcodes, then uses the second bitmap as the
    /// source for a BarCodeReader via SetBarCodeImage.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate the first barcode (demonstration only; not used later)
        // ------------------------------------------------------------
        using (BarcodeGenerator gen1 = new BarcodeGenerator(EncodeTypes.Code128, "First123"))
        {
            using (Bitmap bmp1 = gen1.GenerateBarCodeImage())
            {
                // The bitmap could be saved or processed here.
                // For this example we simply let it be disposed.
            }
        }

        // ------------------------------------------------------------
        // Generate the second barcode which will serve as the in‑memory source
        // for recognition.
        // ------------------------------------------------------------
        using (BarcodeGenerator gen2 = new BarcodeGenerator(EncodeTypes.Code128, "Second456"))
        {
            using (Bitmap sourceBitmap = gen2.GenerateBarCodeImage())
            {
                // Create a BarCodeReader without an initial image.
                using (BarCodeReader reader = new BarCodeReader())
                {
                    // Replace the reader's image source with the in‑memory bitmap.
                    reader.SetBarCodeImage(sourceBitmap);

                    // Optionally limit decoding to Code128 symbology.
                    reader.SetBarCodeReadType(DecodeType.Code128);

                    Console.WriteLine("Reading barcode from in‑memory bitmap:");
                    // Iterate through all detected barcodes and output their details.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                    }
                }
            }
        }
    }
}
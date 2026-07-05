// Title: Demonstrate SetBarCodeImage to replace barcode source
// Description: Shows how to generate two barcode images in memory, read the first, then replace the reader's image with a second barcode using SetBarCodeImage.
// Prompt: Replace the current bitmap source using SetBarCodeImage to process a different in‑memory image.
// Tags: barcode, setbarcodeimage, in-memory, code128, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that demonstrates replacing the bitmap source of a <see cref="BarCodeReader"/>
/// with a different in‑memory image using <c>SetBarCodeImage</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two Code128 barcodes in memory, reads the first,
    /// then swaps the reader's image to the second barcode and reads again.
    /// </summary>
    static void Main()
    {
        // Generate the first barcode image (Code128) and keep it in a memory stream
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "First123"))
        {
            using (var stream1 = new MemoryStream())
            {
                generator1.Save(stream1, BarCodeImageFormat.Png);
                stream1.Position = 0; // Reset stream position for reading

                // Load the first image into a Bitmap object
                using (var bitmap1 = new Bitmap(stream1))
                {
                    // Create a reader for the first bitmap, configured for Code128 decoding
                    using (var reader = new BarCodeReader(bitmap1, DecodeType.Code128))
                    {
                        Console.WriteLine("Reading first barcode:");
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }

                        // Generate the second barcode image (Code128) in a new memory stream
                        using (var generator2 = new BarcodeGenerator(EncodeTypes.Code128, "Second456"))
                        {
                            using (var stream2 = new MemoryStream())
                            {
                                generator2.Save(stream2, BarCodeImageFormat.Png);
                                stream2.Position = 0; // Reset stream position for reading

                                // Load the second image into a Bitmap object
                                using (var bitmap2 = new Bitmap(stream2))
                                {
                                    // Replace the bitmap source of the existing reader with the second image
                                    reader.SetBarCodeImage(bitmap2);

                                    Console.WriteLine("Reading after replacing bitmap source:");
                                    foreach (BarCodeResult result in reader.ReadBarCodes())
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
    }
}
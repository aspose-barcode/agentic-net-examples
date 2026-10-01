// Title: Replace barcode image source with SetBarCodeImage to read a different in‑memory barcode
// Description: Demonstrates generating two barcodes in memory, reading the first, then swapping the image source using SetBarCodeImage to read the second barcode without creating a new reader.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator, BarCodeReader, and SetBarCodeImage API classes, which are commonly used to create barcodes, decode them, and efficiently switch image sources when processing multiple in‑memory images. Developers often need this pattern when handling streams of barcode images without repeatedly instantiating readers.
// Prompt: Replace the current bitmap source using SetBarCodeImage to process a different in‑memory image.
// Tags: barcode generation, barcode recognition, setbarcodeimage, in-memory, code128, qr, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates replacing the barcode image source of a <see cref="BarCodeReader"/> using <c>SetBarCodeImage</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcodes in memory, reads the first, then swaps the image source to read the second.
    /// </summary>
    static void Main()
    {
        // Generate the first barcode (Code128) and keep it in memory
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "First"))
        {
            using (var ms1 = new MemoryStream())
            {
                // Save the first barcode as PNG into the memory stream
                generator1.Save(ms1, BarCodeImageFormat.Png);
                ms1.Position = 0; // Reset stream position for reading

                using (var bitmap1 = new Bitmap(ms1))
                {
                    // Create a reader for the first image (Code128)
                    using (var reader = new BarCodeReader(bitmap1, DecodeType.Code128))
                    {
                        Console.WriteLine("Reading first barcode:");
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");
                        }

                        // Generate a second barcode (QR) in memory
                        using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "Second"))
                        {
                            using (var ms2 = new MemoryStream())
                            {
                                // Save the second barcode as PNG into a new memory stream
                                generator2.Save(ms2, BarCodeImageFormat.Png);
                                ms2.Position = 0; // Reset stream position for reading

                                using (var bitmap2 = new Bitmap(ms2))
                                {
                                    // Replace the current image source with the new bitmap
                                    reader.SetBarCodeImage(bitmap2);

                                    Console.WriteLine("Reading second barcode after SetBarCodeImage:");
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"CodeText: {result.CodeText}");
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
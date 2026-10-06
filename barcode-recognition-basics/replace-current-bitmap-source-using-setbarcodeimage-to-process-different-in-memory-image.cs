// Title: Replace barcode image source using SetBarCodeImage
// Description: Demonstrates how to switch the in‑memory bitmap source of a BarCodeReader to read different barcodes without recreating the reader.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcode images, BarCodeReader to decode them, and the SetBarCodeImage method to replace the image source dynamically. Developers working with multiple in‑memory images or streams often need to reuse a single reader instance for efficiency.
// Prompt: Replace the current bitmap source using SetBarCodeImage to process a different in‑memory image.
// Tags: barcode symbology, image source replacement, in-memory bitmap, setbarcodeimage, generation, recognition, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to replace the barcode image source of a BarCodeReader using SetBarCodeImage.
/// </summary>
class Program
{
    /// <summary>
    /// Generates two barcode images in memory and reads them sequentially with a single BarCodeReader instance,
    /// demonstrating the SetBarCodeImage method to change the source image.
    /// </summary>
    static void Main()
    {
        // Generate the first barcode bitmap (Code128, value "12345")
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            using (Bitmap bitmap1 = generator1.GenerateBarCodeImage())
            {
                // Generate the second barcode bitmap (Code128, value "ABCDEF")
                using (var generator2 = new BarcodeGenerator(EncodeTypes.Code128, "ABCDEF"))
                {
                    using (Bitmap bitmap2 = generator2.GenerateBarCodeImage())
                    {
                        // Create a BarCodeReader without an initial image source
                        using (var reader = new BarCodeReader())
                        {
                            // Set the first bitmap as the source and read its barcode(s)
                            reader.SetBarCodeImage(bitmap1);
                            Console.WriteLine("Reading from first bitmap:");
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                            }

                            // Replace the source with the second bitmap and read its barcode(s)
                            reader.SetBarCodeImage(bitmap2);
                            Console.WriteLine("Reading from second bitmap:");
                            foreach (var result in reader.ReadBarCodes())
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
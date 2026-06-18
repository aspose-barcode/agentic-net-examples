using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a rotated Code128 barcode, loading it into a bitmap,
/// and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a rotated barcode, saves it to a memory stream,
    /// loads it into a bitmap, and reads the barcode information.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real scenario, capture a frame from a webcam.
        // For this self‑contained example we generate a rotated barcode image in memory.

        // Create a Code128 barcode with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Rotate the barcode to simulate a tilted barcode in a video frame.
            generator.Parameters.RotationAngle = 45f;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image into an Aspose.Drawing.Bitmap.
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize the reader to detect all supported barcode types.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Iterate over detected barcodes and log orientation angles.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                            Console.WriteLine($"BarCode Text: {result.CodeText}");
                            Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
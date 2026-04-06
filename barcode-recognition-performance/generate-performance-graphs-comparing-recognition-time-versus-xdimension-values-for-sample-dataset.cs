using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample barcode text
        const string codeText = "1234567890";

        // XDimension values to test (in points)
        float[] xDimensions = new float[] { 0.5f, 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f };

        Console.WriteLine("XDimension (pt),RecognitionTime (ms)");

        foreach (float xDim in xDimensions)
        {
            // Generate barcode with specific XDimension
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set XDimension (smallest bar width)
                generator.Parameters.Barcode.XDimension.Point = xDim;

                // Optional: set bar height to a reasonable value
                generator.Parameters.Barcode.BarHeight.Point = 30f;

                // Generate image in memory
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Measure recognition time
                    var stopwatch = Stopwatch.StartNew();

                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        // Perform recognition (read all barcodes)
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // No need to process result; just ensure reading occurs
                        }
                    }

                    stopwatch.Stop();
                    Console.WriteLine($"{xDim},{stopwatch.Elapsed.TotalMilliseconds}");
                }
            }
        }
    }
}
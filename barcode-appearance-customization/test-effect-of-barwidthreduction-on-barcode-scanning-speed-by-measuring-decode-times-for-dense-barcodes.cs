using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a dense Code128 barcode text (100 characters)
        string denseText = new string('A', 100);

        // File names for the two test images
        const string fileNoReduction = "barcode_no_reduction.png";
        const string fileWithReduction = "barcode_with_reduction.png";

        // Generate barcode without BarWidthReduction
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, denseText))
        {
            // Ensure the image size is sufficient for the long text
            generator.Parameters.ImageWidth.Point = 600f;
            generator.Parameters.ImageHeight.Point = 150f;

            // No bar width reduction
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            generator.Save(fileNoReduction);
        }

        // Generate barcode with BarWidthReduction
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, denseText))
        {
            generator.Parameters.ImageWidth.Point = 600f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Apply a small reduction (e.g., 0.5 points)
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.5f;

            generator.Save(fileWithReduction);
        }

        // Measure decoding time for barcode without reduction
        long timeNoReduction = MeasureDecodeTime(fileNoReduction);
        // Measure decoding time for barcode with reduction
        long timeWithReduction = MeasureDecodeTime(fileWithReduction);

        Console.WriteLine($"Decoding time without BarWidthReduction: {timeNoReduction} ms");
        Console.WriteLine($"Decoding time with BarWidthReduction (0.5pt): {timeWithReduction} ms");
    }

    static long MeasureDecodeTime(string imagePath)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Read all barcodes in the image (there should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text to ensure correctness
                Console.WriteLine($"Decoded from {imagePath}: {result.CodeText}");
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}
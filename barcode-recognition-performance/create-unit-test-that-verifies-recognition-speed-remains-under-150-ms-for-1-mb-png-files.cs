using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demo program that generates a large barcode image, saves it to a temporary file,
/// and measures the time required to recognize it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it, reads it back, measures recognition time,
    /// and reports success or failure based on a 150 ms threshold.
    /// </summary>
    static void Main()
    {
        // Define temporary file path for the barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "test_barcode.png");

        // Generate a barcode image large enough to be around 1 MB
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "PerformanceTest1234567890"))
        {
            // Use interpolation mode and set explicit image dimensions (points)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 3000f;   // width in points
            generator.Parameters.ImageHeight.Point = 3000f;  // height in points

            // Save the generated image to the temporary file
            generator.Save(tempFile);
        }

        // Verify that the file was created successfully
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Start timing the barcode recognition process
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Open the saved image for reading and recognize all supported barcode types
        using (var reader = new BarCodeReader(tempFile, DecodeType.AllSupportedTypes))
        {
            // Apply a standard quality preset for recognition
            reader.QualitySettings = QualitySettings.NormalQuality;

            // Iterate through all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                // Output each result to prevent the loop from being optimized away
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // Stop the timer and calculate elapsed milliseconds
        stopwatch.Stop();
        long elapsedMs = stopwatch.ElapsedMilliseconds;

        // Report whether recognition completed within the 150 ms threshold
        if (elapsedMs < 150)
        {
            Console.WriteLine($"Success: Recognition completed in {elapsedMs} ms (under 150 ms).");
        }
        else
        {
            Console.WriteLine($"Failure: Recognition took {elapsedMs} ms (exceeds 150 ms).");
        }

        // Attempt to delete the temporary file; ignore any errors that occur
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Cleanup errors are intentionally ignored
        }
    }
}
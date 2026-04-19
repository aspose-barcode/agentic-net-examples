using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the sample barcode image
        const string imagePath = "sample.png";

        // Generate a simple Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Measure recognition time with HighPerformance preset
        long highPerfTime = MeasureRecognitionTime(imagePath, QualitySettings.HighPerformance);

        // Measure recognition time with MaxQuality preset
        long maxQualityTime = MeasureRecognitionTime(imagePath, QualitySettings.MaxQuality);

        // Output the results
        Console.WriteLine($"HighPerformance preset time: {highPerfTime} ms");
        Console.WriteLine($"MaxQuality preset time: {maxQualityTime} ms");

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }

    // Helper method to measure the elapsed time for barcode recognition using a specific QualitySettings preset
    private static long MeasureRecognitionTime(string imageFile, QualitySettings preset)
    {
        using (var reader = new BarCodeReader(imageFile, DecodeType.Code128))
        {
            reader.QualitySettings = preset;

            var stopwatch = Stopwatch.StartNew();

            // Perform the recognition; iterate to ensure the operation is executed
            foreach (var result in reader.ReadBarCodes())
            {
                // No additional processing needed; just iterate
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
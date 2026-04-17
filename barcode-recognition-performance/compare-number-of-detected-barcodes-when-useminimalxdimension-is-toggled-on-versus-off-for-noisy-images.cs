using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the noisy image. Change as needed.
        const string imagePath = "noisy.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        int countNormal = CountBarcodes(imagePath, XDimensionMode.Normal, minimalXDimension: null);
        int countUseMinimal = CountBarcodes(imagePath, XDimensionMode.UseMinimalXDimension, minimalXDimension: 2);

        Console.WriteLine($"Detected barcodes (Normal XDimension): {countNormal}");
        Console.WriteLine($"Detected barcodes (UseMinimalXDimension, MinimalXDimension=2): {countUseMinimal}");
    }

    private static int CountBarcodes(string imagePath, XDimensionMode mode, int? minimalXDimension)
    {
        using (var reader = new BarCodeReader(imagePath))
        {
            // Use a high-quality preset to improve detection on noisy images.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Set the XDimension mode.
            reader.QualitySettings.XDimension = mode;

            // If the mode requires a minimal XDimension, set it.
            if (minimalXDimension.HasValue)
                reader.QualitySettings.MinimalXDimension = minimalXDimension.Value;

            // Perform recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Return the number of detected barcodes.
            return results?.Length ?? 0;
        }
    }
}
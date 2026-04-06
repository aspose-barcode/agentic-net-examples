using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for generated images
        const string smallImagePath = "small.png";
        const string normalImagePath = "normal.png";

        // Create a barcode with a very small XDimension (1 point)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set image size
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Set small XDimension
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Save the small‑XDimension barcode
            generator.Save(smallImagePath);
        }

        // Create a barcode with default XDimension (auto)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set image size to be the same as the small one
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Do NOT modify XDimension – use default detection mode
            generator.Save(normalImagePath);
        }

        // Detect the small barcode using Small XDimension mode
        bool smallDetectedWithSmallMode = DetectBarcode(smallImagePath, XDimensionMode.Small);
        // Detect the small barcode using default (Auto) mode
        bool smallDetectedWithDefaultMode = DetectBarcode(smallImagePath, XDimensionMode.Auto);

        // Detect the normal barcode using Small XDimension mode (should also succeed)
        bool normalDetectedWithSmallMode = DetectBarcode(normalImagePath, XDimensionMode.Small);
        // Detect the normal barcode using default mode
        bool normalDetectedWithDefaultMode = DetectBarcode(normalImagePath, XDimensionMode.Auto);

        // Output comparison results
        Console.WriteLine("Detection results:");
        Console.WriteLine($"Small barcode with Small XDimension mode: {(smallDetectedWithSmallMode ? "Detected" : "Not detected")}");
        Console.WriteLine($"Small barcode with Default mode: {(smallDetectedWithDefaultMode ? "Detected" : "Not detected")}");
        Console.WriteLine($"Normal barcode with Small XDimension mode: {(normalDetectedWithSmallMode ? "Detected" : "Not detected")}");
        Console.WriteLine($"Normal barcode with Default mode: {(normalDetectedWithDefaultMode ? "Detected" : "Not detected")}");
    }

    // Helper method to read a barcode image with a specific XDimension mode
    static bool DetectBarcode(string imagePath, XDimensionMode mode)
    {
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure recognition quality settings
            reader.QualitySettings = QualitySettings.HighPerformance;
            reader.QualitySettings.XDimension = mode;

            // Perform detection
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // If any result is found, detection succeeded
                if (!string.IsNullOrEmpty(result.CodeText))
                    return true;
            }
        }
        return false;
    }
}
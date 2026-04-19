using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Folder for temporary barcode images
        string folder = Path.Combine(Path.GetTempPath(), "DataMatrixXDimTest");
        Directory.CreateDirectory(folder);

        // XDimension values to test (in pixels)
        float[] xDimensions = { 1f, 3f };
        // Store file paths for later reading
        string[] imagePaths = new string[xDimensions.Length];

        // Generate DataMatrix barcodes with different XDimension settings
        for (int i = 0; i < xDimensions.Length; i++)
        {
            string filePath = Path.Combine(folder, $"datamatrix_x{(int)xDimensions[i]}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Test12345"))
            {
                // Set XDimension in pixels
                generator.Parameters.Barcode.XDimension.Pixels = xDimensions[i];
                generator.Save(filePath);
            }
            imagePaths[i] = filePath;
            Console.WriteLine($"Generated barcode with XDimension={xDimensions[i]}px at {filePath}");
        }

        // Compare detection using Small and Large XDimension modes
        foreach (string imagePath in imagePaths)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            Console.WriteLine($"\nReading image: {Path.GetFileName(imagePath)}");

            // Detect with Small mode (suitable for 1‑pixel XDimension)
            using (var readerSmall = new BarCodeReader(imagePath, DecodeType.DataMatrix))
            {
                readerSmall.QualitySettings.XDimension = XDimensionMode.Small;
                var resultSmall = readerSmall.ReadBarCodes().FirstOrDefault();
                bool detectedSmall = resultSmall != null;
                Console.WriteLine($"  Small mode detection: {(detectedSmall ? "Success" : "Failed")}");
            }

            // Detect with Large mode (suitable for larger XDimension)
            using (var readerLarge = new BarCodeReader(imagePath, DecodeType.DataMatrix))
            {
                readerLarge.QualitySettings.XDimension = XDimensionMode.Large;
                var resultLarge = readerLarge.ReadBarCodes().FirstOrDefault();
                bool detectedLarge = resultLarge != null;
                Console.WriteLine($"  Large mode detection: {(detectedLarge ? "Success" : "Failed")}");
            }
        }

        // Cleanup (optional)
        // Directory.Delete(folder, true);
    }
}
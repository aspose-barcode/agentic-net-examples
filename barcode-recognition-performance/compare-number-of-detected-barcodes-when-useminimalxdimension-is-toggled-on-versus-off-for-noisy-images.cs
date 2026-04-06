using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for temporary images
        string barcodePath = "barcode.png";
        string noisyPath = "noisy.png";

        // Generate a simple barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath);
        }

        // Load the barcode image and add random noise
        using (var bitmap = new Bitmap(barcodePath))
        {
            Random rnd = new Random();
            // Introduce noise by altering a percentage of pixels
            int width = bitmap.Width;
            int height = bitmap.Height;
            int totalPixels = width * height;
            int noisyPixels = totalPixels / 20; // 5% noise

            for (int i = 0; i < noisyPixels; i++)
            {
                int x = rnd.Next(width);
                int y = rnd.Next(height);
                // Set pixel to a random color
                bitmap.SetPixel(x, y, Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            }

            // Save the noisy image
            bitmap.Save(noisyPath);
        }

        // Detect barcodes with default XDimension mode
        int countDefault;
        using (var reader = new BarCodeReader(noisyPath, DecodeType.Code128))
        {
            // Use default quality settings (NormalQuality)
            reader.ReadBarCodes();
            countDefault = reader.FoundCount;
        }

        // Detect barcodes with UseMinimalXDimension mode
        int countUseMinimal;
        using (var reader = new BarCodeReader(noisyPath, DecodeType.Code128))
        {
            // Set XDimension mode to UseMinimalXDimension and define minimal size
            reader.QualitySettings = QualitySettings.NormalQuality;
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1; // minimal X dimension in pixels
            reader.ReadBarCodes();
            countUseMinimal = reader.FoundCount;
        }

        // Output the comparison results
        Console.WriteLine($"Detected barcodes with default XDimension mode: {countDefault}");
        Console.WriteLine($"Detected barcodes with UseMinimalXDimension mode: {countUseMinimal}");
    }
}
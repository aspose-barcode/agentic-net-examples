using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a small DataMatrix barcode with high resolution
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SMALL123"))
        {
            // Set a high DPI to improve detection of small modules
            generator.Parameters.Resolution = 300f; // 300 dpi

            // Reduce XDimension to make the barcode modules small (1 point)
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Optional: set padding to zero to keep the image compact
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode to a bitmap file
            string imagePath = "datamatrix.png";
            generator.Save(imagePath);
        }

        // Load the high‑resolution bitmap and configure the reader for small XDimension
        using (var bitmap = new Bitmap("datamatrix.png"))
        using (var reader = new BarCodeReader())
        {
            // Specify that we want to read DataMatrix codes
            reader.SetBarCodeReadType(DecodeType.DataMatrix);

            // Use a quality preset suitable for high‑resolution small barcodes
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Set XDimension mode to Small to help detection of tiny modules
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Provide the bitmap to the reader
            reader.SetBarCodeImage(bitmap);

            // Perform recognition and output results
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Type: " + result.CodeTypeName);
                Console.WriteLine("Decoded Text : " + result.CodeText);
                Console.WriteLine("Confidence   : " + result.Confidence);
            }
        }
    }
}
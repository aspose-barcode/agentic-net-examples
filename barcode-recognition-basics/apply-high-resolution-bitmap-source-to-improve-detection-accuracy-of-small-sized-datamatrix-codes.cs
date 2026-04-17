using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define a small DataMatrix code text
        const string codeText = "SMALL";

        // Create a DataMatrix barcode generator with high resolution (300 DPI)
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Increase resolution to improve detection of small symbols
            generator.Parameters.Resolution = 300f; // DPI

            // Optionally set image size to ensure enough pixels
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 400f;

            // Generate the barcode image into a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Load the high‑resolution bitmap
                using (var bitmap = (Bitmap)Image.FromStream(ms))
                {
                    // Initialize the barcode reader with high‑quality settings
                    using (var reader = new BarCodeReader(bitmap, DecodeType.DataMatrix))
                    {
                        // Use high‑quality preset and enable small X‑dimension detection
                        reader.QualitySettings = QualitySettings.HighQuality;
                        reader.QualitySettings.XDimension = XDimensionMode.Small;

                        // Read barcodes
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a simple Code128 barcode and store it in a memory stream
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                // Save the barcode image as PNG into the stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position before reading
            ms.Position = 0;

            // Initialize the reader without an image, then assign the stream
            using (var reader = new BarCodeReader())
            {
                // Assign the barcode image stream to the reader
                reader.SetBarCodeImage(ms);

                // Enable try‑harder mode for low‑contrast / challenging lighting
                reader.QualitySettings = QualitySettings.HighQuality;

                // Read and output detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");
                }
            }
        }
    }
}
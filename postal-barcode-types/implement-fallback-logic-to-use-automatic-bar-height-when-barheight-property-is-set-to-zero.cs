using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodeBarHeightFallback
{
    class Program
    {
        static void Main()
        {
            // Sample barcode text
            const string codeText = "1234567890";

            // Desired bar height in pixels (set to 0 to trigger automatic fallback)
            float desiredBarHeight = 0f;

            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Ensure AutoSizeMode is None (default) for 1D barcodes
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Apply bar height only if a positive value is provided
                if (desiredBarHeight > 0f)
                {
                    generator.Parameters.Barcode.BarHeight.Pixels = desiredBarHeight;
                }
                else
                {
                    // BarHeight left at default (automatic) – no explicit assignment
                    // Optionally, you could log or handle this case as needed
                }

                // Save the generated barcode image
                const string outputFile = "barcode.png";
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to {outputFile}");
            }
        }
    }
}
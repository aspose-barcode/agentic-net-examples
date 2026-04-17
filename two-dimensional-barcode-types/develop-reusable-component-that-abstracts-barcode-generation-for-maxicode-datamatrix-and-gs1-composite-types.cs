using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDemo
{
    // Helper class that abstracts barcode generation for MaxiCode, DataMatrix, and GS1 Composite.
    public static class BarcodeHelper
    {
        // Generates a MaxiCode barcode (Mode 2) and saves it as a PNG file.
        public static void GenerateMaxiCode(string outputPath)
        {
            // Create MaxiCode codetext for Mode 2.
            var maxiCodeCodetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",   // 9‑digit US postal code
                CountryCode = 56,           // Country code
                ServiceCategory = 999       // Service category
            };

            // Standard second message.
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Test MaxiCode"
            };
            maxiCodeCodetext.SecondMessage = secondMessage;

            // Generate the barcode image using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                        File.WriteAllBytes(outputPath, stream.ToArray());
                    }
                }
            }
        }

        // Generates a DataMatrix barcode and saves it as a PNG file.
        public static void GenerateDataMatrix(string outputPath)
        {
            const string codeText = "Sample DataMatrix";

            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Set X‑dimension (pixel size of the smallest module).
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Choose a specific DataMatrix version.
                generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_12x12;

                // Save directly to file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        // Generates a GS1 Composite barcode and saves it as a PNG file.
        public static void GenerateGS1Composite(string outputPath)
        {
            // Linear part (GS1‑128) and 2D part separated by '|'.
            const string codetext = "(01)03212345678906|(21)A12345678";

            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                // Linear component type.
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // 2D component type.
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // X‑dimension for both components.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;

                // Height of the linear component.
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Save directly to file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                string maxiCodePath = "maxicode.png";
                string dataMatrixPath = "datamatrix.png";
                string gs1CompositePath = "gs1composite.png";

                BarcodeHelper.GenerateMaxiCode(maxiCodePath);
                Console.WriteLine($"MaxiCode barcode saved to {maxiCodePath}");

                BarcodeHelper.GenerateDataMatrix(dataMatrixPath);
                Console.WriteLine($"DataMatrix barcode saved to {dataMatrixPath}");

                BarcodeHelper.GenerateGS1Composite(gs1CompositePath);
                Console.WriteLine($"GS1 Composite barcode saved to {gs1CompositePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
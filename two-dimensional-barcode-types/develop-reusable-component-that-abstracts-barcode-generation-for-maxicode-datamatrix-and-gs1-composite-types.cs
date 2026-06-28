using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeDemo
{
    /// <summary>
    /// Provides static methods for generating various types of barcodes using Aspose.BarCode.
    /// </summary>
    public static class BarcodeFactory
    {
        /// <summary>
        /// Generates a MaxiCode barcode image.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The file path where the image will be saved.</param>
        public static void GenerateMaxiCode(string codeText, string outputPath)
        {
            // Validate input parameters.
            if (string.IsNullOrWhiteSpace(codeText))
                throw new ArgumentException("Code text cannot be null or empty.", nameof(codeText));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Create a generator for MaxiCode with the specified text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Set high resolution for better image quality.
                generator.Parameters.Resolution = 300f;
                // Use interpolation to automatically size the barcode.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                // Save the generated barcode to the given path.
                generator.Save(outputPath);
            }
        }

        /// <summary>
        /// Generates a DataMatrix barcode image.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The file path where the image will be saved.</param>
        public static void GenerateDataMatrix(string codeText, string outputPath)
        {
            // Validate input parameters.
            if (string.IsNullOrWhiteSpace(codeText))
                throw new ArgumentException("Code text cannot be null or empty.", nameof(codeText));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Create a generator for DataMatrix with the specified text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Set the DataMatrix version (size) to 20x20 modules.
                generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_20x20;
                // Set high resolution for better image quality.
                generator.Parameters.Resolution = 300f;
                // Use interpolation to automatically size the barcode.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                // Save the generated barcode to the given path.
                generator.Save(outputPath);
            }
        }

        /// <summary>
        /// Generates a GS1 Composite barcode image that combines a linear and a 2‑D component.
        /// </summary>
        /// <param name="linearPart">The linear component (e.g., GS1‑128) text.</param>
        /// <param name="twoDPart">The 2‑D component (e.g., DataMatrix) text.</param>
        /// <param name="outputPath">The file path where the image will be saved.</param>
        public static void GenerateGS1Composite(string linearPart, string twoDPart, string outputPath)
        {
            // Validate input parameters.
            if (string.IsNullOrWhiteSpace(linearPart))
                throw new ArgumentException("Linear part cannot be null or empty.", nameof(linearPart));
            if (string.IsNullOrWhiteSpace(twoDPart))
                throw new ArgumentException("2D part cannot be null or empty.", nameof(twoDPart));
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Combine linear and 2‑D parts using the '|' separator required by GS1 Composite.
            string combinedCodeText = $"{linearPart}|{twoDPart}";

            // Create a generator for GS1 Composite with the combined text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
            {
                // Specify that the linear component should be encoded as GS1‑128.
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                // Set high resolution for better image quality.
                generator.Parameters.Resolution = 300f;
                // Disable auto‑sizing; the composite size is defined explicitly.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                // Save the generated barcode to the given path.
                generator.Save(outputPath);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates sample barcodes and demonstrates recognition.
        /// </summary>
        static void Main()
        {
            // Determine the output directory for generated barcode images.
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Generate a MaxiCode barcode.
            string maxiCodePath = Path.Combine(outputDir, "maxicode.png");
            BarcodeFactory.GenerateMaxiCode("Sample MaxiCode Text", maxiCodePath);
            Console.WriteLine($"MaxiCode saved to: {maxiCodePath}");

            // Generate a DataMatrix barcode.
            string dataMatrixPath = Path.Combine(outputDir, "datamatrix.png");
            BarcodeFactory.GenerateDataMatrix("DM1234567890", dataMatrixPath);
            Console.WriteLine($"DataMatrix saved to: {dataMatrixPath}");

            // Generate a GS1 Composite barcode.
            string gs1CompositePath = Path.Combine(outputDir, "gs1composite.png");
            string linear = "(01)01234567890123";
            string twoD = "(21)ABC12345";
            BarcodeFactory.GenerateGS1Composite(linear, twoD, gs1CompositePath);
            Console.WriteLine($"GS1 Composite saved to: {gs1CompositePath}");

            // Read and display barcode information from the generated DataMatrix image.
            using (BarCodeReader reader = new BarCodeReader(dataMatrixPath, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Recognized [{result.CodeTypeName}] CodeText: {result.CodeText}");
                }
            }
        }
    }
}
// Title: Barcode generation demo for MaxiCode, DataMatrix, and GS1 Composite
// Description: Demonstrates how to generate MaxiCode, DataMatrix, and GS1 Composite barcodes using Aspose.BarCode and save them as image files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of ComplexBarcodeGenerator for MaxiCode, BarcodeGenerator for DataMatrix and GS1 Composite symbologies. Developers often need to create various barcode types for packaging, inventory, and shipping; this snippet illustrates key API classes (ComplexBarcodeGenerator, BarcodeGenerator, EncodeTypes) and typical configuration steps for practical implementations.
// Prompt: Develop a reusable component that abstracts barcode generation for MaxiCode, DataMatrix, and GS1 Composite types.
// Tags: barcode, maxicode, datamatrix, gs1 composite, generation, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeDemo
{
    /// <summary>
    /// Provides static methods to generate different barcode types (MaxiCode, DataMatrix, GS1 Composite) and save them to files.
    /// </summary>
    public static class BarcodeFactory
    {
        /// <summary>
        /// Generates a MaxiCode barcode using ComplexBarcodeGenerator and saves it to the specified path.
        /// </summary>
        /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
        public static void GenerateMaxiCode(string outputPath)
        {
            // Prepare MaxiCode codetext (Mode 2 with standard second message).
            var maxiCodeCodetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",
                CountryCode = 56,
                ServiceCategory = 999
            };
            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Sample MaxiCode"
            };
            maxiCodeCodetext.SecondMessage = secondMessage;

            // Generate and save the barcode.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save(outputPath);
            }
        }

        /// <summary>
        /// Generates a DataMatrix barcode using BarcodeGenerator and saves it to the specified path.
        /// </summary>
        /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
        public static void GenerateDataMatrix(string outputPath)
        {
            // Simple DataMatrix with a sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
            {
                // Choose a square ECC200 version.
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
                // Set module size.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(outputPath);
            }
        }

        /// <summary>
        /// Generates a GS1 Composite barcode using BarcodeGenerator and saves it to the specified path.
        /// </summary>
        /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
        public static void GenerateGS1Composite(string outputPath)
        {
            // Linear and 2D parts are separated by the '|' character.
            string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                // Configure linear component type.
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                // Configure 2D component type.
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Set X-Dimension for both components.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                // Set height for the linear component.
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                generator.Save(outputPath);
            }
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application. Creates output directory and generates sample barcodes.
        /// </summary>
        static void Main()
        {
            // Create output directory.
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Generate each barcode type.
            string maxiCodePath = Path.Combine(outputDir, "maxicode.png");
            BarcodeFactory.GenerateMaxiCode(maxiCodePath);
            Console.WriteLine($"MaxiCode saved to: {maxiCodePath}");

            string dataMatrixPath = Path.Combine(outputDir, "datamatrix.png");
            BarcodeFactory.GenerateDataMatrix(dataMatrixPath);
            Console.WriteLine($"DataMatrix saved to: {dataMatrixPath}");

            string gs1CompositePath = Path.Combine(outputDir, "gs1composite.png");
            BarcodeFactory.GenerateGS1Composite(gs1CompositePath);
            Console.WriteLine($"GS1 Composite saved to: {gs1CompositePath}");

            // Program ends successfully.
        }
    }
}
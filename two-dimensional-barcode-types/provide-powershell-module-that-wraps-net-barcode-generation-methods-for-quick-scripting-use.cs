using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeModuleDemo
{
    internal static class BarcodeHelper
    {
        // Generates a barcode image using the specified symbology name, code text and output file path.
        // Returns true on success, false otherwise.
        public static bool GenerateBarcode(string symbologyName, string codeText, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(symbologyName))
                throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));

            if (string.IsNullOrWhiteSpace(codeText))
                throw new ArgumentException("Code text must be provided.", nameof(codeText));

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be provided.", nameof(outputPath));

            // Resolve the EncodeTypes static field that matches the symbology name.
            var encodeTypesType = typeof(EncodeTypes);
            var fieldInfo = encodeTypesType.GetField(symbologyName);
            if (fieldInfo == null)
                throw new ArgumentException($"Symbology '{symbologyName}' is not a valid EncodeTypes member.", nameof(symbologyName));

            var encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

            // Ensure the directory for the output file exists.
            var directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Create and configure the barcode generator.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Basic appearance settings.
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Set resolution (dpi).
                generator.Parameters.Resolution = 300;

                // Use explicit sizing (no auto‑size) to demonstrate unit properties.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set image dimensions.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Set X‑dimension (module width) and bar height for 1D barcodes.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;

                // Configure padding (5 points on each side).
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Hide human‑readable text for demonstration.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Save the barcode image as PNG.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            return true;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Expected arguments: <SymbologyName> <CodeText> <OutputPath>
            // Fallback to safe defaults if arguments are missing.
            string symbology = args.Length > 0 ? args[0] : "Code128";
            string codeText = args.Length > 1 ? args[1] : "1234567890";
            string outputPath = args.Length > 2 ? args[2] : "barcode.png";

            try
            {
                bool success = BarcodeHelper.GenerateBarcode(symbology, codeText, outputPath);
                if (success)
                {
                    Console.WriteLine($"Barcode generated successfully: {Path.GetFullPath(outputPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }
    }
}
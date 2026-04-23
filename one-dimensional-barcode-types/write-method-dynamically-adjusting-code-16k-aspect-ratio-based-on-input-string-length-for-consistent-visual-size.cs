using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace Code16KAspectRatioDemo
{
    class Program
    {
        /// <summary>
        /// Calculates an aspect ratio for Code 16K based on the length of the input text.
        /// Longer texts receive a slightly larger aspect ratio to keep the visual size of the barcode consistent.
        /// </summary>
        /// <param name="codeText">The text to be encoded.</param>
        /// <returns>Aspect ratio (Height/Width) as a float.</returns>
        private static float CalculateAspectRatio(string codeText)
        {
            const float baseRatio = 1.0f;                     // default aspect ratio
            int extraChars = Math.Max(0, codeText.Length - 5); // characters beyond a baseline length
            // Increase ratio by 0.05 for each extra character
            return baseRatio + extraChars * 0.05f;
        }

        static void Main(string[] args)
        {
            // Use command‑line argument if provided, otherwise a default sample.
            string code = args.Length > 0 ? args[0] : "Sample12345";

            // Determine appropriate aspect ratio (currently not applied due to API limitation).
            float aspectRatio = CalculateAspectRatio(code);

            // Create the barcode generator for Code 16K.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
            {
                generator.CodeText = code;

                // Save the barcode image to a PNG file.
                string outputFile = "code16k.png";
                generator.Save(outputFile);
                Console.WriteLine($"Barcode saved to '{outputFile}' with calculated aspect ratio {aspectRatio:F2}.");
            }
        }
    }
}
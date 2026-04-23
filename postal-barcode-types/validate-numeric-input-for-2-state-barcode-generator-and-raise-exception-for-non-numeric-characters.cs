using System;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace NumericBarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Sample inputs: one valid numeric string and one invalid containing a letter.
            string[] inputs = { "123456", "12A456" };

            foreach (string text in inputs)
            {
                try
                {
                    // Validate that the input contains only digits.
                    ValidateNumeric(text);

                    // Create the barcode generator for a 2‑state barcode (Code128 used as example).
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                    {
                        // Enable exception throwing for incorrect code text.
                        generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                        // Set the code text and save the barcode image.
                        generator.CodeText = text;
                        string fileName = $"{text}.png";
                        generator.Save(fileName);
                        Console.WriteLine($"Barcode generated and saved to '{fileName}'.");
                    }
                }
                catch (Exception ex)
                {
                    // Output the error for non‑numeric inputs.
                    Console.WriteLine($"Error for input '{text}': {ex.Message}");
                }
            }
        }

        // Throws an exception if the provided string contains any non‑digit characters.
        static void ValidateNumeric(string code)
        {
            if (string.IsNullOrEmpty(code) || !code.All(char.IsDigit))
            {
                throw new ArgumentException("Code text must be numeric.");
            }
        }
    }
}
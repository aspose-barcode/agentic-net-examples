using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeNumericValidation
{
    class Program
    {
        static void Main()
        {
            // Valid numeric input
            string validCode = "1234567890";
            ValidateNumeric(validCode);

            // Generate barcode for valid input
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, validCode))
            {
                generator.Save("valid_barcode.png");
            }

            // Demonstrate handling of invalid (non‑numeric) input
            try
            {
                string invalidCode = "12AB34";
                ValidateNumeric(invalidCode); // This will throw
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, invalidCode))
                {
                    generator.Save("invalid_barcode.png");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Ensures the code text contains only digits; otherwise throws an exception
        static void ValidateNumeric(string text)
        {
            foreach (char ch in text)
            {
                if (!char.IsDigit(ch))
                {
                    throw new ArgumentException("CodeText must contain only numeric characters.");
                }
            }
        }
    }
}
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace Gs1CompositeExample
{
    class Program
    {
        // Validates that the GS1 Composite codetext contains the required '|' separator.
        static void ValidateGs1CompositeCodeText(string codeText)
        {
            if (string.IsNullOrEmpty(codeText) || !codeText.Contains("|"))
            {
                throw new ArgumentException("GS1 Composite codetext must contain a '|' separator between linear and 2D parts.");
            }
        }

        static void Main()
        {
            // Example of an incorrect codetext (missing '|')
            string incorrectCodeText = "(01)03212345678906(21)A1B2C3D4E5F6G7H8";

            // Example of a correct codetext
            string correctCodeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

            // Attempt to generate barcode with incorrect codetext and handle the validation error.
            try
            {
                ValidateGs1CompositeCodeText(incorrectCodeText);
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, incorrectCodeText))
                {
                    // Configure components (won't be reached because validation fails)
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                    generator.Save("gs1Composite_invalid.png");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (BarCodeException ex)
            {
                Console.WriteLine($"Aspose.BarCode error: {ex.Message}");
            }

            // Generate barcode with correct codetext.
            try
            {
                ValidateGs1CompositeCodeText(correctCodeText);
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, correctCodeText))
                {
                    // Set linear component to GS1 Code128 and 2D component to CC_A (MicroPDF417)
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                    // Example visual settings
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                    // Save the generated barcode image
                    generator.Save("gs1Composite_valid.png");
                }

                Console.WriteLine("GS1 Composite barcode generated successfully: gs1Composite_valid.png");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (BarCodeException ex)
            {
                Console.WriteLine($"Aspose.BarCode error: {ex.Message}");
            }
        }
    }
}
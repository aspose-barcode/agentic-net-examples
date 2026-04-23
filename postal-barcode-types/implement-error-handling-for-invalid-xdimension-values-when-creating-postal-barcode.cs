using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample postal barcode (Postnet) with code text.
        const string codeText = "12345";
        // Desired XDimension in points.
        float xDimensionValue = -2f; // Intentionally invalid for demonstration.

        try
        {
            ValidateXDimension(xDimensionValue);

            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
            {
                // Set a valid XDimension after validation.
                generator.Parameters.Barcode.XDimension.Point = xDimensionValue;

                // Save the generated barcode image.
                string outputPath = "postnet.png";
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (BarCodeException ex)
        {
            // Handles Aspose.BarCode specific errors.
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }

    static void ValidateXDimension(float value)
    {
        // XDimension must be a positive number.
        if (value <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "XDimension must be greater than zero.");
        }
    }
}
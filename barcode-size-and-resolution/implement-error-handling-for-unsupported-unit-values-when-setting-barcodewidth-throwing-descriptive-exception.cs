using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Sets the barcode image width in pixels.
    // Throws ArgumentOutOfRangeException if the value is not positive.
    static void SetBarCodeWidth(BarcodeGenerator generator, float widthInPixels)
    {
        if (widthInPixels <= 0f)
        {
            throw new ArgumentOutOfRangeException(
                nameof(widthInPixels),
                widthInPixels,
                "BarCodeWidth must be a positive value greater than zero.");
        }

        // AutoSizeMode must be set to a mode that respects ImageWidth.
        generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
        generator.Parameters.ImageWidth.Pixels = widthInPixels;
    }

    static void Main()
    {
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Attempt to set a valid width.
                SetBarCodeWidth(generator, 300f);

                // Save the barcode image.
                generator.Save("barcode_valid.png");
                Console.WriteLine("Barcode saved with valid width.");

                // Attempt to set an invalid width to demonstrate error handling.
                SetBarCodeWidth(generator, -50f);
                generator.Save("barcode_invalid.png"); // This line will not be reached.
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (BarCodeException ex)
        {
            // Handles any Aspose.BarCode specific exceptions.
            Console.WriteLine($"Barcode generation error: {ex.Message}");
        }
    }
}
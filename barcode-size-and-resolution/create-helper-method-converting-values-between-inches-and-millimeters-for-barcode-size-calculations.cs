using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Convert inches to millimeters.
    static float InchesToMillimeters(float inches)
    {
        return inches * 25.4f;
    }

    // Convert millimeters to inches.
    static float MillimetersToInches(float millimeters)
    {
        return millimeters / 25.4f;
    }

    static void Main()
    {
        // Example: create a Code128 barcode and set its bar height using conversion helpers.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "1234567890";

            // Desired bar height: 0.5 inches.
            float heightInInches = 0.5f;
            float heightInMillimeters = InchesToMillimeters(heightInInches);
            generator.Parameters.Barcode.BarHeight.Millimeters = heightInMillimeters;

            // Save the barcode image.
            generator.Save("barcode.png");
        }

        // Demonstrate reverse conversion.
        float mm = 30f;
        float inches = MillimetersToInches(mm);
        Console.WriteLine($"{mm} mm = {inches:F2} inches");
    }
}
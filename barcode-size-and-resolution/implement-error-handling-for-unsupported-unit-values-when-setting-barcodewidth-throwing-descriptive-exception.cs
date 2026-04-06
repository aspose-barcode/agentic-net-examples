using System;
using Aspose.BarCode.Generation;

class Program
{
    // Sets the barcode image width using the specified unit.
    // Supported units: "Point", "Pixel", "Inch", "Millimeter".
    // Throws ArgumentException for unsupported unit strings.
    // Throws ArgumentOutOfRangeException for non‑positive values.
    static void SetBarCodeWidth(BarcodeGenerator generator, float value, string unit)
    {
        if (value <= 0f)
            throw new ArgumentOutOfRangeException(nameof(value), "BarCode width must be greater than zero.");

        switch (unit.Trim().ToLowerInvariant())
        {
            case "point":
                generator.Parameters.ImageWidth.Point = value;
                break;
            case "pixel":
                generator.Parameters.ImageWidth.Pixels = value;
                break;
            case "inch":
                generator.Parameters.ImageWidth.Inches = value;
                break;
            case "millimeter":
                generator.Parameters.ImageWidth.Millimeters = value;
                break;
            default:
                throw new ArgumentException($"Unsupported unit '{unit}'. Supported units are Point, Pixel, Inch, Millimeter.", nameof(unit));
        }
    }

    static void Main()
    {
        // Example: valid width setting
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123456";
                SetBarCodeWidth(generator, 200f, "Point"); // valid
                generator.Save("valid_barcode.png");
                Console.WriteLine("Barcode saved with valid width.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during valid barcode generation: {ex.Message}");
        }

        // Example: invalid unit
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "ABCDEF";
                SetBarCodeWidth(generator, 100f, "LightYear"); // unsupported unit
                generator.Save("invalid_unit.png");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught expected exception for unsupported unit: {ex.Message}");
        }

        // Example: invalid (non‑positive) value
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "XYZ123";
                SetBarCodeWidth(generator, -50f, "Pixel"); // non‑positive value
                generator.Save("invalid_value.png");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught expected exception for invalid value: {ex.Message}");
        }
    }
}
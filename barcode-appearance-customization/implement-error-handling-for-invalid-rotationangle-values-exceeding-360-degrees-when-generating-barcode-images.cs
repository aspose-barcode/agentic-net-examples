using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Example rotation angles to test
        float[] angles = { 0f, 90f, 360f, 400f };

        foreach (float angle in angles)
        {
            Console.WriteLine($"Generating barcode with RotationAngle = {angle} degrees.");
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    // Validate rotation angle before applying
                    SetRotationAngle(generator, angle);

                    generator.CodeText = "123456";
                    string fileName = $"barcode_{angle}.png";
                    generator.Save(fileName);
                    Console.WriteLine($"Saved barcode to {fileName}");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.WriteLine();
        }
    }

    // Validates and sets the RotationAngle property.
    // Throws ArgumentOutOfRangeException if the angle is greater than 360 degrees.
    static void SetRotationAngle(BarcodeGenerator generator, float angle)
    {
        if (angle < 0f || angle > 360f)
        {
            throw new ArgumentOutOfRangeException(nameof(angle),
                $"RotationAngle must be between 0 and 360 degrees. Provided value: {angle}");
        }

        generator.Parameters.RotationAngle = angle;
    }
}
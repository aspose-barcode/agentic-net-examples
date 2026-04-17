using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample barcode text
        const string codeText = "1234567890";

        // Example rotation angle (change to test)
        float rotationAngle = 400f; // Invalid angle for demonstration

        // Create the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            try
            {
                // Validate and set the rotation angle
                SetRotationAngle(generator, rotationAngle);
                Console.WriteLine($"Rotation angle set to {rotationAngle} degrees.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Handle invalid angle: log and fallback to default (0 degrees)
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Falling back to default rotation angle (0 degrees).");
                generator.Parameters.RotationAngle = 0f;
            }

            // Save the generated barcode image
            const string outputFile = "barcode.png";
            generator.Save(outputFile);
            Console.WriteLine($"Barcode image saved to '{outputFile}'.");
        }
    }

    // Validates the rotation angle and applies it to the generator
    static void SetRotationAngle(BarcodeGenerator generator, float angle)
    {
        if (angle < 0f || angle > 360f)
        {
            throw new ArgumentOutOfRangeException(nameof(angle), "RotationAngle must be between 0 and 360 degrees.");
        }

        generator.Parameters.RotationAngle = angle;
    }
}
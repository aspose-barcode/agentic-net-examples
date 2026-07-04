// Title: Barcode Generation with Rotation Angle Validation
// Description: Demonstrates generating a Code128 barcode image while validating the rotation angle to ensure it does not exceed 360 degrees.
// Prompt: Implement error handling for invalid RotationAngle values exceeding 360 degrees when generating barcode images.
// Tags: barcode, code128, rotation, validation, image, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a barcode image with a validated rotation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and handles invalid rotation angles.
    /// </summary>
    static void Main()
    {
        // Sample rotation angle (intentionally invalid for demonstration)
        float rotationAngle = 400f;

        try
        {
            // Validate that the rotation angle is within the allowed range
            ValidateRotationAngle(rotationAngle);

            // Create a Code128 barcode generator with the specified data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Apply the validated rotation angle to the barcode parameters
                generator.Parameters.RotationAngle = rotationAngle;

                // Save the generated barcode image to a file
                generator.Save("barcode.png");

                // Inform the user of successful generation
                Console.WriteLine("Barcode generated successfully with rotation angle: " + rotationAngle);
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Output a clear error message when the rotation angle is invalid
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Ensures the rotation angle is within the allowed range [0, 360]
    static void ValidateRotationAngle(float angle)
    {
        if (angle < 0f || angle > 360f)
        {
            // Throw an exception with a descriptive message if the angle is out of bounds
            throw new ArgumentOutOfRangeException(nameof(angle),
                $"RotationAngle must be between 0 and 360 degrees. Provided value: {angle}");
        }
    }
}
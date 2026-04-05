using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Desired canvas size (points)
        float canvasWidth = 300f;
        float canvasHeight = 300f;

        // Create a MaxiCode generator with simple code text
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test"))
        {
            // Set the image size to match the canvas
            generator.Parameters.ImageWidth.Point = canvasWidth;
            generator.Parameters.ImageHeight.Point = canvasHeight;

            // Apply custom padding on all sides (points)
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Rotate the barcode image (degrees)
            generator.Parameters.RotationAngle = 45f;

            // Save the resulting image
            generator.Save("MaxiCodeWithPaddingAndRotation.png");
        }

        Console.WriteLine("MaxiCode barcode generated successfully.");
    }
}
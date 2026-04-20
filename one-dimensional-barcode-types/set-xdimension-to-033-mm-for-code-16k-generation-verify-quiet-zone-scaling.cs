using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Set XDimension to 0.33 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Save the barcode image
            generator.Save("code16k.png");

            // Retrieve quiet zone coefficients
            int leftCoef = generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef;
            int rightCoef = generator.Parameters.Barcode.Code16K.QuietZoneRightCoef;

            // Calculate quiet zone sizes based on XDimension
            float xDim = generator.Parameters.Barcode.XDimension.Millimeters;
            float leftQuietZone = leftCoef * xDim;
            float rightQuietZone = rightCoef * xDim;

            // Output the results
            Console.WriteLine($"XDimension: {xDim} mm");
            Console.WriteLine($"Left Quiet Zone Coefficient: {leftCoef}");
            Console.WriteLine($"Right Quiet Zone Coefficient: {rightCoef}");
            Console.WriteLine($"Calculated Left Quiet Zone: {leftQuietZone} mm");
            Console.WriteLine($"Calculated Right Quiet Zone: {rightQuietZone} mm");
        }
    }
}
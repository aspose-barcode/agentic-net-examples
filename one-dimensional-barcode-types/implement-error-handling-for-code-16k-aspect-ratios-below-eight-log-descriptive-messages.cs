using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Sample aspect ratio that might be invalid (below 8)
        float aspectRatio = 5.5f;

        // Validate aspect ratio for Code16K
        if (aspectRatio < 8f)
        {
            Console.WriteLine($"[Warning] Code16K aspect ratio {aspectRatio} is below the minimum recommended value of 8. It will be adjusted to 8.");
            aspectRatio = 8f;
        }

        // Create barcode generator for Code16K
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
        {
            // Set a simple codetext
            generator.CodeText = "123456789012345678901234567890123456";

            // Apply the (validated) aspect ratio
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

            // Optional: set image size to ensure visibility
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the barcode image
            try
            {
                generator.Save("code16k.png");
                Console.WriteLine("Barcode generated successfully: code16k.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to generate barcode: {ex.Message}");
            }
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    // Generates a MaxiCode barcode with the specified aspect ratio and returns its image dimensions.
    static (int Width, int Height) GenerateMaxiCodeSize(float aspectRatio)
    {
        // Create the generator with a simple codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test MaxiCode"))
        {
            // Set the aspect ratio (Height/Width) for the MaxiCode modules.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = aspectRatio;

            // Save the barcode to a memory stream in PNG format.
            using (var stream = new MemoryStream())
            {
                generator.Save(stream, BarCodeImageFormat.Png);
                stream.Position = 0;

                // Load the image from the stream to obtain its pixel dimensions.
                using (var image = Image.FromStream(stream))
                {
                    return (image.Width, image.Height);
                }
            }
        }
    }

    static void Main()
    {
        // Default aspect ratio (typically 1.0).
        var defaultSize = GenerateMaxiCodeSize(1.0f);
        // Increased aspect ratio (e.g., 2.0 makes modules taller).
        var tallSize = GenerateMaxiCodeSize(2.0f);
        // Decreased aspect ratio (e.g., 0.5 makes modules wider).
        var wideSize = GenerateMaxiCodeSize(0.5f);

        // Compute height/width ratios for each image.
        float defaultRatio = (float)defaultSize.Height / defaultSize.Width;
        float tallRatio = (float)tallSize.Height / tallSize.Width;
        float wideRatio = (float)wideSize.Height / wideSize.Width;

        // Output the dimensions and ratios.
        Console.WriteLine($"Default (1.0)  - Width: {defaultSize.Width}, Height: {defaultSize.Height}, Ratio: {defaultRatio:F3}");
        Console.WriteLine($"Tall    (2.0)  - Width: {tallSize.Width}, Height: {tallSize.Height}, Ratio: {tallRatio:F3}");
        Console.WriteLine($"Wide    (0.5)  - Width: {wideSize.Width}, Height: {wideSize.Height}, Ratio: {wideRatio:F3}");

        // Simple verification: changing the aspect ratio should affect the image's height/width ratio.
        bool tallIncreased = tallRatio > defaultRatio;
        bool wideDecreased = wideRatio < defaultRatio;

        if (tallIncreased && wideDecreased)
        {
            Console.WriteLine("Aspect ratio adjustments affect MaxiCode dimensions as expected.");
        }
        else
        {
            Console.WriteLine("Aspect ratio adjustments did NOT affect dimensions as expected.");
        }
    }
}
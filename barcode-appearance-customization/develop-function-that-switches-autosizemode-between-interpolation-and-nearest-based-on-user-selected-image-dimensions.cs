using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Default dimensions (points)
        float defaultWidth = 300f;
        float defaultHeight = 150f;

        // Parse command‑line arguments if provided
        float width = defaultWidth;
        float height = defaultHeight;

        if (args.Length >= 2)
        {
            if (!float.TryParse(args[0], out width))
            {
                Console.WriteLine($"Invalid width '{args[0]}', using default {defaultWidth}.");
                width = defaultWidth;
            }
            if (!float.TryParse(args[1], out height))
            {
                Console.WriteLine($"Invalid height '{args[1]}', using default {defaultHeight}.");
                height = defaultHeight;
            }
        }
        else
        {
            Console.WriteLine("Width and height not supplied, using default dimensions.");
        }

        // Decide AutoSizeMode based on dimensions
        // Use Interpolation for larger images, Nearest for smaller ones
        AutoSizeMode mode = (width >= 300f && height >= 150f) ? AutoSizeMode.Interpolation : AutoSizeMode.Nearest;

        // Create barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply chosen AutoSizeMode
            generator.Parameters.AutoSizeMode = mode;

            // Set target image size
            generator.Parameters.ImageWidth.Point = width;
            generator.Parameters.ImageHeight.Point = height;

            // Save the barcode image
            generator.Save("barcode.png");
        }

        Console.WriteLine($"Barcode generated with AutoSizeMode.{mode} at size {width}x{height} points.");
    }
}
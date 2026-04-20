using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample codetext – Code16K accepts any string; using a simple example.
        const string codeText = "SampleCode16K";

        // Create the barcode generator for Code16K symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the aspect ratio of the Code16K modules to 9 (height/width).
            generator.Parameters.Barcode.Code16K.AspectRatio = 9f;

            // Optional: set image resolution (dpi) if desired.
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG image.
            generator.Save("code16k.png");
        }

        Console.WriteLine("Code16K barcode saved as code16k.png");
    }
}
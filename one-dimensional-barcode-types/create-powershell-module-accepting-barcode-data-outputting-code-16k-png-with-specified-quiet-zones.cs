using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Default values
        string codeText = "1234567890";
        int leftQuietZone = 10;   // default as per documentation
        int rightQuietZone = 1;   // default as per documentation

        // Parse command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && int.TryParse(args[1], out int leftCoef))
            leftQuietZone = leftCoef;

        if (args.Length > 2 && int.TryParse(args[2], out int rightCoef))
            rightQuietZone = rightCoef;

        // Create the barcode generator for Code 16K
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftQuietZone;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightQuietZone;

            // Optional: set a reasonable XDimension to control image size
            generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points per module

            // Save as PNG
            string outputFile = "code16k.png";
            generator.Save(outputFile);
            Console.WriteLine($"Code 16K barcode saved to '{outputFile}'.");
        }
    }
}
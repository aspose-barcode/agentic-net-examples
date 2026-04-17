using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Codetext containing characters not supported by the default ISO-8859-1 encoding.
        string codetext = "漢字";

        try
        {
            // Create a DotCode generator with Auto encoding mode (default).
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codetext))
            {
                // Explicitly set the encode mode to Auto for clarity.
                generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Auto;

                // Attempt to save the barcode image.
                generator.Save("dotcode.png");
                Console.WriteLine("Barcode generated successfully.");
            }
        }
        catch (InvalidCodeException ex)
        {
            // Handle unsupported characters specific to DotCode Auto mode.
            Console.WriteLine($"Unsupported character error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.WriteLine($"Generation failed: {ex.Message}");
        }
    }
}
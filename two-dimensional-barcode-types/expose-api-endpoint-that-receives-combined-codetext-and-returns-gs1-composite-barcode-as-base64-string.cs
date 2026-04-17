using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulated request payload: combined CodeText for GS1 Composite barcode
        string linearPart = "(01)03212345678906";
        string twoDPart = "(21)A1B2C3D4E5F6G7H8";
        string combinedCodeText = $"{linearPart}|{twoDPart}";

        // Create barcode generator for GS1 Composite Bar
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Configure linear and 2D component types
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set dimensions (using float literals with 'f')
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Generate image into a memory stream as PNG
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();

                // Convert image bytes to Base64 string (simulated API response)
                string base64Result = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64Result);
            }
        }
    }
}
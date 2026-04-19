using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare common barcode settings
        const string codeText = "1234567890";
        var encodeType = EncodeTypes.Code128;

        // Generate barcode with visible code text (default location is Below)
        byte[] imageWithText;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Ensure the code text is visible
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageWithText = ms.ToArray();
            }
        }

        // Generate barcode with hidden code text
        byte[] imageWithoutText;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Hide the code text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageWithoutText = ms.ToArray();
            }
        }

        // Simple verification: the two images must differ in size (text adds pixels)
        if (imageWithText.Length == imageWithoutText.Length)
        {
            Console.WriteLine("Test Failed: Images have the same size, text visibility may not have changed.");
        }
        else
        {
            Console.WriteLine("Test Passed: Images differ, indicating code text visibility toggled correctly.");
        }

        // Additional check: ensure the location property reflects the intended setting
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            if (generator.Parameters.Barcode.CodeTextParameters.Location != CodeLocation.Below)
            {
                Console.WriteLine("Test Failed: CodeTextParameters.Location did not retain the set value (Below).");
            }
            else
            {
                Console.WriteLine("Location set to Below verified.");
            }

            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            if (generator.Parameters.Barcode.CodeTextParameters.Location != CodeLocation.None)
            {
                Console.WriteLine("Test Failed: CodeTextParameters.Location did not retain the set value (None).");
            }
            else
            {
                Console.WriteLine("Location set to None verified.");
            }
        }
    }
}
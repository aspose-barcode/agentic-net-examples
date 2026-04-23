using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // 8‑digit numeric string to encode
        string codeText = "12345678";

        // OneCode requires length of 20, 25, 29, or 31 digits
        int[] validLengths = { 20, 25, 29, 31 };
        if (Array.IndexOf(validLengths, codeText.Length) < 0)
        {
            Console.WriteLine("Error: OneCode barcode requires numeric codetext of length 20, 25, 29, or 31 digits.");
            return;
        }

        // Generate the OneCode barcode with default settings
        using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Save the barcode image to a file
            generator.Save("onecode.png");
        }

        Console.WriteLine("OneCode barcode generated successfully.");
    }
}
using System;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample alphanumeric input
        string input = "Abc123XYZ";

        // Validate that the input contains only letters and digits
        if (!Regex.IsMatch(input, @"^[A-Za-z0-9]+$"))
        {
            throw new ArgumentException("Input must be alphanumeric (letters and digits only).");
        }

        // Convert to uppercase before encoding
        string validatedInput = input.ToUpperInvariant();

        // Generate a linear Code128 barcode (suitable for plain alphanumeric data)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = validatedInput;
            // Save the barcode image to a PNG file
            generator.Save("code128.png");
        }

        Console.WriteLine("Barcode generated successfully.");
    }
}
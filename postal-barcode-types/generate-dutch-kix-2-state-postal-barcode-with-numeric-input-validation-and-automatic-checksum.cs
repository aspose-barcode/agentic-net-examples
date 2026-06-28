using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Dutch KIX barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode from a numeric string and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Sample numeric input; replace with your own value or command‑line argument.
        string input = "1234567890123"; // 13 digits; checksum will be added automatically.

        // Validate that the input contains only digits.
        if (!IsNumeric(input))
        {
            Console.WriteLine("Error: Code text must be numeric.");
            return;
        }

        // Define the symbology for Dutch KIX.
        BaseEncodeType symbology = EncodeTypes.DutchKIX;

        // Output file path.
        string outputPath = "kix.png";

        // Generate the barcode within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(symbology, input))
        {
            // Enable automatic checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Show the checksum digit in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Dutch KIX barcode saved to {Path.GetFullPath(outputPath)}");
    }

    // Helper method to ensure the string consists only of digits.
    static bool IsNumeric(string s)
    {
        foreach (char c in s)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}
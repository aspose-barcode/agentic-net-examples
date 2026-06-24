using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Australia Post barcodes with different
/// <see cref="CustomerInformationInterpretingType"/> values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes for each test case and prints results.
    /// </summary>
    static void Main()
    {
        // Define test cases: each tuple contains the encoding type, the text to encode, and the output file name.
        var testCases = new (CustomerInformationInterpretingType Encoding, string CodeText, string FileName)[]
        {
            (CustomerInformationInterpretingType.CTable, "ABC123XYZ", "AustraliaPost_CTable.png"),
            (CustomerInformationInterpretingType.NTable, "1234567890", "AustraliaPost_NTable.png"),
            (CustomerInformationInterpretingType.Other, "12", "AustraliaPost_Other.png")
        };

        // Iterate over each test case and attempt to generate the corresponding barcode.
        foreach (var (encoding, codeText, fileName) in testCases)
        {
            try
            {
                // Create a barcode generator for the Australia Post symbology with the specified code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
                {
                    // Apply the selected customer information interpreting type (alternative encoding table).
                    generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = encoding;

                    // Set a white background to make the generated images easier to view.
                    generator.Parameters.BackColor = Color.White;

                    // Save the generated barcode image to the specified file.
                    generator.Save(fileName);

                    // Write details about the successful generation to the console.
                    Console.WriteLine($"Generated {fileName}");
                    Console.WriteLine($"  Encoding type : {encoding}");
                    Console.WriteLine($"  CodeText length: {codeText.Length}");
                    Console.WriteLine($"  CodeText value : {codeText}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                // If generation fails (e.g., invalid code text for the chosen encoding), report the error.
                Console.WriteLine($"Failed to generate {fileName} with encoding {encoding}");
                Console.WriteLine($"  Reason: {ex.Message}");
                Console.WriteLine();
            }
        }

        // Provide a brief assessment of how each encoding type impacts data capacity.
        Console.WriteLine("Capacity Impact Assessment:");
        Console.WriteLine("- CTable allows alphanumeric characters, enabling longer mixed strings.");
        Console.WriteLine("- NTable restricts to digits only, limiting the character set but still supports long numeric strings.");
        Console.WriteLine("- Other permits only a few symbols (0‑3 characters), resulting in the smallest capacity.");
    }
}
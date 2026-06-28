using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode from the source code of this file
/// and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads this source file, truncates its content, creates a barcode, and saves it.
    /// </summary>
    static void Main()
    {
        // Determine the full path to the current source file (Program.cs)
        string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Program.cs");

        // Verify that the source file exists before attempting to read it
        if (!File.Exists(sourcePath))
        {
            Console.WriteLine("Source file not found: " + sourcePath);
            return;
        }

        // Read the entire content of the source file into a string
        string codeContent = File.ReadAllText(sourcePath);

        // Limit the text length to a reasonable size for barcode generation
        const int maxLength = 100;
        if (codeContent.Length > maxLength)
        {
            // Truncate the string to the maximum allowed length
            codeContent = codeContent.Substring(0, maxLength);
        }

        // Define the output path for the generated barcode image (barcode.png)
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "barcode.png");

        // Create a barcode generator using the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Assign the (possibly truncated) source code as the barcode text
            generator.CodeText = codeContent;

            // Configure image dimensions and disable automatic sizing
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.ImageWidth.Point = 300f;   // Width in points
            generator.Parameters.ImageHeight.Point = 150f;  // Height in points

            // Save the generated barcode as a PNG file at the specified location
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine("Barcode image generated at: " + outputPath);
    }
}
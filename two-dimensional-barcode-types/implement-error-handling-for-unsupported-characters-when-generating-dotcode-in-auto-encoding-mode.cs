using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DotCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample codetext containing an unsupported character (emoji) for the default ISO-8859-1 encoding.
        string codeText = "Test😀DotCode";

        // Output file path (temporary folder).
        string outputPath = Path.Combine(Path.GetTempPath(), "dotcode.png");

        try
        {
            // Create the barcode generator for DotCode with the provided codetext.
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Ensure Auto encoding mode is used (default, but set explicitly for clarity).
                generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Auto;

                // Save the barcode image. This will throw if the codetext contains characters
                // not supported by the selected ECI encoding (ISO-8859-1 by default).
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user that the barcode was generated successfully.
            Console.WriteLine($"Barcode generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Handle unsupported character errors (or any other generation errors).
            Console.WriteLine("Error generating DotCode barcode:");
            Console.WriteLine(ex.Message);
        }
    }
}
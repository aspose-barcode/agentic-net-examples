using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a OneCode barcode with a sample numeric codetext,
    /// configures high‑resolution settings, saves the image, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample OneCode numeric codetext (20 digits)
        string codeText = "12345678901234567890";

        // Initialize the barcode generator for OneCode symbology with the provided codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Configure the generator to use a high‑resolution of 300 DPI
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a high‑resolution PNG file
            generator.Save("OneCode.png");
        }

        // Output a confirmation message to the console
        Console.WriteLine("OneCode barcode generated and saved as OneCode.png");
    }
}
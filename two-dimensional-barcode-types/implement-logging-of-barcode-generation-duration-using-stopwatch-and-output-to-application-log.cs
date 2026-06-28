using System;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image using Aspose.BarCode and measures execution time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it to a file, and logs the elapsed time.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "barcode.png";

        // Start a stopwatch to measure how long the barcode generation takes.
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Create a BarcodeGenerator instance for Code128 encoding with the data "123456".
        // The using statement ensures the generator is disposed properly after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the generated barcode image to the specified output path.
            generator.Save(outputPath);
        }

        // Stop the stopwatch now that barcode generation is complete.
        stopwatch.Stop();

        // Output the elapsed time in milliseconds to the console.
        Console.WriteLine($"Barcode generation completed in {stopwatch.ElapsedMilliseconds} ms.");

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a Swiss Post Parcel barcode.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode (Swiss Post Parcel identifier)
        string codeText = "1234567890123456789012345678901234567890";

        // Determine the full path for the output PNG file in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostParcel.png");

        // Initialize the barcode generator with the specific encode type and data
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Persist the generated barcode image to disk in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}
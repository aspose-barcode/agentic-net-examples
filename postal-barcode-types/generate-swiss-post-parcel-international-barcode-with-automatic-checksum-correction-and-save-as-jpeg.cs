using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the codetext for the Swiss Post Parcel barcode.
        // Replace this sample value with a valid Swiss Post Parcel code as needed.
        string codeText = "123456789012345678901234567890";

        // Define the output file path for the generated barcode image.
        string outputPath = "SwissPostParcel.jpg";

        // Initialize the barcode generator with the Swiss Post Parcel symbology and the provided codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            // Configure the generator to ignore codetext validation errors (no exception will be thrown).
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}
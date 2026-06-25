using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, writes it to a file, and reports success.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Specify the data to encode in the barcode.
            generator.CodeText = "1234567890";

            // Define the destination file path for the generated image.
            string outputPath = "barcode.png";

            // Create a file stream to write the barcode image to disk.
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Save the barcode image to the stream in PNG format.
                generator.Save(fileStream, BarCodeImageFormat.Png);
                // The using block ensures the file stream is properly disposed.
            }
            // The using block ensures the barcode generator is disposed.
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine("Barcode image saved successfully.");
    }
}
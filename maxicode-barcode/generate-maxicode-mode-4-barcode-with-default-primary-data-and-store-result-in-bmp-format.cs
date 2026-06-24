using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode in Mode 4 using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode Mode 4 barcode and saves it as a BMP file.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the current working directory)
        string outputPath = "maxicode_mode4.bmp";

        // Prepare the MaxiCode codetext with Mode 4 and a sample message
        var maxiCodeCodetext = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample message"
        };

        // Attempt to generate the barcode and write it to disk
        try
        {
            // Initialize the complex barcode generator with the prepared codetext
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Save the generated barcode as a BMP image to the specified path
                generator.Save(outputPath, BarCodeImageFormat.Bmp);
            }

            // Output the full path of the saved image for user confirmation
            Console.WriteLine($"MaxiCode Mode 4 barcode saved to: {Path.GetFullPath(outputPath)}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during barcode generation or file saving
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}
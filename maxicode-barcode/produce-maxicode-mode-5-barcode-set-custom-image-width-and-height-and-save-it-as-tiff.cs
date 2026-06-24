using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode (Mode 5) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode barcode, configures its size,
    /// saves it to a TIFF file, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize MaxiCode data with Mode 5 and a sample message.
        var maxiCode = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode5,
            Message = "Sample MaxiCode Mode 5"
        };

        // Create a barcode generator for the specified MaxiCode data.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Set the desired image dimensions (in points).
            generator.Parameters.ImageWidth.Point = 300f;   // Width of the barcode image
            generator.Parameters.ImageHeight.Point = 200f;  // Height of the barcode image

            // Save the generated barcode to a TIFF file.
            generator.Save("MaxiCodeMode5.tiff");
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine("MaxiCode Mode 5 barcode saved as MaxiCodeMode5.tiff");
    }
}
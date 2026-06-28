using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating an RM4SCC barcode using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with RM4SCC symbology and the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, "ABCD1234"))
        {
            // Configure the barcode to have hollow (non-filled) bars.
            generator.Parameters.Barcode.FilledBars = false;

            // Save the generated barcode image to a PNG file.
            generator.Save("rm4scc.png");

            // Notify the user that the file has been saved.
            Console.WriteLine("RM4SCC barcode saved to rm4scc.png");
        } // The using block disposes the generator and releases resources.
    }
}
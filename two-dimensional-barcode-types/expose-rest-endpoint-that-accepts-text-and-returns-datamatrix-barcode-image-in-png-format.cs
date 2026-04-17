using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // The original requirement was to expose a REST endpoint.
        // Due to the execution constraints (no long‑running services or listeners),
        // a REST service cannot be hosted in this console application.
        // Instead, we provide a simple command‑line fallback that generates a DataMatrix
        // barcode image from the supplied text and saves it as a PNG file.

        string text = args.Length > 0 ? args[0] : "SampleText";

        try
        {
            // Create the barcode generator for DataMatrix with the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                // Save the barcode directly to a PNG file.
                const string outputFile = "datamatrix.png";
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"DataMatrix barcode generated and saved to '{outputFile}'.");
            }
        }
        catch (Exception ex)
        {
            // Gracefully report any errors (e.g., missing Aspose.BarCode license).
            Console.Error.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }
}
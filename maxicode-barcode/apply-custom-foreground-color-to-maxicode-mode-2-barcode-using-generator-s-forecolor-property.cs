// Title: Apply custom foreground color to a MaxiCode Mode 2 barcode
// Description: Demonstrates generating a MaxiCode Mode 2 barcode and setting a custom foreground color using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode2, configuring barcode parameters such as color, mode, and module size. Developers working with high‑density 2‑D barcodes (e.g., shipping labels) often need to customize appearance and output format, making this pattern a common reference.
// Prompt: Apply a custom foreground color to a MaxiCode Mode 2 barcode using the generator's ForeColor property.
// Tags: maxicode, color, png, complexbarcode, generator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode Mode 2 barcode, applies a custom foreground color, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, builds MaxiCode data, configures the generator,
    /// and writes the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "MaxiCodeMode2.png");

        // Build MaxiCode Mode 2 codetext (required 9‑digit postal code)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9 digits required for Mode 2
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Optional: add a structured second message (address lines, year, etc.)
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99;
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set custom foreground (bar) color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Explicitly set MaxiCode mode to Mode2 (default for this codetext type)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Optional: adjust the size of each module (pixel density)
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine("MaxiCode barcode saved to: " + outputPath);
    }
}
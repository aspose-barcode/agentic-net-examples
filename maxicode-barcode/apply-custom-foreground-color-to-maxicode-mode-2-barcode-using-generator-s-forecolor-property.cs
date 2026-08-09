// Title: Custom foreground color for MaxiCode Mode 2 barcode
// Description: Demonstrates generating a MaxiCode Mode 2 barcode and applying a custom foreground (bar) color using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode2, configuring barcode appearance via the Parameters.Barcode.BarColor property, and saving the result as an image. Developers working with high‑density 2‑D barcodes such as MaxiCode often need to customize visual attributes for branding or readability, making this pattern a common starting point.
// Prompt: Apply a custom foreground color to a MaxiCode Mode 2 barcode using the generator's ForeColor property.
// Tags: maxicode, barcode, color, foreground, generation, aspose.barcode, complexbarcode, png

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Generates a MaxiCode Mode 2 barcode with a custom foreground color and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode data, configures the generator, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode Mode 2 codetext with sample values.
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample message" }
        };

        // Initialize the complex barcode generator using the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Set a custom foreground (bar) color for the barcode.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;

            // Generate the barcode image.
            using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the generated image to a PNG file.
                image.Save("MaxiCodeMode2_Red.png");
            }
        }
    }
}
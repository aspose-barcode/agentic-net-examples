// Title: Apply custom foreground color to a MaxiCode Mode 2 barcode
// Description: This example creates a MaxiCode Mode 2 barcode, sets a custom bar (foreground) color, and saves it as a PNG image. It demonstrates how to customize the visual appearance of complex barcodes using Aspose.BarCode.
// Category-Description: The sample belongs to the Aspose.BarCode complex barcode generation category, where developers work with multi‑message symbologies such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related parameter classes to configure barcode data and appearance. Typical scenarios include shipping labels, parcel tracking, and logistics applications that require colored MaxiCode symbols.
// Prompt: Apply a custom foreground color to a MaxiCode Mode 2 barcode using the generator's ForeColor property.
// Tags: maxicode, color, generation, png, aspose.barcode, complexbarcodegenerator, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates applying a custom foreground color to a MaxiCode Mode 2 barcode and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the MaxiCode data, configures the barcode color, generates the image, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode Mode 2 codetext with required fields
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // USA numeric country code
            ServiceCategory = 999       // Example service category
        };

        // Create and assign the standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Initialize the generator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set a custom foreground (bar) color for the barcode
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Generate the barcode image in memory
            generator.GenerateBarCodeImage();

            // Define output file path and save the image as PNG
            const string outputPath = "maxicode_mode2.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}
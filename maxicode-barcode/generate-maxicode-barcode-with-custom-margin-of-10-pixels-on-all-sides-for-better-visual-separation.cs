// Title: Generate MaxiCode barcode with custom margins
// Description: Demonstrates how to create a MaxiCode barcode (Mode 2) and apply a 10‑pixel margin on all sides for visual separation.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode2 and MaxiCodeStandardSecondMessage to produce a MaxiCode symbol. Developers commonly need to customize padding, colors, and output formats when integrating MaxiCode into packaging or shipping labels.
// Prompt: Generate a MaxiCode barcode with a custom margin of 10 pixels on all sides for better visual separation.
// Tags: maxicode, generate, png, complexbarcodegenerator, maxicodecodetextmode2, maxicodestandardsecondmessage

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode barcode with a 10‑pixel margin on each side
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext (Mode 2 with a standard second message)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };

        // Define the optional second message displayed beneath the MaxiCode symbol
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Generate the barcode with custom padding (10 pixels on each side)
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Apply 10‑pixel margins using the Padding properties
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optional: set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}
// Title: Generate MaxiCode barcode with structured secondary message
// Description: Demonstrates how to create a MaxiCode barcode (Mode 2) that includes a structured secondary message containing recipient name, street, and city, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator to produce a MaxiCode symbol. Typical use cases include shipping labels and logistics where a structured secondary message conveys address details. Developers often need to configure postal information, service categories, and visual appearance when generating such barcodes.
/// Prompt: Generate a MaxiCode barcode with a structured secondary message containing recipient name, street, and city fields.
/// Tags: maxicode, generate, png, complexbarcodegenerator, maxicodecodetextmode2, maxicodestructuredsecondmessage

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode barcode with a structured secondary message and saves it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds the secondary message, configures the MaxiCode payload,
    /// generates the barcode, and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Build a structured secondary message containing recipient details.
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("John Doe");          // Recipient name
        secondMessage.Add("123 Main St");       // Street address
        secondMessage.Add("Anytown");           // City

        // Configure the MaxiCode payload (Mode 2) with required postal data and the secondary message.
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit postal code required for Mode 2
            CountryCode = 56,           // Example country code
            ServiceCategory = 999,      // Example service category
            SecondMessage = secondMessage
        };

        // Generate the MaxiCode barcode and save it as a PNG image.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Optional visual settings: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            string outputPath = "maxicode.png";
            generator.Save(outputPath);
            Console.WriteLine($"MaxiCode barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}
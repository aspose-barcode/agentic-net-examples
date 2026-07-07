// Title: Generate MaxiCode barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode (Mode 2) barcode that includes a structured secondary message containing recipient name, street, and city.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and MaxiCodeStructuredSecondMessage classes to encode both primary and secondary data. Developers often need to generate shipping labels or logistics barcodes where additional address information is embedded in the secondary message.
// Prompt: Generate a MaxiCode barcode with a structured secondary message containing recipient name, street, and city fields.
// Tags: maxicode, complex barcode, secondary message, shipping label, aspnet.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a structured secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "maxicode.png";

        // Prepare MaxiCode codetext for Mode 2, including required primary fields
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "123456789", // 9‑digit postal code required for Mode 2
            CountryCode = 840,        // USA numeric country code
            ServiceCategory = 999     // Example service category
        };

        // Build the structured secondary message with recipient address details
        var secondaryMessage = new MaxiCodeStructuredSecondMessage();
        secondaryMessage.Add("John Doe");      // Recipient name
        secondaryMessage.Add("123 Main St");   // Street address
        secondaryMessage.Add("Anytown");       // City

        // Assign the secondary message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = secondaryMessage;

        // Generate the MaxiCode barcode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (Image barcodeImage = generator.GenerateBarCodeImage())
            {
                // Save the generated barcode image as a PNG file
                barcodeImage.Save(outputPath, ImageFormat.Png);
            }
        }

        // Output the full path of the saved barcode image to the console
        Console.WriteLine($"MaxiCode barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}
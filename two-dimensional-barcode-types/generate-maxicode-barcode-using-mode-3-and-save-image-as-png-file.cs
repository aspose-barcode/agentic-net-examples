// Title: Generate MaxiCode Mode 3 Barcode and Save as PNG
// Description: Demonstrates creating a MaxiCode barcode in mode 3 using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode3, a common scenario for shipping and logistics applications where MaxiCode symbols are required. Developers often need to configure postal codes, country codes, and service categories before rendering the barcode to an image format.
// Prompt: Generate a MaxiCode barcode using mode 3 and save the image as PNG file.
// Tags: maxicode, barcode, generation, png, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode barcode (mode 3) and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize MaxiCode codetext for mode 3
        var maxiCodeCodetext = new MaxiCodeCodetextMode3();

        // Set required fields: postal code (6 alphanumeric characters)
        maxiCodeCodetext.PostalCode = "B1050";

        // Set 3‑digit country code
        maxiCodeCodetext.CountryCode = 56;

        // Set 3‑digit service category
        maxiCodeCodetext.ServiceCategory = 999;

        // Create and assign a standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the barcode using ComplexBarcodeGenerator and save as PNG
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // The file extension determines the output image format (PNG)
            generator.Save("maxicode.png");
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("MaxiCode barcode (mode 3) saved as maxicode.png");
    }
}
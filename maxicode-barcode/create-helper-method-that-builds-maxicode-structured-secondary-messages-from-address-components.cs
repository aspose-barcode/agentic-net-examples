// Title: Build MaxiCode Structured Secondary Message Helper
// Description: Demonstrates creating a MaxiCode barcode with a structured secondary message built from address components.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to encode postal information and a custom secondary message. Developers often need to generate MaxiCode barcodes for shipping and logistics, requiring precise formatting of address data and service categories.
// Prompt: Create a helper method that builds MaxiCode structured secondary messages from address components.
// Tags: maxicode, structured secondary message, barcode generation, aspnet, aspose.barcode, complexbarcode, helper method

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates building a MaxiCode barcode with a structured secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Builds a structured secondary message for MaxiCode from address components.
    /// </summary>
    /// <param name="line1">First address line (e.g., street).</param>
    /// <param name="line2">Second address line (e.g., city).</param>
    /// <param name="state">State abbreviation.</param>
    /// <param name="year">Two‑digit year value.</param>
    /// <returns>A populated <see cref="MaxiCodeStructuredSecondMessage"/> instance.</returns>
    static MaxiCodeStructuredSecondMessage BuildStructuredSecondMessage(string line1, string line2, string state, int year)
    {
        // Create a new structured message container.
        var message = new MaxiCodeStructuredSecondMessage();

        // Add address components to the message in the required order.
        message.Add(line1);
        message.Add(line2);
        message.Add(state);

        // Set the year field.
        message.Year = year;

        return message;
    }

    /// <summary>
    /// Entry point. Generates a MaxiCode barcode using address components and saves it as an image.
    /// </summary>
    static void Main()
    {
        // Sample address components.
        string street = "634 ALPHA DRIVE";
        string city = "PITTSBURGH";
        string state = "PA";
        int year = 99;

        // Configure MaxiCode codetext for Mode 2 (postal code, country, service category).
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code.
            CountryCode = 056,          // USA numeric country code.
            ServiceCategory = 999       // Example service category.
        };

        // Assign the structured secondary message built from the address components.
        maxiCodeCodetext.SecondMessage = BuildStructuredSecondMessage(street, city, state, year);

        // Generate the MaxiCode barcode and save it as a PNG image.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save("maxicode.png");
        }

        Console.WriteLine("MaxiCode barcode generated: maxicode.png");
    }
}
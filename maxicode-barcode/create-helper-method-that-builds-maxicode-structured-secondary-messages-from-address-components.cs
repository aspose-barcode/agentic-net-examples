// Title: Generate MaxiCode Mode 2 barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode and populating its structured secondary message using address components.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode category, showcasing how to work with MaxiCode symbology. It uses the MaxiCodeCodetextMode2 and MaxiCodeStructuredSecondMessage classes to set postal information, service category, and a structured secondary message. Developers often need to generate shipping labels or tracking barcodes that include detailed address data, and this snippet illustrates the typical workflow for such use cases.
// Prompt: Create a helper method that builds MaxiCode structured secondary messages from address components.
// Tags: maxicode, barcode, structured secondary message, aspose.barcode, complexbarcode, codetext, c#

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with a structured secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, builds the secondary message,
    /// configures the MaxiCode codetext, generates the barcode image, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory for the generated barcode image.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string filePath = Path.Combine(outputDir, "MaxiCodeMode2Structured.png");

        // Build a structured secondary message from address lines and a year value.
        MaxiCodeStructuredSecondMessage secondMessage = BuildStructuredSecondMessage(
            new string[] { "634 ALPHA DRIVE", "PITTSBURGH", "PA" }, 99);

        // Configure the MaxiCode codetext with postal code, country code, service category, and the secondary message.
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Generate the barcode using the configured codetext and save it to a PNG file.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(filePath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {filePath}");
    }

    /// <summary>
    /// Helper method that creates a <see cref="MaxiCodeStructuredSecondMessage"/> from an array of address lines
    /// and a year value. Each line is added to the message, and the year property is set.
    /// </summary>
    /// <param name="lines">Array of address components (e.g., street, city, state).</param>
    /// <param name="year">Two‑digit year to include in the structured message.</param>
    /// <returns>A populated <see cref="MaxiCodeStructuredSecondMessage"/> instance.</returns>
    static MaxiCodeStructuredSecondMessage BuildStructuredSecondMessage(string[] lines, int year)
    {
        var message = new MaxiCodeStructuredSecondMessage();

        // Add each address line to the structured message.
        foreach (var line in lines)
        {
            message.Add(line);
        }

        // Set the year component of the structured message.
        message.Year = year;
        return message;
    }
}
// Title: Generate MaxiCode barcode with structured secondary message
// Description: Demonstrates building a MaxiCode structured secondary message from address components and generating a Mode 2 MaxiCode barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator to create postal‑oriented MaxiCode symbols. Developers working with shipping, logistics, or postal automation frequently need to encode address data and secondary messages in MaxiCode barcodes; this snippet illustrates the typical workflow and key API classes for that scenario.
// Prompt: Create a helper method that builds MaxiCode structured secondary messages from address components.
// Tags: maxicode, structured secondary message, barcode generation, aspose.barcode, complexbarcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that builds a MaxiCode structured secondary message and generates a Mode 2 MaxiCode barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Builds a structured second message for MaxiCode from address components.
    /// </summary>
    /// <param name="addressLines">Array of address lines (at least one required).</param>
    /// <param name="city">City name.</param>
    /// <param name="state">State abbreviation.</param>
    /// <param name="year">Two‑digit year (0‑99).</param>
    /// <returns>A populated <see cref="MaxiCodeStructuredSecondMessage"/> instance.</returns>
    static MaxiCodeStructuredSecondMessage BuildStructuredSecondMessage(string[] addressLines, string city, string state, int year)
    {
        // Validate input parameters
        if (addressLines == null) throw new ArgumentNullException(nameof(addressLines));
        if (addressLines.Length == 0) throw new ArgumentException("At least one address line is required.", nameof(addressLines));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.", nameof(city));
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State is required.", nameof(state));
        if (year < 0 || year > 99) throw new ArgumentOutOfRangeException(nameof(year), "Year must be a two‑digit value (0‑99).");

        var structuredMessage = new MaxiCodeStructuredSecondMessage();

        // Add each address line to the structured message
        foreach (var line in addressLines)
        {
            structuredMessage.Add(line);
        }

        // Append city and state
        structuredMessage.Add(city);
        structuredMessage.Add(state);

        // Set the two‑digit year field
        structuredMessage.Year = year;

        return structuredMessage;
    }

    /// <summary>
    /// Entry point of the program. Generates a MaxiCode barcode image and writes its size to the console.
    /// </summary>
    static void Main()
    {
        // Sample address components used to build the secondary message
        string[] addressLines = { "634 ALPHA DRIVE" };
        string city = "PITTSBURGH";
        string state = "PA";
        int year = 99;

        // Configure MaxiCode codetext for Mode 2 (USA postal code)
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = BuildStructuredSecondMessage(addressLines, city, state, year)
        };

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            generator.GenerateBarCodeImage();

            // Save the generated image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated MaxiCode barcode image size: {ms.Length} bytes");
            }
        }

        // Program terminates without waiting for user input
    }
}
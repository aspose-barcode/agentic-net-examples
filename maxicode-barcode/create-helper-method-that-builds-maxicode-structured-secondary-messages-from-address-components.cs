using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates building a MaxiCode structured secondary message and generating a MaxiCode barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Builds a structured secondary message for MaxiCode from address components.
    /// </summary>
    /// <param name="addressLines">List of address lines (max 3 lines are typical for MaxiCode).</param>
    /// <param name="city">City name.</param>
    /// <param name="state">State abbreviation.</param>
    /// <param name="year">Two‑digit year (0‑99).</param>
    /// <returns>A <see cref="MaxiCodeStructuredSecondMessage"/> populated with the provided data.</returns>
    static MaxiCodeStructuredSecondMessage BuildMaxiCodeStructuredMessage(
        IList<string> addressLines,
        string city,
        string state,
        int year)
    {
        // Validate input parameters.
        if (addressLines == null) throw new ArgumentNullException(nameof(addressLines));
        if (addressLines.Count == 0) throw new ArgumentException("At least one address line is required.", nameof(addressLines));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.", nameof(city));
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State is required.", nameof(state));
        if (year < 0 || year > 99) throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 0 and 99.");

        var secondMessage = new MaxiCodeStructuredSecondMessage();

        // Add address lines.
        foreach (var line in addressLines)
        {
            secondMessage.Add(line);
        }

        // Add city, state and year as separate identifiers.
        secondMessage.Add(city);
        secondMessage.Add(state);
        secondMessage.Year = year;

        return secondMessage;
    }

    /// <summary>
    /// Entry point of the program. Generates a MaxiCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample address components.
        var addressLines = new List<string>
        {
            "634 ALPHA DRIVE",
            "PITTSBURGH"
        };
        string city = "PA";
        string state = "US";
        int year = 99; // Two‑digit year.

        // Build the structured second message using the helper method.
        var structuredMessage = BuildMaxiCodeStructuredMessage(addressLines, city, state, year);

        // Configure MaxiCode codetext (Mode 2 example).
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code.
            CountryCode = 056,          // Example country code.
            ServiceCategory = 999,      // Example service category.
            SecondMessage = structuredMessage
        };

        // Generate the MaxiCode barcode using ComplexBarcodeGenerator.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Optional: set a human‑readable text displayed below the barcode.
            complexGenerator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Sample MaxiCode";

            // Generate the barcode image.
            using (var image = complexGenerator.GenerateBarCodeImage())
            {
                // Save the image as PNG.
                image.Save("maxicode.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("MaxiCode barcode generated: maxicode.png");
    }
}
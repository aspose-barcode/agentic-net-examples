// Title: Generate MaxiCode Mode 2 barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode and adding a structured secondary message built from address components.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode generation category. It shows how to use MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator to produce a MaxiCode barcode with custom secondary data. Developers working with shipping, logistics, or inventory systems often need to embed address information in MaxiCode symbols; this snippet illustrates the typical workflow and key API classes for that purpose.
// Prompt: Create a helper method that builds MaxiCode structured secondary messages from address components.
// Tags: maxicode, barcode, structured secondary message, aspose.barcode, complexbarcode, generation, c#

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with a structured secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, builds a structured secondary message,
    /// configures MaxiCode codetext, generates the barcode image, and writes the file path to console.
    /// </summary>
    static void Main()
    {
        // Determine a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Build a structured secondary message from address components.
        var secondMessage = BuildStructuredSecondMessage(
            "634 ALPHA DRIVE",
            "PITTSBURGH",
            "PA",
            99);

        // Configure the MaxiCode codetext with postal code, country code, service category, and the secondary message.
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = secondMessage
        };

        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode2Structured.png");

        // Generate the barcode and save it to the specified file.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }

    /// <summary>
    /// Helper method that creates a <see cref="MaxiCodeStructuredSecondMessage"/> from address lines,
    /// state abbreviation, and a year value.
    /// </summary>
    /// <param name="line1">First address line (e.g., street address).</param>
    /// <param name="line2">Second address line (e.g., city).</param>
    /// <param name="state">State or region abbreviation.</param>
    /// <param name="year">Year component to include in the message.</param>
    /// <returns>A populated <see cref="MaxiCodeStructuredSecondMessage"/> instance.</returns>
    static MaxiCodeStructuredSecondMessage BuildStructuredSecondMessage(string line1, string line2, string state, int year)
    {
        var message = new MaxiCodeStructuredSecondMessage();

        // Add non‑empty address components to the message.
        if (!string.IsNullOrEmpty(line1))
            message.Add(line1);
        if (!string.IsNullOrEmpty(line2))
            message.Add(line2);
        if (!string.IsNullOrEmpty(state))
            message.Add(state);

        // Set the year field.
        message.Year = year;

        return message;
    }
}
// Title: Generate MaxiCode Mode 2 barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode and populating its structured secondary message using address components.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to encode postal information and a structured secondary message. Developers working with shipping, logistics, or retail labeling often need to generate MaxiCode barcodes with detailed address data, and this snippet provides a clear pattern for doing so.
// Prompt: Create a helper method that builds MaxiCode structured secondary messages from address components.
// Tags: maxicode, barcode, complexbarcode, structuredmessage, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with a structured secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system's temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode2Structured.png");

        // Create MaxiCode codetext for Mode 2 with postal data and a structured secondary message
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = BuildStructuredSecondMessage(
                "634 ALPHA DRIVE",
                "PITTSBURGH",
                "PA",
                99)
        };

        // Generate the barcode image and save it to the specified path
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Builds a MaxiCode structured secondary message from address components.
    /// </summary>
    /// <param name="addressLine1">First line of the address.</param>
    /// <param name="addressLine2">Second line of the address.</param>
    /// <param name="cityState">City and state combined.</param>
    /// <param name="year">Two‑digit year (0‑99).</param>
    /// <returns>A populated <see cref="MaxiCodeStructuredSecondMessage"/> instance.</returns>
    static MaxiCodeStructuredSecondMessage BuildStructuredSecondMessage(
        string addressLine1,
        string addressLine2,
        string cityState,
        int year)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(addressLine1))
            throw new ArgumentException("Address line 1 cannot be null or empty.", nameof(addressLine1));
        if (string.IsNullOrEmpty(addressLine2))
            throw new ArgumentException("Address line 2 cannot be null or empty.", nameof(addressLine2));
        if (string.IsNullOrEmpty(cityState))
            throw new ArgumentException("City/State cannot be null or empty.", nameof(cityState));
        if (year < 0 || year > 99)
            throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 0 and 99.");

        // Populate the structured second message with address components
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add(addressLine1);
        secondMessage.Add(addressLine2);
        secondMessage.Add(cityState);
        secondMessage.Year = year;
        return secondMessage;
    }
}
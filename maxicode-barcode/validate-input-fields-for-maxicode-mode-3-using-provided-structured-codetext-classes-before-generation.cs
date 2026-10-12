// Title: Generate MaxiCode Mode 3 Barcode with Input Validation
// Description: Demonstrates how to validate MaxiCode Mode 3 fields using structured codetext classes before generating a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode3, MaxiCodeStandardSecondMessage, and MaxiCodeStructuredSecondMessage classes. Typical scenarios include preparing shipping labels or tracking codes where MaxiCode Mode 3 is required, and developers often need to ensure input data complies with the specification before rendering the barcode.
// Prompt: Validate input fields for MaxiCode Mode 3 using the provided structured codetext classes before generation.
// Tags: barcode, maxicode, mode3, validation, generation, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that validates and generates a MaxiCode Mode 3 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Constructs codetext, validates it, generates a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Construct MaxiCode Mode 3 codetext with a standard second message
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Second message" }
        };

        try
        {
            // Validate all required fields before barcode generation
            ValidateMaxiCodeMode3(maxiCodeCodetext);

            // Determine a temporary file path for the generated PNG image
            string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeMode3.png");

            // Generate the barcode using the complex barcode generator and save it as PNG
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user where the barcode image was saved
            Console.WriteLine($"MaxiCode Mode 3 barcode generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any validation or generation errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates the properties of a MaxiCodeCodetextMode3 instance according to the specification.
    /// </summary>
    /// <param name="codetext">The codetext object to validate.</param>
    static void ValidateMaxiCodeMode3(MaxiCodeCodetextMode3 codetext)
    {
        if (codetext == null)
            throw new ArgumentNullException(nameof(codetext));

        // PostalCode must be a non‑empty string
        if (string.IsNullOrWhiteSpace(codetext.PostalCode))
            throw new ArgumentException("PostalCode must be a non-empty string.", nameof(codetext.PostalCode));

        // CountryCode must be within the allowed range 0‑999
        if (codetext.CountryCode < 0 || codetext.CountryCode > 999)
            throw new ArgumentOutOfRangeException(nameof(codetext.CountryCode), "CountryCode must be between 0 and 999.");

        // ServiceCategory must be within the allowed range 0‑999
        if (codetext.ServiceCategory < 0 || codetext.ServiceCategory > 999)
            throw new ArgumentOutOfRangeException(nameof(codetext.ServiceCategory), "ServiceCategory must be between 0 and 999.");

        // SecondMessage must be provided
        if (codetext.SecondMessage == null)
            throw new ArgumentException("SecondMessage must be provided.", nameof(codetext.SecondMessage));

        // Validate standard second message
        if (codetext.SecondMessage is MaxiCodeStandardSecondMessage standardMsg)
        {
            if (string.IsNullOrWhiteSpace(standardMsg.Message))
                throw new ArgumentException("Standard second message cannot be empty.", nameof(standardMsg.Message));
        }
        // Validate structured second message
        else if (codetext.SecondMessage is MaxiCodeStructuredSecondMessage structuredMsg)
        {
            if (structuredMsg.Identifiers == null || structuredMsg.Identifiers.Count == 0)
                throw new ArgumentException("Structured second message must contain at least one line.", nameof(structuredMsg));
        }
        else
        {
            // Any other message type is unsupported for this example
            throw new ArgumentException("Unsupported second message type.", nameof(codetext.SecondMessage));
        }
    }
}
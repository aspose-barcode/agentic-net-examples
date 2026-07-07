// Title: Validate MaxiCode Mode 3 Input Fields Using Structured Codetext Classes
// Description: Demonstrates how to validate the required fields of a MaxiCode Mode 3 codetext object before generating the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related second‑message classes to prepare and validate data before rendering. Developers working with shipping or logistics barcodes often need to ensure data conforms to MaxiCode specifications, making validation a common prerequisite.
// Prompt: Validate input fields for MaxiCode Mode 3 using the provided structured codetext classes before generation.
// Tags: maxicode, validation, image, complexbarcodegenerator, codetext, aspnet, csharp

using System;
using System.Text.RegularExpressions;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that validates MaxiCode Mode 3 data and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds a MaxiCode Mode 3 codetext, validates it,
    /// and generates a PNG image using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare sample data for MaxiCode Mode 3
        // ------------------------------------------------------------
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050A",          // 6 alphanumeric characters
            CountryCode = 56,               // 3‑digit numeric country code
            ServiceCategory = 999           // 3‑digit service category
        };

        // Optional standard second message (e.g., additional textual information)
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        codetext.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // Validate the codetext before attempting barcode generation
        // ------------------------------------------------------------
        try
        {
            ValidateMaxiCodeMode3(codetext);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            return; // Abort if validation fails
        }

        // ------------------------------------------------------------
        // Generate the barcode image and save it to disk
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.GenerateBarCodeImage();   // Render the barcode
            generator.Save("maxicode_mode3.png"); // Save as PNG
        }

        Console.WriteLine("MaxiCode Mode 3 barcode generated successfully.");
    }

    /// <summary>
    /// Validates the fields of a MaxiCodeCodetextMode3 instance according to
    /// MaxiCode Mode 3 specifications, including optional second‑message validation.
    /// </summary>
    /// <param name="codetext">The codetext object to validate.</param>
    static void ValidateMaxiCodeMode3(MaxiCodeCodetextMode3 codetext)
    {
        if (codetext == null)
            throw new ArgumentException("Codetext object cannot be null.");

        // PostalCode: exactly 6 alphanumeric characters
        if (string.IsNullOrEmpty(codetext.PostalCode) ||
            codetext.PostalCode.Length != 6 ||
            !Regex.IsMatch(codetext.PostalCode, @"^[A-Za-z0-9]{6}$"))
        {
            throw new ArgumentException("PostalCode must be exactly 6 alphanumeric characters for MaxiCode Mode 3.");
        }

        // CountryCode: numeric value between 0 and 999 (inclusive)
        if (codetext.CountryCode < 0 || codetext.CountryCode > 999)
        {
            throw new ArgumentException("CountryCode must be a 3‑digit integer between 0 and 999.");
        }

        // ServiceCategory: numeric value between 0 and 999 (inclusive)
        if (codetext.ServiceCategory < 0 || codetext.ServiceCategory > 999)
        {
            throw new ArgumentException("ServiceCategory must be a 3‑digit integer between 0 and 999.");
        }

        // Validate second message if it is provided
        if (codetext.SecondMessage != null)
        {
            if (codetext.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
            {
                // Standard second message must contain non‑empty text
                if (string.IsNullOrWhiteSpace(stdMsg.Message))
                    throw new ArgumentException("Standard second message must contain non‑empty text.");
            }
            else if (codetext.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
            {
                // Structured second message must have at least one identifier
                if (structMsg.Identifiers == null || structMsg.Identifiers.Count == 0)
                    throw new ArgumentException("Structured second message must contain at least one identifier.");
            }
            else
            {
                // Any other type is not supported in this example
                throw new ArgumentException("Unsupported second message type.");
            }
        }
    }
}
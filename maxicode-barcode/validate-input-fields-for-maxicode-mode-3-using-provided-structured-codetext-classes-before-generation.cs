// Title: Validate and Generate MaxiCode Mode 3 Barcode with Structured Second Message
// Description: Demonstrates how to validate input fields for MaxiCode Mode 3 using structured codetext classes before generating the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related structured message classes to create a valid MaxiCode barcode. Developers working with shipping, logistics, or inventory systems often need to validate and generate MaxiCode barcodes that include postal codes, country codes, service categories, and structured address information.
// Prompt: Validate input fields for MaxiCode Mode 3 using the provided structured codetext classes before generation.
// Tags: maxicode, validation, generation, png, complexbarcodegenerator, maxicodecodetextmode3, maxicodestructuredsecondmessage

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that validates MaxiCode Mode 3 codetext fields and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares data, validates it, and creates a MaxiCode Mode 3 barcode.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3.png");

        // Create and populate MaxiCode codetext for Mode 3
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build a structured second message (address lines and year)
        var structuredSecondMessage = new MaxiCodeStructuredSecondMessage();
        structuredSecondMessage.Add("634 ALPHA DRIVE");
        structuredSecondMessage.Add("PITTSBURGH");
        structuredSecondMessage.Add("PA");
        structuredSecondMessage.Year = 99;
        maxiCodeCodetext.SecondMessage = structuredSecondMessage;

        // Validate the populated codetext; abort if validation fails
        try
        {
            ValidateMaxiCodeMode3(maxiCodeCodetext);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Validation error: " + ex.Message);
            return;
        }

        // Generate the barcode image and save it to the output path
        try
        {
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save(outputPath);
            }
            Console.WriteLine("MaxiCode Mode 3 barcode generated at:");
            Console.WriteLine(outputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Barcode generation failed: " + ex.Message);
        }
    }

    /// <summary>
    /// Validates the fields of a MaxiCodeCodetextMode3 instance according to MaxiCode specifications.
    /// </summary>
    /// <param name="codetext">The codetext object to validate.</param>
    static void ValidateMaxiCodeMode3(MaxiCodeCodetextMode3 codetext)
    {
        if (codetext == null)
            throw new ArgumentException("Codetext object cannot be null.");

        if (string.IsNullOrWhiteSpace(codetext.PostalCode))
            throw new ArgumentException("PostalCode must be provided.");

        if (codetext.CountryCode < 0 || codetext.CountryCode > 999)
            throw new ArgumentException("CountryCode must be between 0 and 999.");

        if (codetext.ServiceCategory < 0)
            throw new ArgumentException("ServiceCategory must be non‑negative.");

        if (codetext.SecondMessage == null)
            throw new ArgumentException("SecondMessage must be set.");

        // Additional validation for structured second messages
        if (codetext.SecondMessage is MaxiCodeStructuredSecondMessage structured)
        {
            // Example validation: Year must be within 0‑99
            if (structured.Year < 0 || structured.Year > 99)
                throw new ArgumentException("Year in structured second message must be between 0 and 99.");
        }
        else if (codetext.SecondMessage is MaxiCodeStandardSecondMessage standard)
        {
            if (string.IsNullOrWhiteSpace(standard.Message))
                throw new ArgumentException("Standard second message text must be provided.");
        }
        else
        {
            throw new ArgumentException("Unsupported type of SecondMessage.");
        }
    }
}
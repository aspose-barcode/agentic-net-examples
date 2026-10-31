// Title: Validate MaxiCode Mode 3 Codetext Before Barcode Generation
// Description: Demonstrates how to validate the fields of a MaxiCode Mode 3 codetext object, including structured and standard second messages, before generating the barcode with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related second‑message classes. Developers often need to ensure codetext correctness prior to rendering to avoid runtime errors and to meet industry standards for shipping and logistics applications.
// Prompt: Validate input fields for MaxiCode Mode 3 using the provided structured codetext classes before generation.
// Tags: maxicode, barcode, validation, generation, aspose.barcode, complexbarcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates validation and generation of a MaxiCode Mode 3 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a MaxiCode Mode 3 codetext, validates it, and generates a PNG barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare sample data for MaxiCode Mode 3
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Choose an unstructured second message and assign it
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample second message"
        };
        codetext.SecondMessage = secondMessage;

        // Validate the codetext fields before attempting barcode generation
        try
        {
            ValidateMaxiCodeMode3(codetext);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            return;
        }

        // Define output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCodeMode3.png");

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Optional: throw an exception if the codetext is incorrect during generation
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
            generator.Save(outputPath);
        }

        Console.WriteLine($"MaxiCode Mode 3 barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Validates the required fields of a MaxiCodeCodetextMode3 instance.
    /// Throws <see cref="ArgumentException"/> when any validation rule fails.
    /// </summary>
    /// <param name="codetext">The MaxiCode codetext to validate.</param>
    static void ValidateMaxiCodeMode3(MaxiCodeCodetextMode3 codetext)
    {
        if (codetext == null)
            throw new ArgumentException("Codetext object cannot be null.");

        if (string.IsNullOrWhiteSpace(codetext.PostalCode))
            throw new ArgumentException("PostalCode must be provided.");

        if (codetext.CountryCode < 0 || codetext.CountryCode > 999)
            throw new ArgumentException("CountryCode must be between 0 and 999.");

        if (codetext.ServiceCategory < 0 || codetext.ServiceCategory > 999)
            throw new ArgumentException("ServiceCategory must be between 0 and 999.");

        if (codetext.SecondMessage == null)
            throw new ArgumentException("SecondMessage must be provided.");

        // If the second message is structured, ensure it contains at least one identifier line
        if (codetext.SecondMessage is MaxiCodeStructuredSecondMessage structured)
        {
            if (structured.Identifiers == null || structured.Identifiers.Count == 0)
                throw new ArgumentException("Structured second message must contain at least one line.");
        }
        // If the second message is standard, ensure the message text is not empty
        else if (codetext.SecondMessage is MaxiCodeStandardSecondMessage standard)
        {
            if (string.IsNullOrWhiteSpace(standard.Message))
                throw new ArgumentException("Standard second message cannot be empty.");
        }
        else
        {
            throw new ArgumentException("Unsupported second message type.");
        }
    }
}
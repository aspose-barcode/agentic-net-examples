// Title: Validate MaxiCode Mode 3 Input and Generate PNG Barcode
// Description: Demonstrates how to validate required fields for MaxiCode Mode 3 using Aspose.BarCode's structured codetext classes, then generate a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode3 and related second‑message classes, a common scenario for developers needing to ensure data integrity before creating shipping or logistics barcodes. Typical use cases include validating postal codes, country codes, and service categories prior to barcode rendering.
// Prompt: Validate input fields for MaxiCode Mode 3 using the provided structured codetext classes before generation.
// Tags: maxicode, validation, generation, png, complexbarcodegenerator, maxicodecodetextmode3, maxicodestandardsecondmessage, maxicodestructuredsecondmessage

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates validation of MaxiCode Mode 3 data and barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Validates the fields required for MaxiCode Mode 3.
    /// Throws <see cref="ArgumentException"/> if any field is invalid.
    /// </summary>
    /// <param name="data">The MaxiCode codetext object containing mode‑3 data.</param>
    static void ValidateMaxiCodeMode3(MaxiCodeCodetextMode3 data)
    {
        if (data == null)
            throw new ArgumentException("MaxiCode data object cannot be null.");

        // PostalCode must be exactly 6 alphanumeric characters.
        if (string.IsNullOrEmpty(data.PostalCode) ||
            data.PostalCode.Length != 6 ||
            !Regex.IsMatch(data.PostalCode, @"^[A-Za-z0-9]{6}$"))
        {
            throw new ArgumentException("PostalCode must be exactly 6 alphanumeric characters for MaxiCode Mode 3.");
        }

        // CountryCode must be a three‑digit number (0‑999).
        if (data.CountryCode < 0 || data.CountryCode > 999)
        {
            throw new ArgumentException("CountryCode must be between 0 and 999.");
        }

        // ServiceCategory must be a three‑digit number (0‑999).
        if (data.ServiceCategory < 0 || data.ServiceCategory > 999)
        {
            throw new ArgumentException("ServiceCategory must be between 0 and 999.");
        }

        // Optional: if a second message is supplied, ensure it is of a supported type.
        if (data.SecondMessage != null &&
            !(data.SecondMessage is MaxiCodeStandardSecondMessage) &&
            !(data.SecondMessage is MaxiCodeStructuredSecondMessage))
        {
            throw new ArgumentException("SecondMessage must be either MaxiCodeStandardSecondMessage or MaxiCodeStructuredSecondMessage.");
        }
    }

    /// <summary>
    /// Entry point of the example. Creates sample data, validates it, and generates a MaxiCode Mode 3 PNG barcode.
    /// </summary>
    static void Main()
    {
        // Sample valid data for MaxiCode Mode 3.
        var maxiCodeData = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050A", // 6 alphanumeric characters
            CountryCode = 56,      // example country code
            ServiceCategory = 999 // example service category
        };

        // Optional: add a standard second message.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        maxiCodeData.SecondMessage = secondMessage;

        try
        {
            // Perform manual validation before generation.
            ValidateMaxiCodeMode3(maxiCodeData);

            // Generate the barcode using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
            {
                // Save the image to a PNG file in the current directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode_mode3.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"MaxiCode Mode 3 barcode saved to: {outputPath}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors from the Aspose library.
            Console.WriteLine($"An error occurred during barcode generation: {ex.Message}");
        }
    }
}
// Title: Set Code 16K Quiet Zone Coefficients and Export as JPEG
// Description: Demonstrates how to configure quiet‑zone coefficients for a Code 16K barcode and save it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as quiet‑zone coefficients. Developers often need to adjust quiet zones for scanner compatibility or layout requirements, and then export the barcode to common image formats like JPEG.
// Prompt: Set Code 16K left quiet zone coefficient 0.5 and right coefficient 0.7, export JPEG.
// Tags: code16k, quietzone, jpeg, aspose.barcode, barcode generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code 16K barcode, demonstrates handling of quiet‑zone coefficient constraints,
/// and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures quiet‑zone coefficients (if valid) and exports the barcode.
    /// </summary>
    static void Main()
    {
        // Desired quiet‑zone coefficients (the task requests fractional values)
        float leftCoefRequested = 0.5f;
        float rightCoefRequested = 0.7f;

        // Code16K quiet‑zone coefficients are integer properties.
        // If non‑integer values are supplied we cannot assign them.
        // Inform the user and retain default coefficients.
        if (leftCoefRequested % 1 != 0 || rightCoefRequested % 1 != 0)
        {
            Console.WriteLine("Code16K quiet‑zone coefficients must be integers. Using default values.");
        }

        // Sample Code16K barcode text (any valid string for Code16K)
        string codeText = "12345678901234567890";

        // Create the generator for Code16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set integer quiet‑zone coefficients only if they are whole numbers.
            // In this example we keep defaults because the requested values are fractional.
            // Example of setting integer values:
            // generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 1;
            // generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 2;

            // Export the barcode as JPEG
            generator.Save("code16k.jpg");
        }

        Console.WriteLine("Barcode generated: code16k.jpg");
    }
}
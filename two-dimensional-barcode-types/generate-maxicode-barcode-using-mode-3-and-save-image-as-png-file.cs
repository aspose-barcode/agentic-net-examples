// Title: Generate MaxiCode barcode (Mode 3) and save as PNG
// Description: Demonstrates creating a MaxiCode barcode using mode 3 with Aspose.BarCode, then saving the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to use the ComplexBarcodeGenerator with MaxiCodeCodetextMode3, MaxiCodeStandardSecondMessage, and related classes. Typical use cases include encoding shipping information for UPS and other logistics providers. Developers often need to generate MaxiCode symbols for parcel tracking and require guidance on setting postal code, country code, service category, and secondary messages.
// Prompt: Generate a MaxiCode barcode using mode 3 and save the image as PNG file.
// Tags: maxicode, mode3, barcode generation, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode barcode in mode 3 and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, builds the MaxiCode payload,
    /// generates the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3.png");

        // Build the MaxiCode codetext for mode 3
        MaxiCodeCodetextMode3 codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",   // Postal code component
            CountryCode = 56,       // Numeric country identifier
            ServiceCategory = 999   // Service category identifier
        };

        // Create a standard second message (optional additional data)
        MaxiCodeStandardSecondMessage secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Second message"
        };
        codetext.SecondMessage = secondMessage;

        // Generate the barcode and save it to the specified file
        try
        {
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
            {
                generator.Save(outputPath);
                Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating MaxiCode barcode: {ex.Message}");
        }
    }
}
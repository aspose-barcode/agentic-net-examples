// Title: Generate Swiss Post Parcel additional service code barcode (Code 128) with human‑readable text
// Description: Demonstrates how to create a Swiss Post Parcel additional service code barcode using Aspose.BarCode. The example configures human‑readable text and saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on specialized symbologies such as Swiss Post Parcel (Code 128). It shows how to set barcode parameters, customize text appearance, and export the image. Developers working with postal services, logistics, or custom barcode requirements can use these patterns to integrate barcode creation into .NET applications.
// Prompt: Generate a Swiss Post Parcel additional service code barcode in Code 128 format with human‑readable description.
// Tags: barcode symbology, generation, png, aspose.barcode, code128, swisspostparcel

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss Post Parcel additional service code barcode with human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample additional service code for Swiss Post Parcel (replace with real data as needed)
        const string serviceCode = "1234567890123";

        // Output file path for the generated barcode image
        const string outputPath = "SwissPostParcel.png";

        // Initialize the barcode generator for Swiss Post Parcel (internally uses Code128 encoding)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, serviceCode))
        {
            // Position the human‑readable text below the barcode and center it
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Optional styling for the human‑readable text (font family and size)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // General barcode appearance settings
            generator.Parameters.Barcode.XDimension.Point = 2f;               // Module (X) size
            generator.Parameters.Barcode.FilledBars = false;                // Use non‑filled bars
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode image as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Swiss Post Parcel barcode saved to: {outputPath}");
    }
}
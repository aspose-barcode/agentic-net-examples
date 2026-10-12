// Title: Generate a Code 39 HIBC LIC barcode with primary data and save as PNG
// Description: Demonstrates how to create a HIBC Code 39 LIC barcode using primary data and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the ComplexBarcodeGenerator together with HIBCLICPrimaryDataCodetext and related classes (EncodeTypes, PrimaryData) to produce HIBC‑compliant barcodes. Developers often need to generate HIBC‑type barcodes for medical device labeling, inventory, and regulatory compliance, and this snippet illustrates the typical setup and saving process.
// Prompt: Generate a Code 39 HIBC LIC barcode with primary data and save it as a PNG image.
// Tags: barcode symbology, generation, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC Code 39 LIC barcode with primary data
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its parameters,
    /// and writes the image to the file system.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCLICPrimary.png");

        // Instantiate the complex codetext object for HIBC LIC primary data.
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext();

        // Resolve the specific EncodeTypes value (HIBCCode39LIC) via reflection.
        var field = typeof(EncodeTypes).GetField("HIBCCode39LIC");
        if (field == null)
        {
            // If the field is missing, inform the user and abort execution.
            Console.WriteLine("EncodeTypes does not contain HIBCCode39LIC. Cannot generate barcode.");
            return;
        }
        // Assign the resolved barcode type to the complex codetext.
        complexCodetext.BarcodeType = (BaseEncodeType)field.GetValue(null);

        // Populate the primary data required for the HIBC LIC barcode.
        complexCodetext.Data = new PrimaryData();
        complexCodetext.Data.ProductOrCatalogNumber = "12345";
        complexCodetext.Data.LabelerIdentificationCode = "A999";
        complexCodetext.Data.UnitOfMeasureID = 1;

        // Create the generator, set visual parameters, and save the barcode as PNG.
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set the X-dimension (module width) in pixels.
            gen.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode image to the specified path.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"HIBC LIC Code 39 barcode saved to: {outputPath}");
    }
}
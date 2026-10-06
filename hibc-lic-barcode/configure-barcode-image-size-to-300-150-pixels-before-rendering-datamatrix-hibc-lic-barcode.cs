// Title: Configure image size for DataMatrix HIBC LIC barcode
// Description: Demonstrates how to set a custom image width and height (300 × 150 pixels) when generating a DataMatrix HIBC LIC barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC. It shows how to use ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, and related parameter settings (XDimension, ImageWidth, ImageHeight, AutoSizeMode) to control barcode appearance. Developers creating healthcare or logistics labels often need to customize barcode dimensions while preserving symbology compliance.
// Prompt: Configure barcode image size to 300 × 150 pixels before rendering a DataMatrix HIBC LIC barcode.
// Tags: datamatrix, hibc, lic, image-size, png, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates configuring image dimensions for a DataMatrix HIBC LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the barcode and saves it to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "HIBCLIC_DataMatrix.png");

        // Create primary data codetext for HIBC LIC.
        HIBCLICPrimaryDataCodetext complexCodetext = new HIBCLICPrimaryDataCodetext();

        // Resolve the DataMatrix HIBC symbology name.
        string symName = "HIBCDM";
        var field = typeof(EncodeTypes).GetField(symName);
        if (field == null)
        {
            // Fallback to QR if DataMatrix not available.
            field = typeof(EncodeTypes).GetField("HIBCQRLIC");
            if (field == null)
            {
                Console.WriteLine("Neither HIBCDM nor HIBCQRLIC symbology is available.");
                return;
            }
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        complexCodetext.BarcodeType = encodeType;

        // Set primary data values required for HIBC LIC.
        complexCodetext.Data = new PrimaryData();
        complexCodetext.Data.ProductOrCatalogNumber = "12345";
        complexCodetext.Data.LabelerIdentificationCode = "A999";
        complexCodetext.Data.UnitOfMeasureID = 1;

        // Generate the barcode with the specified image size.
        using (ComplexBarcodeGenerator gen = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Optional: set module (X) dimension in pixels.
            gen.Parameters.Barcode.XDimension.Pixels = 5f;

            // Explicitly set image dimensions: 300 × 150 pixels.
            gen.Parameters.ImageWidth.Pixels = 300f;
            gen.Parameters.ImageHeight.Pixels = 150f;

            // Ensure the generator respects the manual size.
            gen.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Save the generated barcode as a PNG file.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}
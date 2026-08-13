// Title: Generate a rectangular DataMatrix barcode (10 rows x 30 columns)
// Description: Demonstrates how to configure a DataMatrix barcode to use a rectangular shape approximating 10 rows and 30 columns and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It shows how to select a specific rectangular version of a DataMatrix barcode using the BarcodeGenerator class, a common requirement when fitting barcodes into constrained layouts. Developers often need to control barcode dimensions for packaging, labeling, or UI rendering, and this snippet illustrates the typical API usage for version selection and image output.
// Prompt: Configure DataMatrix barcode to use rectangular shape with 10 rows and 30 columns.
// Tags: datamatrix, barcode, rectangular, version, aspose.barcode, c#, image, png, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a rectangular DataMatrix barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with a rectangular version close to 10 rows by 30 columns,
    /// then writes the resulting image to the file system.
    /// </summary>
    static void Main()
    {
        // The text to be encoded in the barcode.
        string codeText = "HelloWorld";

        // Initialize the barcode generator for DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Select a rectangular version that best matches the desired 10x30 size.
            // The exact 10x30 version is unavailable; ECC200_12x26 is the nearest alternative.
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_12x26;

            // Save the generated barcode as a PNG image.
            generator.Save("datamatrix.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("DataMatrix barcode generated: datamatrix.png");
    }
}
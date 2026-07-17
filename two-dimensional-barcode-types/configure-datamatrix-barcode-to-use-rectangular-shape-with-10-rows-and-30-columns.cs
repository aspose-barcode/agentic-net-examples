// Title: Configure DataMatrix Barcode with Rectangular Shape (10x30)
// Description: Demonstrates how to generate a DataMatrix barcode using a rectangular layout approximating 10 rows and 30 columns.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to set specific DataMatrix versions and shape options. It uses the BarcodeGenerator class together with EncodeTypes and DataMatrixVersion enums to control barcode dimensions. Developers often need to create rectangular DataMatrix codes for space‑constrained labels or to meet industry standards, and this snippet shows the typical API usage.
// Prompt: Configure DataMatrix barcode to use rectangular shape with 10 rows and 30 columns.
// Tags: datamatrix, barcode, rectangular, shape, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode with a rectangular shape approximating 10 rows and 30 columns,
/// then saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its version, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with the sample text "Sample".
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample"))
        {
            // Set the DataMatrix version to the nearest rectangular ECC200 size (8 rows x 32 columns).
            // Exact 10x30 is not defined in the specification, so the closest available version is used.
            generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_8x32;

            // Save the generated barcode to a PNG file named "datamatrix.png".
            generator.Save("datamatrix.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("DataMatrix barcode generated: datamatrix.png");
    }
}
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code16K barcodes with varying quiet zone coefficients using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for each combination of left and right quiet zone coefficients.
    /// </summary>
    static void Main()
    {
        // The data to encode in the barcode.
        const string codeText = "12345678901234567890";

        // Arrays of quiet zone coefficients to iterate over.
        int[] leftCoefficients = { 10, 15, 20 };
        int[] rightCoefficients = { 1, 3, 5 };

        // Loop through each left coefficient.
        foreach (int leftCoef in leftCoefficients)
        {
            // Loop through each right coefficient.
            foreach (int rightCoef in rightCoefficients)
            {
                // Create a barcode generator for Code16K with the specified text.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Set the quiet zone coefficients for the barcode.
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

                    // Build the output file name reflecting the current coefficients.
                    string fileName = $"Code16K_Left{leftCoef}_Right{rightCoef}.png";

                    // Save the generated barcode image to disk.
                    generator.Save(fileName);
                } // The generator is disposed here.
            }
        }
    }
}
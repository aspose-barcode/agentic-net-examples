// Title: Fallback to DataMatrix when MaxiCode generation fails
// Description: Demonstrates generating a MaxiCode barcode and automatically switching to a DataMatrix barcode if the data exceeds MaxiCode limits.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on error handling and fallback strategies. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameter classes to create different symbologies. Developers often need to ensure barcode creation succeeds even when input data constraints prevent a specific symbology, making fallback mechanisms essential.
// Prompt: Implement fallback mechanism that switches to DataMatrix when MaxiCode generation fails due to data size.
// Tags: maxicode, datamatrix, fallback, generation, png, barcodegenerator, parameters, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that attempts to generate a MaxiCode barcode and falls back to a DataMatrix barcode
/// when the input data exceeds MaxiCode size limitations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode; on failure, generates a DataMatrix barcode instead.
    /// </summary>
    static void Main()
    {
        // Prepare a large data string that exceeds MaxiCode capacity to trigger a failure.
        string largeData = new string('A', 2000);

        // Try to create a MaxiCode barcode with the provided data.
        try
        {
            using (var maxiGenerator = new BarcodeGenerator(EncodeTypes.MaxiCode, largeData))
            {
                // Set the MaxiCode mode to Mode4 (data-only mode). This is optional; Mode4 is the default.
                maxiGenerator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

                // Save the generated MaxiCode image to a PNG file.
                maxiGenerator.Save("maxicode.png");
                Console.WriteLine("MaxiCode barcode generated successfully: maxicode.png");
            }
        }
        catch (Exception ex)
        {
            // MaxiCode generation failed (likely due to data size). Log the error and prepare to fallback.
            Console.WriteLine($"MaxiCode generation failed: {ex.Message}");
            Console.WriteLine("Falling back to DataMatrix barcode.");

            // Attempt to generate a DataMatrix barcode using the same data.
            try
            {
                using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, largeData))
                {
                    // Use automatic encoding mode for DataMatrix.
                    dmGenerator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

                    // Save the fallback DataMatrix image to a PNG file.
                    dmGenerator.Save("datamatrix.png");
                    Console.WriteLine("DataMatrix barcode generated as fallback: datamatrix.png");
                }
            }
            catch (Exception fallbackEx)
            {
                // Fallback also failed; report the error.
                Console.WriteLine($"DataMatrix generation also failed: {fallbackEx.Message}");
            }
        }
    }
}
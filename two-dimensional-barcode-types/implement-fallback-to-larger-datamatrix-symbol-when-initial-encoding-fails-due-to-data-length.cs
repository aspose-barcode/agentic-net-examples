using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode by iterating through
/// available DataMatrix versions until the text fits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Attempts to encode a long text string into a DataMatrix barcode,
    /// selecting the smallest possible version that can contain the data.
    /// </summary>
    static void Main()
    {
        // Sample long text that may not fit into the smallest DataMatrix symbols.
        string codeText = "This is a long sample text intended to exceed the capacity of small DataMatrix symbols, forcing a fallback to larger versions.";

        // List of DataMatrix versions ordered from smallest to largest.
        // Includes both square and rectangular sizes for completeness.
        DataMatrixVersion[] versions = new DataMatrixVersion[]
        {
            DataMatrixVersion.ECC200_10x10,
            DataMatrixVersion.ECC200_12x12,
            DataMatrixVersion.ECC200_14x14,
            DataMatrixVersion.ECC200_16x16,
            DataMatrixVersion.ECC200_18x18,
            DataMatrixVersion.ECC200_20x20,
            DataMatrixVersion.ECC200_22x22,
            DataMatrixVersion.ECC200_24x24,
            DataMatrixVersion.ECC200_26x26,
            DataMatrixVersion.ECC200_32x32,
            DataMatrixVersion.ECC200_36x36,
            DataMatrixVersion.ECC200_40x40,
            DataMatrixVersion.ECC200_44x44,
            DataMatrixVersion.ECC200_48x48,
            DataMatrixVersion.ECC200_52x52,
            DataMatrixVersion.ECC200_64x64,
            DataMatrixVersion.ECC200_72x72,
            DataMatrixVersion.ECC200_80x80,
            DataMatrixVersion.ECC200_88x88,
            DataMatrixVersion.ECC200_96x96,
            DataMatrixVersion.ECC200_104x104,
            DataMatrixVersion.ECC200_120x120,
            DataMatrixVersion.ECC200_132x132,
            DataMatrixVersion.ECC200_144x144,
            // Rectangular sizes (optional, added for completeness)
            DataMatrixVersion.ECC200_8x18,
            DataMatrixVersion.ECC200_8x32,
            DataMatrixVersion.ECC200_12x26,
            DataMatrixVersion.ECC200_12x36,
            DataMatrixVersion.ECC200_16x36,
            DataMatrixVersion.ECC200_16x48,
            // DMRE sizes (rectangular only)
            DataMatrixVersion.DMRE_8x48,
            DataMatrixVersion.DMRE_8x64,
            DataMatrixVersion.DMRE_8x80,
            DataMatrixVersion.DMRE_8x96,
            DataMatrixVersion.DMRE_8x120,
            DataMatrixVersion.DMRE_8x144,
            DataMatrixVersion.DMRE_12x64,
            DataMatrixVersion.DMRE_12x88,
            DataMatrixVersion.DMRE_16x64,
            DataMatrixVersion.DMRE_20x36,
            DataMatrixVersion.DMRE_20x44,
            DataMatrixVersion.DMRE_20x64,
            DataMatrixVersion.DMRE_22x48,
            DataMatrixVersion.DMRE_24x48,
            DataMatrixVersion.DMRE_24x64,
            DataMatrixVersion.DMRE_26x40,
            DataMatrixVersion.DMRE_26x48,
            DataMatrixVersion.DMRE_26x64
        };

        // Output file path for the generated barcode image.
        string outputPath = "datamatrix.png";

        // Flag indicating whether a barcode was successfully generated.
        bool generated = false;

        // Iterate through each version, attempting to generate the barcode.
        foreach (var version in versions)
        {
            // Create a new generator for each attempt to ensure a clean state.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
            {
                // Assign the text to encode.
                generator.CodeText = codeText;

                // Set the specific DataMatrix version to try.
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = version;

                try
                {
                    // Attempt to save the barcode image.
                    generator.Save(outputPath);
                    Console.WriteLine($"DataMatrix generated successfully with version {version}.");
                    generated = true;
                    break; // Exit loop on success.
                }
                catch (Exception ex)
                {
                    // If generation fails (e.g., text too large), log and continue.
                    Console.WriteLine($"Failed with version {version}: {ex.Message}");
                }
            }
        }

        // If none of the versions succeeded, inform the user.
        if (!generated)
        {
            Console.WriteLine("Unable to generate DataMatrix barcode with any available version.");
        }
    }
}
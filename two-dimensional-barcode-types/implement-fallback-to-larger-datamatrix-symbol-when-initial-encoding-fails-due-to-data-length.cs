using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample codetext that is too long for the smallest DataMatrix symbols
        string codeText = "This is a sample text that is intentionally long enough to require a larger DataMatrix symbol. " +
                          "It contains multiple sentences and exceeds the capacity of the smallest versions.";

        // List of DataMatrix versions to try, ordered from smallest to largest
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
            DataMatrixVersion.ECC200_144x144
        };

        bool generated = false;
        foreach (var version in versions)
        {
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                {
                    // Set the specific DataMatrix version to attempt
                    generator.Parameters.Barcode.DataMatrix.Version = version;

                    // Optional: set a reasonable image size
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 300f;

                    // Save the barcode image; filename includes the version name
                    string fileName = $"DataMatrix_{version}.png";
                    generator.Save(fileName);
                    Console.WriteLine($"Successfully generated DataMatrix with version {version} -> {fileName}");
                    generated = true;
                    break; // Exit loop after successful generation
                }
            }
            catch (Exception ex)
            {
                // Generation failed, likely due to insufficient symbol size.
                // Continue to the next larger version.
                Console.WriteLine($"Version {version} failed: {ex.Message}");
            }
        }

        if (!generated)
        {
            Console.WriteLine("Failed to generate DataMatrix barcode with all attempted versions.");
        }
    }
}
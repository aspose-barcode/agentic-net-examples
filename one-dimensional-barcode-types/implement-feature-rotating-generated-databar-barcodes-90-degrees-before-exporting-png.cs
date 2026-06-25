using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of various DataBar barcode symbologies,
/// rotates each barcode 90 degrees, and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates, rotates, and saves DataBar barcodes.
    /// </summary>
    static void Main()
    {
        // Determine the output directory for generated images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Create the output directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // List of DataBar symbologies to generate.
        BaseEncodeType[] dataBarTypes = new BaseEncodeType[]
        {
            EncodeTypes.DatabarOmniDirectional,
            EncodeTypes.DatabarStacked,
            EncodeTypes.DatabarStackedOmniDirectional,
            EncodeTypes.DatabarLimited,
            EncodeTypes.DatabarExpanded,
            EncodeTypes.DatabarExpandedStacked
        };

        // Corresponding file name parts for each symbology.
        string[] typeNames = new string[]
        {
            "DatabarOmniDirectional",
            "DatabarStacked",
            "DatabarStackedOmniDirectional",
            "DatabarLimited",
            "DatabarExpanded",
            "DatabarExpandedStacked"
        };

        // Iterate over each DataBar type and generate the barcode.
        for (int i = 0; i < dataBarTypes.Length; i++)
        {
            BaseEncodeType type = dataBarTypes[i];
            string fileName = $"{typeNames[i]}.png";
            string outputPath = Path.Combine(outputDir, fileName);

            // Choose appropriate codetext for each DataBar type.
            // Limited type requires a GTIN style codetext.
            string codeText = (type == EncodeTypes.DatabarLimited)
                ? "(01)08888888888888"
                : "(01)12345678901231";

            // Generate the barcode, rotate it 90 degrees, and save as PNG.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Set rotation angle to 90 degrees.
                generator.Parameters.RotationAngle = 90f;

                // Save the rotated barcode image to the output path.
                generator.Save(outputPath);
            }

            // Inform the user that the file has been generated.
            Console.WriteLine($"Generated {fileName}");
        }

        // Final status message.
        Console.WriteLine("All DataBar barcodes have been generated and rotated.");
    }
}
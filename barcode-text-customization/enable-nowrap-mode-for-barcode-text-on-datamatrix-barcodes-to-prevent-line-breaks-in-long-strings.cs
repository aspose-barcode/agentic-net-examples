// Title: Enable NoWrap mode for DataMatrix barcode text
// Description: Demonstrates how to prevent line breaks in the human‑readable text of a DataMatrix barcode by enabling NoWrap mode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode text formatting. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to control text wrapping. Developers often need to adjust human‑readable text appearance for long strings in various symbologies, especially DataMatrix, to ensure readability in printed or displayed barcodes.
// Prompt: Enable NoWrap mode for barcode text on DataMatrix barcodes to prevent line breaks in long strings.
// Tags: datamatrix, nowrap, barcode, text-formatting, generation, csharp, aspnet, aspnetcore

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataMatrix barcode with NoWrap enabled to keep the human‑readable text on a single line.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode from a long string and saves it as an image.
    /// </summary>
    static void Main()
    {
        // Define a long codetext that would wrap without NoWrap enabled.
        string longText = "ThisIsAVeryLongStringThatMightWrapInTheHumanReadableTextIfNoWrapIsNotEnabled1234567890";

        // Initialize a DataMatrix barcode generator with the long text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Enable NoWrap mode to prevent line breaks in the displayed human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Set auto‑size mode to Interpolation (optional) to ensure proper image scaling.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define the output file path.
            string outputPath = "datamatrix_nowrap.png";

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("DataMatrix barcode generated with NoWrap enabled.");
    }
}
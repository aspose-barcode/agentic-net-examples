// Title: Generate Codabar barcode and save as PNG
// Description: Demonstrates creating a Codabar barcode with Aspose.BarCode, setting the code text, and saving the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarCodeBuilder (via reflection) and BarcodeGenerator to produce barcodes. It covers key API classes such as BarCodeBuilder, BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use for creating various symbologies and exporting them to image formats.
// Prompt: Instantiate BarCodeBuilder, set CodeText, select Codabar symbology, and render to PNG file.
// Tags: barcode, codabar, generation, png, aspose.barcode, barcodelibrary, imageoutput

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output path, attempts to use BarCodeBuilder via reflection,
    /// falls back to BarcodeGenerator if necessary, and writes the resulting image file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "codabar.png");

        // Try to locate BarCodeBuilder type via reflection (optional, may not be present)
        Type builderType = Type.GetType("Aspose.BarCode.BarCodeBuilder, Aspose.BarCode");
        if (builderType != null)
        {
            try
            {
                // Create an instance of BarCodeBuilder
                object builder = Activator.CreateInstance(builderType);

                // Set the barcode text (CodeText property)
                var codeTextProp = builderType.GetProperty("CodeText");
                if (codeTextProp != null && codeTextProp.CanWrite)
                {
                    codeTextProp.SetValue(builder, "-12345-");
                }

                // Set the symbology to Codabar (BarcodeType or Symbology property)
                var symProp = builderType.GetProperty("BarcodeType") ?? builderType.GetProperty("Symbology");
                if (symProp != null && symProp.CanWrite)
                {
                    symProp.SetValue(builder, EncodeTypes.Codabar);
                }

                // Invoke the Save method to write the PNG file
                var saveMethod = builderType.GetMethod("Save", new Type[] { typeof(string), typeof(BarCodeImageFormat) });
                if (saveMethod != null)
                {
                    saveMethod.Invoke(builder, new object[] { outputFile, BarCodeImageFormat.Png });
                    Console.WriteLine($"Barcode generated using BarCodeBuilder at: {outputFile}");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Log any reflection errors and fall back to the generator approach
                Console.WriteLine($"Error using BarCodeBuilder: {ex.Message}");
            }
        }

        // Fallback: use BarcodeGenerator directly to create the Codabar barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Barcode generated using BarcodeGenerator at: {outputFile}");
    }
}
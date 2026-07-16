// Title: Generate MaxiCode barcode with logging of parameters
// Description: Demonstrates creating a MaxiCode barcode, configuring its mode, aspect ratio, and encoding mode, and logging these settings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCode-specific parameters to produce a barcode image. Developers commonly need to customize MaxiCode settings for logistics and shipping applications, adjusting mode, aspect ratio, and encoding mode to meet industry standards.
// Prompt: Implement logging of MaxiCode generation parameters, including mode, aspect ratio, and encoding mode.
// Tags: maxicode, barcode generation, logging, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a MaxiCode barcode, configures its specific parameters, logs the settings,
/// and saves the resulting image to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up the barcode generator, applies MaxiCode options,
    /// logs the configuration, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode.png";

        // Initialize a BarcodeGenerator for the MaxiCode symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Assign the text to be encoded. For MaxiCode modes 4‑6 a simple message is sufficient.
            generator.CodeText = "Sample MaxiCode";

            // Configure MaxiCode‑specific parameters:
            //   Mode: selects the MaxiCode variant (4, 5, or 6).
            //   AspectRatio: defines the height‑to‑width ratio of the symbol.
            //   EncodeMode: determines how the data is encoded (Auto lets the library choose).
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;               // Set mode (4, 5, or 6)
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.0f;                     // Height/Width ratio
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Auto; // Encoding mode

            // Log the configured MaxiCode parameters to the console for verification.
            Console.WriteLine("MaxiCode Generation Parameters:");
            Console.WriteLine($"  Mode          : {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"  AspectRatio   : {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"  EncodeMode    : {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
            Console.WriteLine($"MaxiCode image saved to: {outputPath}");
        }
    }
}
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Han Xin barcode with a long payload,
/// handling version selection and fallback to automatic sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, first attempting a specific small version,
    /// then falling back to automatic version selection if needed.
    /// </summary>
    static void Main()
    {
        const string outputPath = "hanxin.png";

        // Create a code text that is intentionally long to exceed the capacity of a small Han Xin version.
        string longCodeText = new string('A', 2000);

        // First attempt: use a specific small version (Version01) which will likely be insufficient.
        try
        {
            // Initialize the barcode generator with Han Xin encoding and the long payload.
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, longCodeText))
            {
                // Force a small symbol size.
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Version01;

                // Optional: set an error correction level.
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Save the generated barcode to the specified file.
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved successfully to '{outputPath}' using Version01.");
            }
        }
        catch (Exception ex)
        {
            // Log the error from the first attempt.
            Console.WriteLine($"Error with selected Han Xin version: {ex.Message}");

            // Fallback: let the library choose the appropriate version automatically.
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, longCodeText))
                {
                    // Set version to Auto so the library determines the required size.
                    generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                    // Save the barcode using the automatically selected version.
                    generator.Save(outputPath);
                    Console.WriteLine($"Barcode saved successfully to '{outputPath}' using auto version.");
                }
            }
            catch (Exception fallbackEx)
            {
                // Log any errors that occur during the fallback attempt.
                Console.WriteLine($"Failed to generate barcode with auto version: {fallbackEx.Message}");
            }
        }
    }
}
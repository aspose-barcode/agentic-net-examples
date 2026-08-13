// Title: Checksum validation for unsupported symbology
// Description: Demonstrates that enabling checksum on a symbology that does not support it throws a meaningful exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and related parameter classes. Developers often need to validate configuration settings such as checksum support for specific symbologies (e.g., Codabar) and handle the resulting exceptions appropriately. The snippet shows typical use cases for error handling during barcode creation.
// Prompt: Test that setting IsChecksumEnabled true for a symbology lacking checksum support throws a meaningful exception.
// Tags: barcode, codabar, checksum, exception, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that verifies Aspose.BarCode throws an exception when a checksum is enabled
/// for a symbology (Codabar) that does not support it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Attempts to enable checksum on Codabar and expects a <see cref="BarCodeException"/>.
    /// </summary>
    static void Main()
    {
        // Path for the temporary barcode image (will be deleted after the test)
        string outputPath = "codabar.png";

        // Ensure no leftover file exists from previous runs
        if (File.Exists(outputPath))
        {
            try
            {
                File.Delete(outputPath);
            }
            catch
            {
                // Ignored – file may be in use or locked; cleanup will be attempted later
            }
        }

        try
        {
            // Create a barcode generator for Codabar (which lacks checksum support)
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123B"))
            {
                // Attempt to enable checksum – this should trigger validation and throw
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Force barcode generation to invoke the validation logic
                generator.Save(outputPath);
            }

            // If execution reaches here, the expected exception was not thrown
            Console.WriteLine("No exception was thrown. Checksum enabling unexpectedly succeeded.");
        }
        catch (BarCodeException ex)
        {
            // Expected outcome: Aspose.BarCode throws BarCodeException for invalid checksum usage
            Console.WriteLine($"Caught BarCodeException as expected: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other exception type indicates an unexpected failure
            Console.WriteLine($"Caught unexpected exception type: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Clean up the generated file if it was created despite the exception
            if (File.Exists(outputPath))
            {
                try
                {
                    File.Delete(outputPath);
                }
                catch
                {
                    // Ignored – cleanup failure is non‑critical for this example
                }
            }
        }
    }
}
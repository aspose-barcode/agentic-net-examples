// Title: Checksum Validation for Unsupported Symbology
// Description: Demonstrates that enabling checksum on a barcode symbology that does not support it (e.g., Codabar) throws a meaningful exception.
// Prompt: Test that setting IsChecksumEnabled true for a symbology lacking checksum support throws a meaningful exception.
// Tags: barcode, symbology, checksum, exception, aspose.barcode, codabar, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that verifies checksum handling for a symbology without checksum support.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Attempts to enable checksum on Codabar and expects an exception.
    /// </summary>
    static void Main()
    {
        // Select a symbology that does not support checksum (Codabar)
        BaseEncodeType symbology = EncodeTypes.Codabar;
        string codeText = "A123B";

        try
        {
            // Initialize the barcode generator with the chosen symbology and data
            using (BarcodeGenerator generator = new BarcodeGenerator(symbology, codeText))
            {
                // Attempt to enable checksum – this should fail for Codabar
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Save forces validation and triggers barcode generation
                generator.Save("codabar.png");
                Console.WriteLine("Barcode generated successfully (unexpected).");
            }
        }
        catch (Exception ex)
        {
            // Expected outcome: an exception indicating checksum is unsupported
            Console.WriteLine("Expected exception caught:");
            Console.WriteLine(ex.Message);
        }
    }
}
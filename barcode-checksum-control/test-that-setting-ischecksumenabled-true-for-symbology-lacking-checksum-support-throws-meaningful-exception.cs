// Title: Enabling checksum on a QR code triggers an exception
// Description: Demonstrates that setting IsChecksumEnabled to true for a QR code, which lacks checksum support, results in a meaningful exception.
// Category-Description: This example belongs to the Aspose.BarCode generation and validation category, illustrating how to use BarcodeGenerator, configure barcode parameters, and handle validation errors. Developers often need to verify supported features per symbology, such as checksum availability, and capture exceptions when unsupported options are applied.
// Prompt: Test that setting IsChecksumEnabled true for a symbology lacking checksum support throws a meaningful exception.
// Tags: barcode, symbology, checksum, exception handling, generation, aspose.barcode, qr code

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exception handling when enabling checksum on an unsupported symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that attempts to enable checksum on a QR code and catches the expected exception.
    /// </summary>
    static void Main()
    {
        try
        {
            // QR code does not support checksum. Enabling it should trigger validation and throw.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test"))
            {
                // Attempt to enable checksum; this operation is invalid for QR codes.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Generate the barcode to force internal validation logic to run.
                generator.Save("qr.png");

                // If no exception occurs, the behavior is unexpected.
                Console.WriteLine("Checksum enabled without exception (unexpected).");
            }
        }
        catch (Exception ex)
        {
            // Expected path: capture and display the meaningful exception.
            Console.WriteLine($"Caught expected exception: {ex.GetType().Name} - {ex.Message}");
        }
    }
}
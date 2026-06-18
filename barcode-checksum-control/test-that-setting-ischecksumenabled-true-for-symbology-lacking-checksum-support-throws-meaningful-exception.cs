using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program class containing the entry point that demonstrates checksum handling for Codabar.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Codabar with sample data "A123B".
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123B"))
        {
            // Codabar does not support checksum. Enabling it should cause an exception.
            // Set the checksum flag to Yes to provoke the error.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            try
            {
                // Save the barcode to a memory stream in PNG format.
                // This operation triggers the barcode generation and thus the exception.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // If no exception occurs, inform that the checksum enable succeeded unexpectedly.
                Console.WriteLine("No exception was thrown. Checksum enable succeeded unexpectedly.");
            }
            catch (Exception ex)
            {
                // Expected path: catch and display the exception message.
                Console.WriteLine("Expected exception caught:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
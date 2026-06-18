using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and handling
/// the exception thrown when attempting to disable the checksum (which is not allowed for Code128).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, attempts to disable its checksum, and saves the image.
    /// Handles any exceptions that occur during generation.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        string outputPath = "code128.png";

        try
        {
            // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Attempt to disable the checksum.
                // This operation is not supported for Code128 and will throw an exception.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save the generated barcode image to the specified path.
                // This line will not be reached if the above line throws an exception.
                generator.Save(outputPath);

                // Inform the user that the barcode was saved successfully.
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Catch and display the expected exception when disabling checksum for Code128.
            Console.WriteLine("Error generating barcode with checksum disabled:");
            Console.WriteLine(ex.Message);
        }
    }
}
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode with Aspose.BarCode and handling invalid code text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output barcode image in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Choose Code39 symbology, which supports a limited set of characters.
        BaseEncodeType encodeType = EncodeTypes.Code39;

        // Prepare an invalid code text containing a character ('@') not allowed in Code39.
        string invalidCodeText = "ABC@123";

        // Create a BarcodeGenerator instance with the specified symbology.
        using (var generator = new BarcodeGenerator(encodeType))
        {
            // Configure the generator to throw an exception when the code text is invalid.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Assign the invalid code text to the generator.
            generator.CodeText = invalidCodeText;

            try
            {
                // Attempt to save the barcode image to the specified path.
                generator.Save(outputPath);
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
            // Catch specific exception for invalid code text.
            catch (InvalidCodeException ex)
            {
                Console.WriteLine("InvalidCodeException caught: " + ex.Message);
            }
            // Catch any other unexpected exceptions.
            catch (Exception ex)
            {
                Console.WriteLine("General exception caught: " + ex.Message);
            }
        }
    }
}
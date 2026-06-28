using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode without human‑readable text,
/// saving it to a temporary file, reading it back, and cleaning up the file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define the path for the temporary barcode image file.
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with the sample text "123456".
        // The human‑readable text is hidden by setting CodeLocation.None.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode from the saved image to ensure it is decodable.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
            else
            {
                // Output details for each detected barcode.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded code text: {result.CodeText}");
                }

                // Confirm that the human‑readable text was hidden as intended.
                Console.WriteLine("Human‑readable text was hidden (CodeLocation.None).");
            }
        }

        // --------------------------------------------------------------------
        // Optional cleanup: delete the temporary barcode image file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}
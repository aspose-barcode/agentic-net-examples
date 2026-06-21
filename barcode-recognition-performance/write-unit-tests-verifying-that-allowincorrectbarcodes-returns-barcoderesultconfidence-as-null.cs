using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code39 barcode, reading it with relaxed quality settings,
/// and verifying that the Confidence property is null when AllowIncorrectBarcodes is enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary PNG file path for the barcode image.
        // --------------------------------------------------------------------
        string tempFile = Path.Combine(Path.GetTempPath(), "test_barcode.png");

        // --------------------------------------------------------------------
        // Generate a simple Code39 barcode and save it to the temporary file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            generator.Save(tempFile);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image file was successfully created.
        // --------------------------------------------------------------------
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode from the file with AllowIncorrectBarcodes enabled.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code39))
        {
            // Enable recognition of barcodes that may not meet strict quality criteria.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the barcode reading operation.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, report and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate over each detected barcode result.
            foreach (var result in results)
            {
                // Expect the Confidence property to be null when AllowIncorrectBarcodes is true.
                bool isConfidenceNull = result.Confidence == null;

                // Output test result based on the Confidence value.
                Console.WriteLine(isConfidenceNull
                    ? "Test Passed: Confidence is null."
                    : $"Test Failed: Confidence is {result.Confidence}.");
            }
        }

        // --------------------------------------------------------------------
        // Clean up: delete the temporary barcode image file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}
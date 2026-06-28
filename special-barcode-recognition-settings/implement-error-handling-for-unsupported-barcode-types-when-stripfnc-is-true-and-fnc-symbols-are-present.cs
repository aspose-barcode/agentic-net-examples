using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a Code128 barcode containing an FNC1 character,
/// then reading it with StripFNC enabled to observe behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample application.
    /// Generates a barcode, reads it with StripFNC, and outputs results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Create a Code128 barcode that contains an FNC1 character (ASCII 29)
        // This symbology does not support StripFNC, so we expect handling of the case.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "\u001D12345"))
        {
            // Save the generated barcode image to the temporary path
            generator.Save(imagePath);
        }

        // Verify the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Attempt to read the barcode with StripFNC enabled
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable stripping of FNC characters during decoding
            reader.BarcodeSettings.StripFNC = true;

            try
            {
                // Read all barcodes from the image
                var results = reader.ReadBarCodes();

                // Process each decoding result
                foreach (var result in results)
                {
                    // Determine whether the decoded text still contains the FNC character (ASCII 29)
                    bool containsFnc = result.CodeText != null && result.CodeText.Contains("\u001D");

                    if (containsFnc)
                    {
                        // The barcode type does not support StripFNC; report a warning
                        Console.WriteLine($"[Warning] Barcode type '{result.CodeTypeName}' does not support StripFNC. FNC characters remain in the code text.");
                    }
                    else
                    {
                        // StripFNC succeeded; display the cleaned code text
                        Console.WriteLine($"Barcode type: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText (FNC stripped): {result.CodeText}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors, such as unsupported operation for StripFNC
                Console.WriteLine($"Error during barcode reading: {ex.Message}");
                Console.WriteLine("The selected barcode type may not support StripFNC.");
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored – file may be in use or already deleted
        }
    }
}
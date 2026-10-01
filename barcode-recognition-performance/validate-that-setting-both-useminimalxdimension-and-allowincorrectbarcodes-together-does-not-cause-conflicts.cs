// Title: Validate UseMinimalXDimension with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode, then reading it with both UseMinimalXDimension and AllowIncorrectBarcodes enabled to ensure no conflicts arise.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for fine‑tuning the decoding process. Developers often need to adjust X‑dimension handling and tolerate imperfect barcodes; this snippet illustrates how to configure those options without causing runtime conflicts.
// Prompt: Validate that setting both UseMinimalXDimension and AllowIncorrectBarcodes together does not cause conflicts.
// Tags: barcode, symbology, generation, recognition, useminimalxdimension, allowincorrectbarcodes, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that validates the combined use of <c>UseMinimalXDimension</c> and
/// <c>AllowIncorrectBarcodes</c> settings in <see cref="BarCodeReader.QualitySettings"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, reads it with specific
    /// quality settings, and outputs the decoding results.
    /// </summary>
    static void Main()
    {
        // Prepare a sample barcode text and select the Code128 symbology.
        const string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a barcode generator with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Use a memory stream to hold the generated PNG image.
            using (var barcodeStream = new MemoryStream())
            {
                // Save the barcode image to the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                // Reset the stream position so it can be read from the beginning.
                barcodeStream.Position = 0;

                // Initialize a reader for the generated barcode image.
                using (var reader = new BarCodeReader(barcodeStream, DecodeType.Code128))
                {
                    // Enable minimal X‑dimension mode to let the reader choose the smallest possible module size.
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    // Allow the reader to accept barcodes that may not strictly conform to specifications.
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    try
                    {
                        bool anyFound = false;

                        // Iterate through all detected barcodes (there should be only one in this case).
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            anyFound = true;
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                        }

                        // Provide feedback based on detection outcome.
                        if (!anyFound)
                        {
                            Console.WriteLine("No barcode was detected.");
                        }
                        else
                        {
                            Console.WriteLine("Barcode read successfully with both settings enabled.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Any conflict between the settings would surface as an exception here.
                        Console.WriteLine($"Exception occurred while reading barcode: {ex.Message}");
                    }
                }
            }
        }
    }
}
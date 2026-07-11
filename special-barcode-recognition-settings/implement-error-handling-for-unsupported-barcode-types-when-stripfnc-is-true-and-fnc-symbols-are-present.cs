// Title: StripFNC Support Validation for Barcode Types
// Description: Demonstrates generating a barcode, enabling StripFNC during reading, and validating that the barcode symbology supports FNC stripping.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with BarcodeSettings to decode them. Developers often need to handle FNC (Function) characters, especially in GS1 implementations, and must verify that the selected symbology supports stripping these characters. The code illustrates typical error‑handling patterns for unsupported barcode types.
// Prompt: Implement error handling for unsupported barcode types when StripFNC is true and FNC symbols are present.
// Tags: barcode symbology, fnc stripping, error handling, aspose.barcode, generation, recognition, c#

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a barcode, enables StripFNC during reading,
/// and validates whether the barcode type supports FNC stripping.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, configures the reader,
    /// checks for FNC support, and decodes the barcode while handling possible errors.
    /// </summary>
    static void Main()
    {
        // Define the barcode type and a code text that includes an FNC placeholder.
        // In real scenarios FNC characters are represented differently,
        // but for demonstration we use the string "<FNC1>".
        BaseEncodeType barcodeType = EncodeTypes.Code128;
        string originalCodeText = "ABC<FNC1>DEF";

        // --------------------------------------------------------------------
        // Generate and save the barcode image.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(barcodeType, originalCodeText))
        {
            generator.Save("barcode.png");
        }

        // --------------------------------------------------------------------
        // Prepare the barcode reader with StripFNC enabled.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader("barcode.png", DecodeType.Code128))
        {
            // Enable stripping of FNC characters during decoding.
            reader.BarcodeSettings.StripFNC = true;

            // List of symbologies that support FNC stripping.
            BaseEncodeType[] fncSupported = new[]
            {
                EncodeTypes.GS1Code128,
                EncodeTypes.GS1QR,
                EncodeTypes.GS1DataMatrix,
                EncodeTypes.GS1Aztec,
                EncodeTypes.GS1HanXin,
                EncodeTypes.GS1CompositeBar,
                EncodeTypes.GS1DotCode,
                EncodeTypes.GS1MicroPdf417,
                EncodeTypes.QR,
                EncodeTypes.DataMatrix,
                EncodeTypes.Aztec
            };

            // ----------------------------------------------------------------
            // Validate that the selected barcode type supports StripFNC
            // when an FNC placeholder is present in the original code text.
            // ----------------------------------------------------------------
            if (reader.BarcodeSettings.StripFNC && originalCodeText.Contains("<FNC1>"))
            {
                bool isSupported = false;
                foreach (var supported in fncSupported)
                {
                    if (barcodeType.Equals(supported))
                    {
                        isSupported = true;
                        break;
                    }
                }

                if (!isSupported)
                {
                    // Report the unsupported scenario; developers may choose to throw.
                    Console.WriteLine($"Error: StripFNC is enabled, but barcode type '{barcodeType.GetType().Name}' does not support FNC characters.");
                    // throw new ArgumentException("Unsupported barcode type for StripFNC.");
                }
            }

            // ----------------------------------------------------------------
            // Attempt to read the barcode and handle any Aspose.BarCode exceptions.
            // ----------------------------------------------------------------
            try
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
            catch (Aspose.BarCode.BarCodeException ex)
            {
                Console.WriteLine($"BarCodeException caught: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: {ex.Message}");
            }
        }
    }
}
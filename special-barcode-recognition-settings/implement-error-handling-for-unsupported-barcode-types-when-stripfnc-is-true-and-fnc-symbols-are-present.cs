using System;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string imagePath = "fnc_barcode.png";

        // Create a QR barcode that contains an FNC1 symbol using the extended builder
        QrExtCodetextBuilder builder = new QrExtCodetextBuilder();
        builder.AddFNC1FirstPosition();                     // FNC1 at the first position
        builder.AddPlainCodetext("12345");                  // Sample data
        string extendedCodeText = builder.GetExtendedCodetext();

        // Generate and save the barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = extendedCodeText;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Save(imagePath);
        }

        // Define barcode types that support stripping FNC characters
        var fncSupportedTypes = new HashSet<BaseDecodeType>
        {
            DecodeType.GS1Code128,
            DecodeType.GS1CompositeBar,
            DecodeType.GS1DataMatrix,
            DecodeType.GS1DotCode,
            DecodeType.GS1HanXin,
            DecodeType.GS1QR,
            DecodeType.Pdf417,
            DecodeType.MicroPdf417,
            DecodeType.QR,
            DecodeType.Aztec,
            DecodeType.DataMatrix,
            DecodeType.GS1Aztec
        };

        // Attempt to read the barcode using a type that does NOT support FNC stripping (e.g., Code39)
        bool stripFnc = true;
        try
        {
            // Validate support before reading
            if (stripFnc && !fncSupportedTypes.Contains(DecodeType.Code39))
            {
                throw new ArgumentException($"StripFNC is enabled, but the selected decode type '{DecodeType.Code39}' does not support FNC characters.");
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
            {
                // Enable StripFNC as requested
                reader.BarcodeSettings.StripFNC = stripFnc;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
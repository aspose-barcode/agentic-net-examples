using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.BarCodeRecognition; // for XDimensionMode
using Aspose.Drawing; // required for image handling if needed

class Program
{
    static void Main()
    {
        // Step 1: Generate a sample barcode image.
        const string barcodeFile = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a known XDimension (2 points) to have a measurable element size.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodeFile);
        }

        // Step 2: Read the barcode with diagnostic mode enabled.
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.Code128))
        {
            // Activate the UseMinimalXDimension mode.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            Console.WriteLine("Diagnostic mode: UseMinimalXDimension is active.");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Log the decoded text.
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // Attempt to log the element size (XDimension) if available.
                // Some barcode types expose XDimension; otherwise, fallback to a placeholder.
                try
                {
                    // The XDimension property may not exist on all result types.
                    // Use reflection to safely access it if present.
                    var xDimProp = result.GetType().GetProperty("XDimension");
                    if (xDimProp != null)
                    {
                        var xDimValue = xDimProp.GetValue(result);
                        Console.WriteLine($"Element XDimension: {xDimValue}");
                    }
                    else
                    {
                        Console.WriteLine("Element XDimension: not available for this barcode type.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving XDimension: {ex.Message}");
                }
            }
        }
    }
}
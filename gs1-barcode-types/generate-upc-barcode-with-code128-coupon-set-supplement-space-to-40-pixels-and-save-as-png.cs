using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC-A coupon barcode with a supplement space using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a barcode image.
    /// </summary>
    static void Main()
    {
        // The data to encode in the barcode, including the coupon information.
        string codeText = "514141100906(8102)03";

        // Initialize the barcode generator with the specific encoding type and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Set the supplement space (distance) to 40 points.
            generator.Parameters.Barcode.Coupon.SupplementSpace.Point = 40f;

            // Define the output file path for the generated barcode image.
            string outputPath = "upc_a_coupon.png";

            // Save the barcode image to the specified path.
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}
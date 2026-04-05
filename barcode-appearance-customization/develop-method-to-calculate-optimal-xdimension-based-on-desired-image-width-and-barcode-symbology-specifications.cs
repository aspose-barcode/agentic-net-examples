using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Desired barcode image width in pixels
        int desiredWidth = 300;
        // Sample code text and symbology
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Calculate the optimal XDimension (in points) to achieve the desired width
        float optimalXDimension = CalculateOptimalXDimension(desiredWidth, codeText, encodeType);
        Console.WriteLine($"Optimal XDimension (points): {optimalXDimension}");

        // Generate the final barcode using the calculated XDimension
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Apply the calculated XDimension
            generator.Parameters.Barcode.XDimension.Point = optimalXDimension;
            // Save the barcode image
            generator.Save("barcode_optimal.png");
        }
    }

    /// <summary>
    /// Calculates the XDimension (points) needed to obtain a specific image width.
    /// </summary>
    /// <param name="desiredWidth">Target image width in pixels.</param>
    /// <param name="codeText">Text to encode.</param>
    /// <param name="encodeType">Barcode symbology.</param>
    /// <returns>Calculated XDimension in points.</returns>
    static float CalculateOptimalXDimension(int desiredWidth, string codeText, BaseEncodeType encodeType)
    {
        // Create a temporary generator with default settings
        using (var tempGenerator = new BarcodeGenerator(encodeType, codeText))
        {
            // Generate the barcode image to obtain its natural width
            using (Bitmap tempImage = tempGenerator.GenerateBarCodeImage())
            {
                int originalWidth = tempImage.Width;
                // Current XDimension (points) used by the temporary generator
                float originalXDimension = tempGenerator.Parameters.Barcode.XDimension.Point;

                // Scaling factor required to reach the desired width
                float scaleFactor = (float)desiredWidth / originalWidth;

                // New XDimension that will produce the target width
                return originalXDimension * scaleFactor;
            }
        }
    }
}
using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Calculates an approximate optimal XDimension (in points) for a 1D barcode.
    // The calculation assumes ~11 modules per character for Code128 (common for many 1D symbologies).
    // desiredWidthPoints: target image width in points (1 point = 1/72 inch).
    static float CalculateOptimalXDimension(string codeText, BaseEncodeType encodeType, float desiredWidthPoints)
    {
        // Simple estimation of total modules based on code length.
        // For more accurate results, you would need to query the symbology's exact module count.
        const int modulesPerCharacter = 11;
        int totalModules = codeText.Length * modulesPerCharacter;

        if (totalModules == 0)
            throw new ArgumentException("Code text must contain at least one character.", nameof(codeText));

        // XDimension = desired width / total modules
        return desiredWidthPoints / totalModules;
    }

    static void Main()
    {
        // Desired image width: 300 points (~4.17 inches at 72 DPI)
        float desiredWidthPoints = 300f;
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Calculate optimal XDimension
        float optimalXDim = CalculateOptimalXDimension(codeText, encodeType, desiredWidthPoints);
        Console.WriteLine($"Calculated optimal XDimension: {optimalXDim:F3} points");

        // Create barcode generator and apply the calculated XDimension
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the XDimension using the .Point member as required by the API
            generator.Parameters.Barcode.XDimension.Point = optimalXDim;

            // Optionally set the image width to the desired value for consistency
            generator.Parameters.ImageWidth.Point = desiredWidthPoints;

            // Save the barcode image
            generator.Save("barcode.png");
            Console.WriteLine("Barcode image saved as 'barcode.png'.");
        }
    }
}
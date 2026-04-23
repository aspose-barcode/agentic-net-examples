using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define the barcode type and the data to encode
        const string codeText = "1234567890";
        // Create the barcode generator inside a using block (IDisposable)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Hide the human‑readable text (code text) by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image to a file
            const string outputFile = "barcode.png";
            generator.Save(outputFile);

            // Verify that the code text is hidden
            bool isHidden = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None;
            Console.WriteLine(isHidden
                ? "Code text is hidden as expected."
                : "Code text is visible; hiding failed.");

            Console.WriteLine($"Barcode image saved to '{outputFile}'.");
        }
    }
}
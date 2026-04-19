using System;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Non‑ASCII text that is not allowed in Binary mode
        string nonAsciiText = "犬";

        // Attempt to generate a QR barcode in Binary mode
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set Binary encoding mode
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // This assignment will trigger validation and throw if the text contains non‑ASCII characters
                generator.CodeText = nonAsciiText;

                // Save the barcode (won't be reached if exception is thrown)
                generator.Save("qr_binary.bmp");
                Console.WriteLine("Barcode generated successfully.");
            }
        }
        catch (InvalidCodeException ex)
        {
            // Specific Aspose exception for invalid characters
            Console.WriteLine("Error: Unsupported encoding mode for the provided text.");
            Console.WriteLine("Details: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Fallback for any other unexpected errors
            Console.WriteLine("An unexpected error occurred:");
            Console.WriteLine(ex.Message);
        }
    }
}
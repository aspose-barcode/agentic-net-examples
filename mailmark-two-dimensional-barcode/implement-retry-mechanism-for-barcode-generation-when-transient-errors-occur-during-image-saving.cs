using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Output file path
        const string outputPath = "barcode.png";

        // Maximum number of retry attempts
        const int maxRetries = 3;

        int attempt = 0;
        bool saved = false;

        while (attempt < maxRetries && !saved)
        {
            attempt++;
            try
            {
                // Create and configure the barcode generator
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
                {
                    // Example of setting a property (optional)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                    // Save the barcode image to file
                    generator.Save(outputPath);
                }

                saved = true;
                Console.WriteLine($"Barcode saved successfully on attempt {attempt}.");
            }
            catch (Exception ex) // Catch any exception (including BarCodeException, IOException, etc.)
            {
                Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                if (attempt == maxRetries)
                {
                    Console.WriteLine("All retry attempts have been exhausted.");
                }
                // No delay between retries as per constraints
            }
        }
    }
}
// Title: Barcode generation with retry on temporary file system errors
// Description: Demonstrates how to generate a Code128 barcode and retry when IO exceptions occur, ensuring robust file handling.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode. It illustrates typical scenarios where developers need to create barcode images while handling transient file system issues, such as locked files or insufficient permissions, by implementing a retry mechanism.
// Prompt: Implement a retry mechanism for barcode generation when encountering temporary file system errors.
// Tags: barcode generation, retry, ioexception, code128, png, aspose.barcode, autosizemode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode image with a retry mechanism for handling temporary file system errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Attempts to generate a barcode image, retrying on IO exceptions.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        const string outputPath = "barcode.png";

        // Text to encode in the barcode
        const string codeText = "RetryTest";

        // Maximum number of retry attempts
        const int maxAttempts = 3;

        int attempt = 0;
        bool success = false;

        // Loop until the barcode is generated successfully or the max attempts are reached
        while (attempt < maxAttempts && !success)
        {
            attempt++;
            try
            {
                // Initialize the barcode generator with Code128 symbology and the desired text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Use auto size mode to let the library determine optimal dimensions
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                    // Set barcode and background colors (optional)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Ensure the output directory exists
                    string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Save the generated barcode image to the specified path
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Barcode generated successfully on attempt {attempt}.");
                success = true;
            }
            catch (IOException ioEx)
            {
                // Handle temporary file system errors by logging and retrying
                Console.WriteLine($"IO exception on attempt {attempt}: {ioEx.Message}");
                if (attempt >= maxAttempts)
                {
                    Console.WriteLine("Maximum retry attempts reached. Generation failed.");
                }
            }
            catch (Exception ex)
            {
                // Log unexpected errors and abort further attempts
                Console.WriteLine($"Unexpected error: {ex.Message}");
                break;
            }
        }

        // Exit with code 0 for success, 1 for failure
        Environment.Exit(success ? 0 : 1);
    }
}
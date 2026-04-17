using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // for BarCodeImageFormat
using Aspose.BarCode.Generation; // for MaxiCodeMode

class Program
{
    static void Main()
    {
        // Simulated API request payload containing the desired MaxiCode mode as a string.
        // In a real scenario this could come from JSON, query string, etc.
        string requestedMode = "Mode5";

        // Try to parse the string to the MaxiCodeMode enum.
        if (!Enum.TryParse<MaxiCodeMode>(requestedMode, out var maxiCodeMode))
        {
            throw new ArgumentException($"Invalid MaxiCode mode: {requestedMode}");
        }

        // Create a barcode generator for MaxiCode with a sample codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Dynamically set the MaxiCode mode based on the request.
            generator.Parameters.Barcode.MaxiCode.Mode = maxiCodeMode;

            // Save the generated barcode to a memory stream as PNG.
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Optionally write the PNG to a file for verification.
                // The file will be created in the program's working directory.
                File.WriteAllBytes("MaxiCode.png", memoryStream.ToArray());
            }
        }

        Console.WriteLine("MaxiCode barcode generated with mode: " + maxiCodeMode);
    }
}
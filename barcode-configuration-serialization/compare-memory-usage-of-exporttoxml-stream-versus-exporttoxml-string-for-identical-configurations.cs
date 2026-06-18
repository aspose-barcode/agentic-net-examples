using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates memory usage measurement for Aspose.BarCode ExportToXml methods.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator with a sample configuration (Code128, value "123456")
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set some non-default visual parameters
            generator.Parameters.Barcode.BarColor = Color.Red;               // Red barcode color
            generator.Parameters.ImageWidth.Point = 200f;                    // Width in points
            generator.Parameters.ImageHeight.Point = 100f;                   // Height in points
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation; // Auto-size mode

            // Ensure a clean memory state before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // ------------------------------
            // Measure memory usage for ExportToXml(string)
            // ------------------------------

            // Create a temporary file path for the XML export
            string tempFile = Path.GetTempFileName();

            // Record memory before the operation
            long beforeString = GC.GetTotalMemory(true);

            // Export barcode to XML file
            generator.ExportToXml(tempFile);

            // Record memory after the operation
            long afterString = GC.GetTotalMemory(true);

            // Calculate memory difference
            long diffString = afterString - beforeString;

            // ------------------------------
            // Measure memory usage for ExportToXml(Stream)
            // ------------------------------

            long diffStream;
            using (var ms = new MemoryStream())
            {
                // Reset memory counters before the stream export
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // Record memory before the operation
                long beforeStream = GC.GetTotalMemory(true);

                // Export barcode to memory stream
                generator.ExportToXml(ms);

                // Record memory after the operation
                long afterStream = GC.GetTotalMemory(true);

                // Calculate memory difference
                diffStream = afterStream - beforeStream;
            }

            // Output the memory differences to the console
            Console.WriteLine("Memory used by ExportToXml(string): {0} bytes", diffString);
            Console.WriteLine("Memory used by ExportToXml(Stream): {0} bytes", diffStream);

            // Clean up the temporary file created earlier
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
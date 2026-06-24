using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSizeToggleDemo
{
    /// <summary>
    /// Demonstrates generating barcodes with dimensions specified in either pixels or millimeters.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a barcode image using the specified size unit and saves it to the given path.
        /// </summary>
        /// <param name="mode">The unit mode (Pixels or Millimeters) for width and height.</param>
        /// <param name="widthValue">The width value in the selected unit.</param>
        /// <param name="heightValue">The height value in the selected unit.</param>
        /// <param name="outputPath">The file path where the barcode image will be saved.</param>
        static void GenerateBarcode(UnitMode mode, float widthValue, float heightValue, string outputPath)
        {
            // Create a Code128 barcode generator with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
            {
                // Optional: set background and bar colors.
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Apply size based on the selected unit mode.
                if (mode == UnitMode.Pixels)
                {
                    // Width and height are interpreted as pixels.
                    generator.Parameters.ImageWidth.Pixels = widthValue;
                    generator.Parameters.ImageHeight.Pixels = heightValue;
                }
                else // Millimeters
                {
                    // Width and height are interpreted as millimeters.
                    generator.Parameters.ImageWidth.Millimeters = widthValue;
                    generator.Parameters.ImageHeight.Millimeters = heightValue;
                }

                // Save the barcode image in PNG format.
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved ({mode}): {outputPath}");
            }
        }

        /// <summary>
        /// Entry point of the demo application. Generates barcodes using both pixel and millimeter dimensions.
        /// </summary>
        static void Main()
        {
            // NOTE: Full UI integration (e.g., WinForms/WPF) cannot be demonstrated in this console
            // application. The core logic for toggling between Pixels and Millimeters is shown below.

            // Example dimensions in pixels.
            float widthPixels = 300f;
            float heightPixels = 150f;

            // Corresponding dimensions in millimeters (approx. 300px and 150px at 96 DPI).
            float widthMillimeters = 80f;
            float heightMillimeters = 40f;

            // Generate barcode using pixel dimensions.
            GenerateBarcode(
                UnitMode.Pixels,
                widthPixels,
                heightPixels,
                "barcode_pixels.png");

            // Generate barcode using millimeter dimensions.
            GenerateBarcode(
                UnitMode.Millimeters,
                widthMillimeters,
                heightMillimeters,
                "barcode_millimeters.png");

            // End of demo.
            Console.WriteLine("Demo completed.");
        }
    }

    /// <summary>
    /// Simple enum to represent the unit mode for barcode dimensions.
    /// </summary>
    enum UnitMode
    {
        Pixels,
        Millimeters
    }
}
using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

namespace BarcodeProcessorDemo
{
    /// <summary>
    /// Holds common barcode generation settings and provides helper methods to apply them.
    /// </summary>
    public class ProcessorSettings
    {
        // Symbology to use (e.g., EncodeTypes.Code128).
        public BaseEncodeType Symbology { get; set; } = EncodeTypes.Code128;

        // Text to encode.
        public string CodeText { get; set; } = "Sample123";

        // Optional bar height (in points). Applies to 1D barcodes when AutoSizeMode is None.
        public float? BarHeight { get; set; }

        // Optional X-dimension (module width) in points.
        public float? XDimension { get; set; }

        // Optional checksum enable/disable.
        public EnableChecksum? ChecksumEnabled { get; set; }

        // Optional image width/height when using AutoSizeMode.Interpolation or Nearest.
        public float? ImageWidth { get; set; }
        public float? ImageHeight { get; set; }

        // Optional auto-size mode.
        public AutoSizeMode? AutoSizeMode { get; set; }

        // Optional colors.
        public Color? BarColor { get; set; }
        public Color? BackColor { get; set; }

        /// <summary>
        /// Applies the stored settings to an existing <see cref="BarcodeGenerator"/> instance.
        /// </summary>
        /// <param name="generator">The generator to configure.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="generator"/> is null.</exception>
        public void Apply(BarcodeGenerator generator)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));

            // Set the text to encode.
            generator.CodeText = CodeText;

            // Apply optional numeric settings if they have values.
            if (BarHeight.HasValue)
                generator.Parameters.Barcode.BarHeight.Point = BarHeight.Value;

            if (XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = XDimension.Value;

            if (ChecksumEnabled.HasValue)
                generator.Parameters.Barcode.IsChecksumEnabled = ChecksumEnabled.Value;

            if (ImageWidth.HasValue)
                generator.Parameters.ImageWidth.Point = ImageWidth.Value;

            if (ImageHeight.HasValue)
                generator.Parameters.ImageHeight.Point = ImageHeight.Value;

            // Apply optional enum settings.
            if (AutoSizeMode.HasValue)
                generator.Parameters.AutoSizeMode = AutoSizeMode.Value;

            // Apply optional color settings.
            if (BarColor.HasValue)
                generator.Parameters.Barcode.BarColor = BarColor.Value;

            if (BackColor.HasValue)
                generator.Parameters.BackColor = BackColor.Value;
        }

        /// <summary>
        /// Creates a new <see cref="BarcodeGenerator"/> with the configured symbology and applies all settings.
        /// </summary>
        /// <returns>A configured <see cref="BarcodeGenerator"/> instance.</returns>
        public BarcodeGenerator CreateGenerator()
        {
            // Initialise generator with the selected symbology and code text.
            var generator = new BarcodeGenerator(Symbology, CodeText);
            // Apply all optional settings.
            Apply(generator);
            return generator;
        }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the demo application. Generates a QR code using predefined settings.
        /// </summary>
        static void Main()
        {
            // Define reusable processor settings.
            var settings = new ProcessorSettings
            {
                Symbology = EncodeTypes.QR,
                CodeText = "https://example.com",
                XDimension = 2f,
                ImageWidth = 300f,
                ImageHeight = 300f,
                AutoSizeMode = AutoSizeMode.Interpolation,
                BarColor = Color.DarkBlue,
                BackColor = Color.White
            };

            // Generate barcode using the wrapper.
            using (var generator = settings.CreateGenerator())
            {
                // Define output file path.
                string outputPath = "barcode.png";

                // Save the generated barcode image.
                generator.Save(outputPath);

                // Inform the user where the file was saved.
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
    }
}
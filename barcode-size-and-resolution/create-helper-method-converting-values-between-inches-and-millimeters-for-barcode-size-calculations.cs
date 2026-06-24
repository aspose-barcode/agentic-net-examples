using System;

namespace BarcodeSizeHelper
{
    /// <summary>
    /// Entry point for the BarcodeSizeHelper console application.
    /// Demonstrates conversion between inches and millimeters.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method – performs sample conversions and writes results to the console.
        /// </summary>
        static void Main()
        {
            // Define a value in inches to convert.
            float inches = 2f;

            // Convert inches to millimeters using the UnitConverter helper.
            float millimeters = UnitConverter.InchesToMillimeters(inches);

            // Output the conversion result.
            Console.WriteLine($"{inches} inches = {millimeters} mm");

            // Define a value in millimeters to convert.
            float mmValue = 50f;

            // Convert millimeters to inches using the UnitConverter helper.
            float inchesValue = UnitConverter.MillimetersToInches(mmValue);

            // Output the conversion result.
            Console.WriteLine($"{mmValue} mm = {inchesValue} inches");
        }
    }

    /// <summary>
    /// Provides static methods for converting between inches and millimeters.
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// Converts a measurement in inches to millimeters.
        /// </summary>
        /// <param name="inches">The value in inches.</param>
        /// <returns>The equivalent value in millimeters.</returns>
        public static float InchesToMillimeters(float inches)
        {
            // 1 inch equals 25.4 millimeters.
            return inches * 25.4f;
        }

        /// <summary>
        /// Converts a measurement in millimeters to inches.
        /// </summary>
        /// <param name="millimeters">The value in millimeters.</param>
        /// <returns>The equivalent value in inches.</returns>
        public static float MillimetersToInches(float millimeters)
        {
            // Divide by 25.4 to convert millimeters back to inches.
            return millimeters / 25.4f;
        }
    }
}
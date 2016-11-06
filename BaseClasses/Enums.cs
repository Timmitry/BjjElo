namespace BaseClasses
{
    public enum Result
    {
        Loss = 0,
        Draw = 1,
        Win = 2
    }



    /// <summary>
    /// Wieght divisions.
    /// </summary>
    public enum WeightDivision
    {
        /// <summary>
        /// Up to 57.0 kg / 126.5 lbs.
        /// </summary>
        Roosterweight,

        /// <summary>
        /// Up to 64.0 kg / 141.0 lbs.
        /// </summary>
        LightFeatherweigh,

        /// <summary>
        /// Up to 70.0 kg / 154.0 lbs.
        /// </summary>
        Featherweight,

        /// <summary>
        /// Up to 76.0 kg / 167.5 lbs.
        /// </summary>
        Lightweight,

        /// <summary>
        /// Up to 82.3 kg / 181.0 lbs.
        /// </summary>
        Middleweight,

        /// <summary>
        /// Up to 88.3 kg / 194.5 lbs.
        /// </summary>
        MediumHeavyweight,

        /// <summary>
        /// Up to 94.3 kg / 207.5 lbs.
        /// </summary>
        Heavyweight,

        /// <summary>
        /// Up to 100.5 kg / 221.0 lbs.
        /// </summary>
        SuperHeavyweight,

        /// <summary>
        /// Over 100.5 kg / 221.0 lbs.
        /// </summary>
        UltraHeavyweight,

        /// <summary>
        /// Absolute or open class (all weight divisions mixed).
        /// </summary>
        Absolute
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjjEloGui
{
    internal class Constants
    {
        /// <summary>
        /// A measure for the difference of two fighters' strengths.
        /// </summary>
        /// <remarks>
        /// If the two fighters' Elo ratings differ by that value,
        /// the fighter with the higher value has a 91% change of winning and 9% chance of loosing.
        /// If the difference is half the value, his chances of winning are 76%.
        /// </remarks>
        internal const int EloDifference = 400;

        /// <summary>
        /// The 'k'-Factor in the Elo-formula.
        /// Designates the maximum number of points a fighter can win or lose in a single match.
        /// </summary>
        internal const int EloFactor = 32;

        /// <summary>
        /// The percentage of points a fighter gets for a win by points.
        /// Must be between 0.5 (draw) and 1.0 (win by submission).
        /// </summary>
        internal const double WinByPointsFactor = 0.75;
    }
}

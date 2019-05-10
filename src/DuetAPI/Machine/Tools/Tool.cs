﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DuetAPI.Machine
{
    /// <summary>
    /// Information about a configured tool
    /// </summary>
    public class Tool : ICloneable
    {
        /// <summary>
        /// Number of the tool
        /// </summary>
        public int Number { get; set; }
        
        /// <summary>
        /// Active temperatures of the associated heaters (in C)
        /// </summary>
        public float[] Active { get; set; } = new float[0];
        
        /// <summary>
        /// Standby temperatures of the associated heaters (in C)
        /// </summary>
        public float[] Standby { get; set; } = new float[0];
        
        /// <summary>
        /// Name of the tool or null if unset
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Name of the currently loaded filament
        /// </summary>
        public string Filament { get; set; }
        
        /// <summary>
        /// List of associated fans (indices)
        /// </summary>
        public int[] Fans { get; set; } = new int[0];
        
        /// <summary>
        /// List of associated heaters (indices)
        /// </summary>
        public int[] Heaters { get; set; } = new int[0];
        
        /// <summary>
        /// Mix ratios of the associated extruder drives
        /// </summary>
        public float[] Mix { get; set; } = new float[0];
        
        /// <summary>
        /// Associated spindle (index)
        /// </summary>
        public int Spindle { get; set; } = -1;
        
        /// <summary>
        /// Associated axes. At present only X and Y can be mapped per tool.
        /// </summary>
        /// <remarks>
        /// The order is the same as the visual axes, so by default the layout is
        /// [
        ///   [0],        // X
        ///   [1]         // Y
        /// ]
        /// </remarks>
        public List<int[]> Axes { get; set; } = new List<int[]>();

        /// <summary>
        /// Axis offsets (in mm)
        /// This list is in the same order as <see cref="Move.Axes"/>
        /// </summary>
        /// <seealso cref="Axis"/>
        public float[] Offsets { get; set; } = new float[0];                    // same order as Move.Axes

        /// <summary>
        /// Bitmap of the probed axis offsets
        /// </summary>
        public int OffsetsProbed { get; set; }

        /// <summary>
        /// Creates a clone of this instance
        /// </summary>
        /// <returns>A clone of this instance</returns>
        public object Clone()
        {
            return new Tool
            {
                Number = Number,
                Active = (float[])Active.Clone(),
                Standby = (float[])Standby.Clone(),
                Name = (Name != null) ? string.Copy(Name) : null,
                Filament = (Filament != null) ? string.Copy(Filament) : null,
                Fans = (int[])Fans.Clone(),
                Heaters = (int[])Heaters.Clone(),
                Mix = (float[])Mix.Clone(),
                Spindle = Spindle,
                Axes = Axes.Select(subAxes => (int[])subAxes.Clone()).ToList(),
                Offsets = (float[])Offsets.Clone()
            };
        }
    }
}
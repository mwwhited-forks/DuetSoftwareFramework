﻿using System;
using System.Text.Json;

namespace DuetAPI.ObjectModel
{
    /// <summary>
    /// Information about a filament monitor
    /// </summary>
    public class FilamentMonitor : ModelObject
    {
        /// <summary>
        /// Indicates if this filament monitor is enabled
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
			set => SetPropertyValue(ref _enabled, value);
        }
        private bool _enabled;

        /// <summary>
        /// Last reported status of this filament monitor
        /// </summary>
        public FilamentMonitorStatus Status
        {
            get => _status;
            set => SetPropertyValue(ref _status, value);
        }
        private FilamentMonitorStatus _status = FilamentMonitorStatus.NoDataReceived;

        /// <summary>
        /// Type of this filament monitor
        /// </summary>
        public FilamentMonitorType Type
        {
            get => _type;
			protected set => SetPropertyValue(ref _type, value);
        }
        private FilamentMonitorType _type = FilamentMonitorType.Unknown;

        /// <summary>
        /// Figure out the required type for the given filament monitor type
        /// </summary>
        /// <param name="type">Filament monitor type</param>
        /// <returns>Required type</returns>
        private static Type GetFilamentMonitorType(FilamentMonitorType type)
        {
            switch (type)
            {
                case FilamentMonitorType.Laser: return typeof(LaserFilamentMonitor);
                case FilamentMonitorType.Pulsed: return typeof(PulsedFilamentMonitor);
                case FilamentMonitorType.RotatingMagnet: return typeof(RotatingMagnetFilamentMonitor);
                default: return typeof(FilamentMonitor);
            }
        }

        /// <summary>
        /// Update this instance from a given JSON element
        /// </summary>
        /// <param name="jsonElement">Element to update this intance from</param>
        /// <param name="ignoreSbcProperties">Whether SBC properties are ignored</param>
        /// <returns>Updated instance</returns>
        /// <exception cref="JsonException">Failed to deserialize data</exception>
        public override IModelObject UpdateFromJson(JsonElement jsonElement, bool ignoreSbcProperties)
        {
            if (jsonElement.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (jsonElement.TryGetProperty("type", out JsonElement nameProperty))
            {
                FilamentMonitorType filamentMonitorType = (FilamentMonitorType)JsonSerializer.Deserialize(nameProperty.GetRawText(), typeof(FilamentMonitorType));
                Type requiredType = GetFilamentMonitorType(filamentMonitorType);
                if (GetType() != requiredType)
                {
                    FilamentMonitor newInstance = (FilamentMonitor)Activator.CreateInstance(requiredType);
                    return newInstance.UpdateFromJson(jsonElement, ignoreSbcProperties);
                }
            }
            return base.UpdateFromJson(jsonElement, ignoreSbcProperties);
        }
    }
}

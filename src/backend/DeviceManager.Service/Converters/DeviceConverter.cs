namespace DeviceManager.Service.Converters;

internal static class DeviceConverter
{
	internal static DeviceManager.Repository.Models.Device FromDto(DeviceManager.Service.Dtos.Device device)
	{
		if (device == null)
			throw new ArgumentNullException(nameof(device));

		return new Repository.Models.Device()
		{
			ID = device.ID,
			Name = device.Name,
			DeviceTypeId = device.DeviceTypeId,
			AdvancedEnvironmentalConditions = device.AdvancedEnvironmentalConditions,
			FailSafe = device.FailSafe,
			InsertInto19InchCabinet = device.InsertInto19InchCabinet,
			InstallationPosition = device.InstallationPosition,
			MotionEnable = device.MotionEnable,
			PositionAxisNumber = device.PositionAxisNumber,
			RotationAxisNumber= device.RotationAxisNumber,
			SimaticCatalog= device.SimaticCatalog,
			SiplusCatalog= device.SiplusCatalog,
			TempMax = device.TempMax,
			TempMin = device.TempMin
		};
	}

	internal static DeviceManager.Service.Dtos.Device ToDto(DeviceManager.Repository.Models.Device device)
	{
		if (device == null)
			throw new ArgumentNullException(nameof(device));

		return new Dtos.Device()
		{
			ID = device.ID,
			Name = device.Name,
			DeviceTypeId = device.DeviceTypeId,
			AdvancedEnvironmentalConditions = device.AdvancedEnvironmentalConditions,
			FailSafe = device.FailSafe,
			InsertInto19InchCabinet = device.InsertInto19InchCabinet,
			InstallationPosition = device.InstallationPosition,
			MotionEnable = device.MotionEnable,
			PositionAxisNumber = device.PositionAxisNumber,
			RotationAxisNumber = device.RotationAxisNumber,
			SimaticCatalog = device.SimaticCatalog,
			SiplusCatalog = device.SiplusCatalog,
			TempMax = device.TempMax,
			TempMin = device.TempMin
		};
	}
}

using System.Runtime.Serialization;

namespace DeviceManager.Service.Dtos;

public class Device
{
	public string ID { get; set; }

	public string Name { get; set; }

	public string? DeviceTypeId { get; set; }

	public bool FailSafe { get; set; }

	public int TempMin { get; set; }

	public int TempMax { get; set; }

	public string? InstallationPosition { get; set; }

	public bool InsertInto19InchCabinet { get; set; }

	public bool MotionEnable { get; set; }

	public bool SiplusCatalog { get; set; }

	public bool SimaticCatalog { get; set; }

	public int RotationAxisNumber { get; set; }

	public int PositionAxisNumber { get; set; }

	public bool? TerminalElement { get; set; }

	public bool? AdvancedEnvironmentalConditions { get; set; }

	public bool IsValid()
	{
		bool valid = true;

		if(string.IsNullOrWhiteSpace(ID))
			valid = false;
		if(string.IsNullOrWhiteSpace(Name))
			valid = false;

		return valid;
	}
}
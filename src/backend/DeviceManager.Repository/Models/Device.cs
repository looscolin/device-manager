using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace DeviceManager.Repository.Models;

public class Device
{
	[Required]
	[DataMember(Name = "id")]
	public string ID { get; set; }

	[DataMember(Name = "deviceTypeId")]
	public string? Name { get; set; }

	[DataMember(Name = "device")]
	public string? DeviceTypeId { get; set; }

	[DataMember(Name = "failSafe")]
	public bool FailSafe { get; set; }

	[DataMember(Name = "tempMin")]
	public int TempMin { get; set; }

	[DataMember(Name = "tempMax")]
	public int TempMax { get; set; }

	[RegularExpression(@"/^(horizontal)|(vertical)$/")]
	[DataMember(Name = "installationPosition")]
	public string? InstallationPosition { get; set; }

	[DataMember(Name = "insertInto19InchCabinet")]
	public bool InsertInto19InchCabinet { get; set; }

	[DataMember(Name = "motionEnable")]
	public bool MotionEnable { get; set; }

	[DataMember(Name = "siplusCatalog")]
	public bool SiplusCatalog { get; set; }

	[DataMember(Name = "simaticCatalog")]
	public bool SimaticCatalog { get; set; }

	[DataMember(Name = "rotationAxisNumber")]
	public int RotationAxisNumber { get; set; }

	[DataMember(Name = "positionAxisNumber")]
	public int PositionAxisNumber { get; set; }

	[DataMember(Name = "terminalElement")]
	public bool? TerminalElement { get; set; }

	[DataMember(Name = "advancedEnvironmentalConditions")]
	public bool? AdvancedEnvironmentalConditions { get; set; }
}
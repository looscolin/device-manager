using DeviceManager.Repository.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DeviceManager.Repository;

public class JsonDeviceRepository : IDeviceRepository
{
	private string jsonFileName = string.Empty;

	public JsonDeviceRepository(string jsonFileName)
	{
		if(string.IsNullOrWhiteSpace(jsonFileName))
			throw new ArgumentNullException(nameof(jsonFileName));

		this.jsonFileName = jsonFileName;
	}

	public bool ContainsDeviceById(string deviceID)
	{
		// If File exists, check it for ID, if not return false since there are no devices
		if (File.Exists(jsonFileName))
		{
			IEnumerable<Device>? devices = JsonSerializer.Deserialize<IEnumerable<Device>>(File.ReadAllText(jsonFileName));

			if (devices == null)
				return false;

			return devices.Any(d => d.ID.Equals(deviceID));
		}
		else
			return false;
	}

	public void CreateDevices(IEnumerable<Device> newDevices)
	{
		// If File exists, read it and append new devices, if not create new File
		if(File.Exists(jsonFileName))
		{
			List<Device>? devices = JsonSerializer.Deserialize<List<Device>>(File.ReadAllText(jsonFileName));

			newDevices.ToList().ForEach(d => devices.Add(d));

			var difference = devices.Count() - devices.DistinctBy(d => d.ID).Count();

			if(difference > 0)
			{
				string errorMessagePartOne = difference == 1 ? "One ID is duplicate." : $"{difference} ID's are duplicate.";

				throw new ArgumentException($"{errorMessagePartOne} ID's have to be Unique!");
			}

			File.WriteAllText(jsonFileName, JsonSerializer.Serialize(devices));
		}
		else
		{
			File.WriteAllText(jsonFileName, JsonSerializer.Serialize(newDevices));
		}
	}

	public void DeleteDeviceById(string deviceID)
	{
		if (File.Exists(jsonFileName))
		{
			var devices = JsonSerializer.Deserialize<IList<Device>>(File.ReadAllText(jsonFileName));

			if(devices == null)
				throw new ArgumentException($"A Device with the ID {deviceID} doesn't exist");

			var newDevices = devices.Where(d => !d.ID.Equals(deviceID));

			File.WriteAllText(jsonFileName, JsonSerializer.Serialize(newDevices));
		}
		else
			throw new ArgumentException($"A Device with the ID {deviceID} doesn't exist");
	}

	public Device? GetDevice(string deviceID)
	{
		if (File.Exists(jsonFileName))
		{
			IEnumerable<Device>? devices = JsonSerializer.Deserialize<IEnumerable<Device>>(File.ReadAllText(jsonFileName));

			return devices?.SingleOrDefault(d => d.ID.Equals(deviceID));
		}
		else
			return default;
	}

	public IEnumerable<Device> GetDevices()
	{
		if(File.Exists(jsonFileName))
		{
			var devices = JsonSerializer.Deserialize<IEnumerable<Device>>(File.ReadAllText(jsonFileName));

			if(devices == null)
				return Enumerable.Empty<Device>();

			return devices;
		}
		else
			return Enumerable.Empty<Device>();
	}
}

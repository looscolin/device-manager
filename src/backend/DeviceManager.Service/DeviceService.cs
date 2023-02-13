using DeviceManager.Repository;
using DeviceManager.Service.Dtos;
using DeviceManager.Service.Converters;

namespace DeviceManager.Service;

public class DeviceService : IDeviceService
{
	private IDeviceRepository deviceRepository;

	public DeviceService(IDeviceRepository deviceRepository)
	{
		this.deviceRepository = deviceRepository;
	}

	public bool ContainsDeviceById(string deviceID)
	{
		return deviceRepository.ContainsDeviceById(deviceID);
	}

	public void CreateDevices(IEnumerable<Device> newDevices)
	{
		deviceRepository.CreateDevices(newDevices.Select(d => DeviceConverter.FromDto(d)));
	}

	public void DeleteDeviceById(string id)
	{
		deviceRepository.DeleteDeviceById(id);
	}

	public Device? GetDevice(string deviceID)
	{
		DeviceManager.Repository.Models.Device? deviceModel = deviceRepository.GetDevice(deviceID);

		if (default(DeviceManager.Repository.Models.Device) == deviceModel)
			return default(Device);
		else
			return DeviceConverter.ToDto(deviceModel);
	}

	public IEnumerable<Device> GetDevices()
	{
		var devices = deviceRepository.GetDevices();

		return devices.Select(d => DeviceConverter.ToDto(d));
	}
}
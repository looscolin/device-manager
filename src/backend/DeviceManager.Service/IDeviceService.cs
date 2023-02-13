using DeviceManager.Service.Dtos;

namespace DeviceManager.Service;

public interface IDeviceService
{
	IEnumerable<Device> GetDevices();

	Device? GetDevice(string deviceID);

	void CreateDevices(IEnumerable<Device> newDevices);

	void DeleteDeviceById(string deviceID);

	bool ContainsDeviceById(string deviceID);
}
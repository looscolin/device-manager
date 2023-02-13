using DeviceManager.Repository.Models;

namespace DeviceManager.Repository;

public interface IDeviceRepository
{
	IEnumerable<Device> GetDevices();

	Device? GetDevice(string deviceID);

	void CreateDevices(IEnumerable<Device> newDevices);

	void DeleteDeviceById(string deviceID);

	bool ContainsDeviceById(string deviceID);
}

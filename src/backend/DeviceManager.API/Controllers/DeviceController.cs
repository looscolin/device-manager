using DeviceManager.Service;
using Microsoft.AspNetCore.Mvc;
using DeviceManager.Service.Dtos;

namespace DeviceManager.API.Controllers;

[ApiController]
[Route("devices")]
public class DeviceController : ControllerBase
{
	private readonly ILogger<DeviceController> logger;
	private readonly IDeviceService deviceService;

	public DeviceController(ILogger<DeviceController> logger, IDeviceService deviceService)
	{
		this.logger = logger;
		this.deviceService = deviceService;
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[HttpPost("", Name = "CreateDevices")]
	public IActionResult CreateDevices(IEnumerable<DeviceManager.Service.Dtos.Device> devices)
	{
		logger.LogDebug($"Creating {devices.Count()} Devices");

		if (devices == null || devices == default(IEnumerable<DeviceManager.Service.Dtos.Device>) || devices.Count() <= 0)
			return BadRequest("Empty Body");

		try
		{
			deviceService.CreateDevices(devices);
		}
		catch(ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}

		return Ok();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet("{id}", Name = "GetDevice")]
	public IActionResult GetDevice(string id)
	{
		logger.LogDebug($"Getting Device with ID {id}");

		if (!deviceService.ContainsDeviceById(id))
			return NotFound();

		var device = deviceService.GetDevice(id);

		if (device != default(DeviceManager.Service.Dtos.Device))
			return Ok(device);
		else
			return NotFound();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpGet(Name = "GetDevices")]
	public IActionResult GetDevices()
	{
		logger.LogDebug($"Getting all Devices");

		var devices = deviceService.GetDevices();

		if (devices != Enumerable.Empty<Device>())
			return Ok(devices);
		else
			return NoContent();
	}

	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpDelete("{id}", Name = "DeleteDevice")]
	public IActionResult Delete(string id)
	{
		logger.LogDebug($"Deleting Device with ID {id}");

		if (!deviceService.ContainsDeviceById(id))
			return NotFound();

		deviceService.DeleteDeviceById(id);

		return Ok();
	}
}
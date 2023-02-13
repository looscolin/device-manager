$(document).ready(function () {
    loadData();
});

function GoToDevicesPage() {
    location.href = 'devices.html';
}

const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
  });

function loadData() {
    let deviceid = params.deviceid;

    if(deviceid != null)
    {
        $.get(config.server + "/devices/" + deviceid, function(data, status){
            let trueIcon = '<img class="h-6 w-6 mb-1 mt-1" src="../img/check.svg">';
            let falseIcon = '<img class="h-6 w-6 mb-1 mt-1" src="../img/red-x.svg">';

            $('#deviceName').append(data.name);
            $('#deviceType').append(data.deviceTypeId);

            $('#divFailsafe').append(data.failsafe ? trueIcon : falseIcon);
            $('#spanTemp').append(data.tempMin + ' - ' + data.tempMax + ' C°');
            $('#spanInstallPos').append(data.installationPosition);
            $('#div19Inch').append(data.insertInto19InchCabinet ? trueIcon : falseIcon);
            if(data.terminalElement != null) {
                $('#divTerminalAvailable').append(data.terminalElement ? trueIcon : falseIcon)
            } else {
                $('#divTerminalAvailable').append('Nicht vorhanden')
            }
            if(data.advancedEnvironmentalConditions != null) {
                $('#divAdvEnv').append(data.advancedEnvironmentalConditions ? trueIcon : falseIcon)
            } else {
                $('#divAdvEnv').append('Nicht vorhanden')
            }
        });
    }
    else
    {
        alert("Can't load Device data without a specified deviceid");
    }
}
$(document).ready(function () {
    $('#file').on("change", function () { handleFileChange(); });

    loadDevices();
});

function uploadDeviceJson() {
    $('#file').trigger('click');
}

function handleFileChange() {
    let file = document.querySelector('#file');

    if (!file.value.length) return;

    let reader = new FileReader();
    reader.onload = handleFile;
    reader.readAsText(file.files[0]);
}

function handleFile(event) {
    let str = event.target.result;
    let json = JSON.parse(str);

    $.ajax({
        contentType: 'application/json',
        data: JSON.stringify(json.devices),
        success: function (data) {
            loadDevices();
        },
        error: function (data) {
            alert(data.responseText);
        },
        processData: false,
        type: 'POST',
        url: config.server + '/devices'
    });

    $('#file').val('');

}

function deleteDevice(id) {
    // stop bubbling the event so openDevice won't be called afterwards
    window.event.stopPropagation();

    $.ajax({
        url: config.server + '/devices/' + id,
        type: 'DELETE',
        success: function(result) {
            $('[data-id="' + id + '"]').remove();
        }
    });
}

function openDevice(id) {
    location.href = 'device.html?deviceid=' + encodeURI(id);
}

function loadDevices() {
    $('#divDeviceContainer').empty();
    $('#divDeviceContainer').append('<div onclick="uploadDeviceJson()" class="flex items-center justify-center rounded-3xl bg-green-400 p-4 text-black transition-all duration-300 hover:cursor-pointer hover:rounded-sm hover:bg-green-800 hover:text-white"><a>Ger&auml;te hinzuf&uuml;gen</a></div>');

    $.get(config.server + "/devices", function (data, status) {
        data.map((device) => {
            $('#divDeviceContainer').append(createDeviceContainerItem(device.id, device.name, device.deviceTypeId, device.failSafe));
            $('[data-id="' + device.id + '"]').fadeIn();
        });

        if(data.length == 0) {
            $('#divDeviceContainer').append('<div class="flex items-center justify-center rounded-3xl bg-red-400 p-4 text-black transition-all duration-300 hover:rounded-sm hover:bg-red-800 hover:text-white"><a>Keine Ger&auml;te vorhanden</a></div>');
        }
    });
}

function createDeviceContainerItem(id, name, devicetype, failsafe) {
    let failsafeIconName = '';

    if (failsafe) {
        failsafeIconName = 'check';
    } else {
        failsafeIconName = 'red-x';
    }

    return '<div hidden="true" data-id="' + id + '" class="w-full items-center justify-center overflow-hidden rounded-3xl bg-blue-300 p-4 transition-all duration-300 hover:cursor-pointer hover:rounded-sm hover:bg-blue-700 hover:text-white">' +
        '<div onclick="openDevice(\'' + id + '\')" class="grid grid-cols-5 gap-1">' +
        '<div class="col-span-4 overflow-hidden font-lg font-bold">' + name + '</div>' +
        '<div class="col-span-1 h-6">' +
        '<img onclick="deleteDevice(\'' + id + '\')" class="fill-white ml-auto h-8 hover:h-12 transition-all duration-300" src="../img/trash.svg" />' +
        '</div>' +
        '<div class="col-span-5 overflow-hidden font-sm">' + devicetype + '</div>' +
        '<div class="col-span-4 overflow-hidden font-sm">Failsafe:</div>' +
        '<div class="col-span-1 h-6 ml-auto">' +
        '<img src="../img/' + failsafeIconName + '.svg" class="h-4" />' +
        '</div>' +
        '</div>' +
        '</div>';
}
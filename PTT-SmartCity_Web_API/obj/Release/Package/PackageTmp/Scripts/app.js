function btnEnvironment() {
    $.ajax({
        type: "POST",
        url: "/Application/Index",
        data: JSON.stringify({ view: 'EnvironmentSensor' }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert("Hello");
        }
    });
}
function Test(){
    this.Click=function (){
        alert('OK');
    };
    this.AppendTo = function (obj) {
        /*
        var btn = $('<button type="button">Test</button>');
        btn.click(function () {
            $.ajax({
                url: mainIPPort + '/test.abc',
                type: 'POST',
                headers: { "cache-control": "no-cache" },
                cache: false,
                data: 'module=test',
                success: function (string) {
                    alert(mainIPPort);
                    var getData = $.parseJSON(string);
                    $('#Div1').text(getData.khoa);
                },
                error: function () {
                    alert('Error');
                }
            });
        });
        btn.appendTo(obj);
        */
       $.ajax({
                url: mainIPPort + '/readlogo',
                type: 'POST',
                headers: { "cache-control": "no-cache" },
                cache: false,                
                success: function (string) {
                    alert(mainIPPort);
                    $('<img width="200" height="200" src="data:image/jpeg;base64,'+string+'"></img>').appendTo('body');
                },
                error: function () {
                    alert('Error');
                }
            });
    };
}


function Report(){
    var Object=this;
    this.InitReport=function (){
        $('#content').children().remove();
        $('<input id="datetimepicker" type="text" >').appendTo($('#content'));        
        $('#datetimepicker').datetimepicker({
                lang:'vi',                
                timepicker:false,
                format:'d/m/Y',
                closeOnDateSelect:true                
           });
         var btn= $('<button type="button" id="report_button_view">Xem Báo Cáo</Button>');
         btn.appendTo($('#content'));     
         
         var btnBack= $('<button type="button" id="report_button_back">Quay lại</Button>');
         btnBack.appendTo($('#content'));   
         btnBack.click(function (){
             main.AddFunction();
         });
         
         var reportContent=$('<div class="reportcontent" id="reportContent"></div>');
         reportContent.appendTo($('#content'));
    };
    this.LoadReport=function (){
        $.ajax({
            url: mainIPPort + '/readreport.php',
            type: 'POST',
            headers: { "cache-control": "no-cache" },
            cache: false,                
            success: function (string) {                
                var getData = $.parseJSON(string);
                if (getData!=null) {
                    $('#content').children().remove();                    
                    if (getData.length>0) {
                        var table=$('<table class="report_table"></table>');
                        var header=$(
                                    '<tr class="rowheader">'+
                                        '<th>STT</th>'+
                                        '<th>Hóa Đơn</th>'+
                                        '<th>Bàn</th>'+
                                        '<th>Tổng Tiền</th>'+
                                    '</tr>'
                                    );
                        header.appendTo(table);
                        table.appendTo($('#content'));
                        for (i=0;i<getData.length;i++){
                            var row=$(
                                    '<tr class="row'+(i%2)+'">'+
                                        '<td class="report_table_column1">'+(i+1)+'</td>'+
                                        '<td class="report_table_column2">'+getData[i].MaHoaDon+'</td>'+
                                        '<td class="report_table_column3">'+getData[i].TenBan+'</td>'+
                                        '<td class="report_table_column4">'+formatNumber(getData[i].TongTien)+'</td>'+
                                    '</tr>'
                                    );
                            row.appendTo(table);
                        }
                    }
                }
            },
            error: function () {                
                alert('Error');                
            }
        });
    };     
}
function ReportCustomer(){
    var Object=this;
    this.InitReport=function (){
        var report=new Report();
        report.InitReport();        
        
         $('#report_button_view').click(function (){
             Object.LoadReport($('#datetimepicker').val());
         });                          
    };
    this.LoadReport=function (date){        
        $.ajax({
            url: mainIPPort + '/readreportcustomer.php',
            type: 'POST',
            headers: { "cache-control": "no-cache" },
            cache: false,  
            data: 'date='+date,
            success: function (string) {                
                var getData = $.parseJSON(string);
                if (getData!=null) {
                    $('#reportContent').children().remove();                      
                    if (getData.length>0) {
                        var table=$('<table class="report_table"></table>');                        
                        var header=$(
                                    '<tr class="rowheader">'+
                                        '<th>STT</th>'+
                                        '<th>Tên Khách Hàng</th>'+                                        
                                        '<th>Tổng Tiền</th>'+
                                    '</tr>'
                                    );
                        header.appendTo(table);
                        table.appendTo($('#reportContent'));
                        for (i=0;i<getData.length;i++){
                            var row=$(
                                    '<tr class="row'+(i%2)+'">'+
                                        '<td class="report_table_column1">'+(i+1)+'</td>'+
                                        '<td class="report_table_column2">'+getData[i].TenKhachHang+'</td>'+                                        
                                        '<td class="report_table_column4">'+formatNumber(getData[i].TongTien)+'</td>'+
                                    '</tr>'
                                    );
                            row.appendTo(table);
                        }
                    }
                    else{
                        $('<div class="report_daily_nodata">Không có dữ liệu</div>').appendTo($('#reportContent'));                    
                    }
                }
            },
            error: function () {                
                alert('Error');                
            }
        });
    };  
}
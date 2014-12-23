function ReportDaiLy(){
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
            url: mainIPPort + '/readreport.php',
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
                        table.appendTo($('#reportContent'));
                        for (i=0;i<getData.length;i++){                                                                                    
                            $('<tr class="row1">'+
                                        '<td class="report_daily_column1">Ngày</td>'+                                        
                                        '<td class="report_daily_column2">'+toDateString(getData[i].NgayBan)+'</td>'+                                        
                                    '</tr>').appendTo(table);                            
                            $('<tr class="row0">'+
                                        '<td class="report_daily_column1">Tiền Mặt</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].TienMat)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row1">'+
                                        '<td class="report_daily_column1">Tiền Thẻ</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].TienThe)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row0">'+
                                        '<td class="report_daily_column1">Tiền Trả Lại</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].TienTraLai)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row1">'+
                                        '<td class="report_daily_column1">Giảm Giá</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].GiamGia)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row0">'+
                                        '<td class="report_daily_column1">Chiết Khấu</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].ChietKhau)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row1">'+
                                        '<td class="report_daily_column1">Tiền Típ</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].TienBo)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row0">'+
                                        '<td class="report_daily_column1">Phí Dịch Vụ</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].PhiDichVu)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row1">'+
                                        '<td class="report_daily_column1">Tiền Khách Hàng</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].TienKhacHang)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row0">'+
                                        '<td class="report_daily_column1">Tổng Tiền</td>'+
                                        '<td class="report_daily_column2">'+formatNumber(getData[i].TongTien)+'</td>'+                                        
                                    '</tr>').appendTo(table);
                            $('<tr class="row1">'+
                                        '<td class="report_daily_column1">Số Hóa Đơn</td>'+
                                        '<td class="report_daily_column2">'+getData[i].SoHoaDon+'</td>'+                                        
                                    '</tr>').appendTo(table);
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
function Main(){
    var Object=this;
    var HeightTop=60;
    var HeightBot=50;
    this.ReportIcon=null;
    this.InitLayout=function (){
        var top=$('<div class="top"></div>');
        top.width('100%');
        top.height(HeightTop+'px');
        Object.LoadTop(top);
        
        var content=$('<div id="content"></div>');
        //Object.AddFunction(content);
        
        var botton=$('<div class="botton"></div>');
        botton.width('100%');
        botton.height(HeightBot+'px');
        Object.LoadBotton(botton);
        
        top.appendTo('body');
        content.appendTo('body');
        botton.appendTo('body');
    };
    this.AddFunction=function (){  
        var content=$('#content');
        content.children().remove();
       var function1=
            $('<div class="div_function">\n\
                <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
                <div>\n\
                    <h2>Báo cáo doanh số</h2>\n\
                    <p>Doanh số trong ngày</p>\n\
                </div>\n\
             </div>');
        function1.appendTo(content);
        function1.click(function (){      
            Object.FunctionClick(1);
        });
       var function2=
        $('<div class="div_function">\n\
             <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
             <div>\n\
                 <h2>Báo cáo Doanh Số Theo Nhóm</h2>\n\
                 <p>Chi tiết về số lượng,tổng tiền từng nhóm,...</p>\n\
             </div>\n\
          </div>');        
        function2.appendTo(content);
        function2.click(function (){      
            Object.FunctionClick(2);
        });
       var function3=       
        $('<div class="div_function">\n\
             <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
             <div>\n\
                 <h2>Báo cáo doanh số theo món</h2>\n\
                 <p>Chi tiết tên món, số lượng, tổng tiền,...</p>\n\
             </div>\n\
          </div>');
        function3.appendTo(content); 
        function3.click(function (){      
            Object.FunctionClick(3);
        });
        
        var function4=       
        $('<div class="div_function">\n\
             <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
             <div>\n\
                 <h2>Báo cáo thanh toán thẻ</h2>\n\
                 <p>Chi tiết tên thẻ,tổng tiền,...</p>\n\
             </div>\n\
          </div>');
        function4.appendTo(content); 
        function4.click(function (){      
            Object.FunctionClick(4);
        });
        var function5=       
        $('<div class="div_function">\n\
             <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
             <div>\n\
                 <h2>Báo cáo khách hàng</h2>\n\
                 <p>Chi tiết tên khách hàng,tổng tiền,...</p>\n\
             </div>\n\
          </div>');
        function5.appendTo(content); 
        function5.click(function (){      
            Object.FunctionClick(5);
        });
        
        var function6=       
        $('<div class="div_function">\n\
             <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
             <div>\n\
                 <h2>Lịch sử bán hàng</h2>\n\
                 <p>Chi tiết từng hóa đơn,...</p>\n\
             </div>\n\
          </div>');
        function6.appendTo(content); 
        function6.click(function (){      
            Object.FunctionClick(6);
        });
        
        var function7=       
        $('<div class="div_function">\n\
             <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
             <div>\n\
                 <h2>Báo cáo kho hàng</h2>\n\
                 <p>Chi tiết nhập,xuất,tồn kho từng món,...</p>\n\
             </div>\n\
          </div>');
        function7.appendTo(content); 
        function7.click(function (){      
            Object.FunctionClick(7);
        });
    };
    this.LoadTop=function (top){
        //readinfo
        $.ajax({
            url: mainIPPort + '/readinfo.php',
            type: 'POST',
            headers: { "cache-control": "no-cache" },
            cache: false,            
            success: function (string) {                                
                var getData = $.parseJSON(string);                
                var logo= $('<img class="top_logo" src="data:image/jpeg;base64,'+getData.Hinh64+'"></img>');
                logo.appendTo(top);
                logo.width(HeightTop+'px');
                logo.height(HeightTop+'px');
                var content=$('<div class="top_content"></div>');
                $('<h2>'+getData.TenCongTy+'</h2>').appendTo(content);
                $('<p>'+getData.DiaChi+'</p>').appendTo(content);
                $('<p>'+getData.DienThoai+'</p>').appendTo(content);
                content.appendTo(top);
            },
            error: function () {
                alert('Error');
            }
        });        
    };
    this.LoadBotton=function (botton){
        botton.text('Copyright © 2014 Khoa Tran');
    };
    this.InitImage=function (){
        $.ajax({
            url: mainIPPort + '/readreporticon.php',
            type: 'POST',
            headers: { "cache-control": "no-cache" },
            cache: false,                
            success: function (string) {
                Object.ReportIcon=string;                
                Object.AddFunction();
            },
            error: function () {                
                alert('Error');                
            }
        });
    };
    this.FunctionClick=function (num){
        switch (num){
            case 1:
                var reportDaily=new ReportDaiLy();
                reportDaily.InitReport();
                break;
            case 2:
                var reportGroup=new ReportGroup();
                reportGroup.InitReport();
                break;
            case 3:
                var reportItem=new ReportItem();
                reportItem.InitReport();
                break; 
            case 4:
                var reportCard=new ReportCard();
                reportCard.InitReport();
                break; 
            case 5:
                var reportcustomer=new ReportCustomer();
                reportcustomer.InitReport();
                break; 
            case 6:
                var reportHistory=new ReportHistory();
                reportHistory.InitReport();
                break; 
            case 7:
                var reportStock=new ReportStock();
                reportStock.InitReport();
                break; 
            default :
                break;
        }
    };
}


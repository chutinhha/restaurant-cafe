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
    this.AddFunction=function (content){              
       var function1=
            $('<div class="div_function">\n\
                <img src="data:image/jpeg;base64,'+Object.ReportIcon+'"></img>\n\
                <div>\n\
                    <h2>Báo cáo bán hàng</h2>\n\
                    <p>Doanh thu của tát cả các sản phẩm trong ngày</p>\n\
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
                 <h2>Báo cáo tồn kho</h2>\n\
                 <p>Kiểm tra kho hàng, mất kho,...</p>\n\
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
                 <h2>Báo cáo định lượng</h2>\n\
                 <p>Kiểm tra số lượng các nguyên liệu khi chế biến món ăn</p>\n\
             </div>\n\
          </div>');
        function3.appendTo(content); 
        function3.click(function (){      
            Object.FunctionClick(3);
        });
    };
    this.LoadTop=function (top){
        //readinfo
        $.ajax({
            url: mainIPPort + '/readinfo',
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
            url: mainIPPort + '/readreporticon',
            type: 'POST',
            headers: { "cache-control": "no-cache" },
            cache: false,                
            success: function (string) {
                Object.ReportIcon=string;                
                Object.AddFunction($('#content'));
            },
            error: function () {                
                alert('Error');                
            }
        });
    };
    this.FunctionClick=function (num){
        report.LoadReport();
    };
}


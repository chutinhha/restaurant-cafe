function formatNumber (num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
};
function toDateString(dateStr){
  var date=new Date(dateStr);
  return date.getDate()+'/'+(date.getMonth()+1)+'/'+date.getFullYear();
};
function toTimeString(dateStr){
  var date=new Date(dateStr);
  return date.toLocaleTimeString();
};
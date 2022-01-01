
document.getElementById("btnCombobox").addEventListener("click",function(){
    document.getElementsByClassName("m-combobox-data")[0].style.display = 'block';
});
var comboboxItems = document.getElementsByClassName("m-combobox-item");
for (const item of comboboxItems) {
    item.addEventListener("click",function(){
        document.getElementsByClassName("m-combobox-data")[0].style.display='none';
    })
}

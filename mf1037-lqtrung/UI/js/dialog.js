document.getElementById("btnShowDialog").addEventListener("click",function(){
    document.getElementById("dialog").style.display = "block";
})

document.getElementsByClassName("m-dialog-close")[0].addEventListener("click",function(){
    document.getElementById("dialog").style.display = "none";
})
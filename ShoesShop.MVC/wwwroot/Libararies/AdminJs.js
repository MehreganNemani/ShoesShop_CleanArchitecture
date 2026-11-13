function openNav() {
    document.getElementById("mySidepanel").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidepanel").style.width = "0";
} 

var mytoast = document.getElementById('myToast');
var mybtn = document.getElementById('myBtn');
if (mybtn) {
     mybtn.addEventListener('click',function() {
    var toast = new bootstrap.Toast(mytoast);
    toast.show();
});
}
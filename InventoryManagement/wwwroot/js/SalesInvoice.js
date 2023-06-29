var today = new Date();
var dd = today.getDate();
var mm = today.getMonth() + 1; //January is 0!
var yyyy = today.getFullYear();

if (dd < 10) {
    dd = '0' + dd;
}

if (mm < 10) {
    mm = '0' + mm;
}

today = yyyy + '-' + mm + '-' + dd;
document.getElementById("purchaseDate").setAttribute("min", today);
$('#AccountReceivableTab').addClass("active");

/*var table = document.getElementById("table"), sumVal = 0;

for (var i = 1; i < table.rows.length; i++) {
    sumVal = sumVal + parseInt(table.rows[i].cells[3].innerHTML);
}

document.getElementById("SubTotal").innerHTML = "Sum Value = " + sumVal;
console.log(sumVal);*/
function SubTotal() {
    const tableRows = document.getElementsByTagName('tr');
    var sumVal = 0;
    for (var i = 1; i < tableRows.length; i++) {
        sumVal += parseInt(tableRows[i].cells[4].innerHTML);
    }
    console.log(tableRows);
    console.log(sumVal);
    $('#SubTotal').val(sumVal);
}

function GetDiscount() {
    var discountValue = $('#DiscountValue').val();
    var subTotal = $('#SubTotal').val();
    var discountRupees = ((subTotal * discountValue) / 100);
    var amount = subTotal - discountRupees;

    $('#TotalAmount').val(amount);
    $('#DiscountValueInRupee').val((-discountRupees));

}




function GetTotalAmount() {
    var qty = $('#ItemQuantity').val();
    var price = $('#ItemPrice').val();
    var amount = qty * price;
    $('#DiscountValue').val(0);
    $('#ItemAmount').val(amount);

}
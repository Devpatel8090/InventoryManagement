function FillDetailsOfVendorInInvoice() {
    var vendorId = $("#VendorSelectDropDown").val();
    $.ajax({
        url: "/Vendors/GetVendorDetailById?id=" + vendorId,
        success: function (data) {
            console.log(data);
            if (data != null) {
                var item = ""
                var item = `${data.firstName}  ${data.lastName}  \n${data.cityName}, ${data.stateName}, ${data.countryName}`
                $('#VendorDetailTextArea').val(item);
            }
            else {
                $('#VendorDetailTextArea').val('');
            }

        },
        error: function (error) {
            console.log(error);
        }
    })
}

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
        sumVal += parseInt(tableRows[i].cells[3].innerHTML);
    }
    console.log(tableRows);
    console.log(sumVal);
    $('#SubTotal').val(sumVal);
}


function FillTheQtyAndPrice() {
    var id = $('#ItemSelectDropDown').val();
    var itemName = $("#ItemSelectDropDown option:selected").text();
    


        for (var key in arr) {


            if (key == itemName) {
                $('#ItemQuantity').val(arr[key].qty);
                $('#ItemPrice').val(arr[key].price);
                $('#ItemAmount').val(arr[key].amount);
                break;

            } else {
                $('#ItemQuantity').val(0);
                $('#ItemPrice').val(0);
                $('#ItemAmount').val(0);

            }

        }
    

   
}

function GetTotalAmount() {
    var qty = $('#ItemQuantity').val();
    var price = $('#ItemPrice').val();
    var amount = qty * price;
    $('#ItemAmount').val(amount);
}

var arr = [];
/*var obj = {};*/

function AddEntryInData() {
    var itemName = $("#ItemSelectDropDown option:selected").text();
    var obj = {
        qty: $('#ItemQuantity').val(),
        price: $('#ItemPrice').val(),
        amount: $('#ItemAmount').val()
    };
    console.log(obj);
    arr[itemName] = obj;
    console.log(arr);
    console.log(arr.length);
    console.log(Reflect.ownKeys(arr).length);
    var lenthofArray = Reflect.ownKeys(arr).length;
    var items = '';
    /*for (var i = 0; i < lenthofArray - 1; i++) {
      item = `  <tr>
       < td > ${arr[0].id}</td >
                    <td>${arr[itemName].id}</td>
                    <td>Otto</td>
                    <td>Otto</td>
            <td>Otto</td>
        </tr>`
        items += item;
    }*/

    for (var key in arr) {
        if (arr.hasOwnProperty(key)) {

            // Printing Keys
            console.log(key);
            item = `  <tr>
                    <td>${key}</td>
                    <td> ${arr[key].qty}</td>
                    <td>${arr[key].price}</td>
                    <td>${arr[key].amount}</td>
                </tr>`
            items += item;

        }
    }
    console.log(items);
    $('#tableData').html(items);
    SubTotal();
}

function GetDiscount() {
    var discountValue = $('#DiscountValue').val();
    var subTotal = $('#SubTotal').val(sumVal);
    var amount = subTotal - ((subTotal * discountValue) / 100);

    $('#TotalAmount').val(amount);

}
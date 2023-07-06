

/*function ShowPreviousOrders() {
    var vendorId = $("#VendorSelectDropDown").val();
    $.ajax({
        url: "/Vendors/GetVendorOrders?id=" + vendorId,
        success: function (data) {
            console.log(data);
        },
        Error: function (error) {
            console.log(error);
        }
    })
}*/


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
                    $('#ShowOrderDetailButton').removeClass("hide");
                    $('#documentNumber').val(data.documentNumber);
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
    $('#AccountPayableTab').addClass("active");

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
        $('#DiscountValue').val(0);
        $('#ItemAmount').val(amount);

    }

    var arr = [];
    /*var obj = {};*/

    function AddEntryInData() {
        var itemName = $("#ItemSelectDropDown option:selected").text();
        if (PurchaseItemValidation()) {
            var obj = {
                qty: $('#ItemQuantity').val(),
                price: $('#ItemPrice').val(),
                amount: $('#ItemAmount').val(),
                itemId: $('#ItemSelectDropDown').val()
            };
            console.log(obj);
            arr[itemName] = obj;
            console.log(arr);
            console.log(arr.length);

            var items = '';


            for (var key in arr) {
                if (arr.hasOwnProperty(key)) {

                    // Printing Keys
                    console.log(key);
                    item = `  <tr>
                    <td>${arr[key].itemId}</td>
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
            GetDiscount();
        }
    }

    function GetDiscount() {
        var discountValue = $('#DiscountValue').val();
        var subTotal = $('#SubTotal').val();
        var discountRupees = ((subTotal * discountValue) / 100);
        var amount = subTotal - discountRupees;

        $('#TotalAmount').val(amount);
        $('#DiscountValueInRupee').val((-discountRupees));

    }

    function PurchaseItemValidation() {
        var qty = $('#ItemQuantity').val();
        var price = $('#ItemPrice').val();
        var itemId = $('#ItemSelectDropDown').val();
        var flag = true;
        if (itemId < 1 || itemId == "") {
            flag = false;
            toastr.error("Please Select The Item");
        }
        if (price < 0 || price == "") {
            flag = false;
            toastr.error("Please enter positive price");
        }
        if (qty < 0 || qty == "") {
            flag = false;
            toastr.error("Please enter positive qunatity");
        }
        return flag;

    }

    function AddRecordInPurchaseItem() {
        var vendorId = $("#VendorSelectDropDown").val();
        var documentNo = $('#documentNumber').val();
        var purchaseDate = $('#purchaseDate').val();
        var reference = $('#Reference').val();
        var subTotal = $('#SubTotal').val();
        var totalAmount = $('#TotalAmount').val();
        var discount = $('#DiscountValue').val();
        const tableRows = document.getElementsByTagName('tr');
        var tableData = [];

        var oTable = document.getElementById('tableData');

        //gets rows of table
        var rowLength = oTable.rows.length;

        //loops through rows    
        for (i = 0; i < rowLength; i++) {

            //gets cells of current row
            var oCells = oTable.rows.item(i).cells;

            var object = {
                "Id": oCells.item(0).innerHTML,
                "Qty": oCells.item(2).innerHTML,
                "Price": oCells.item(3).innerHTML,
                "Amount": oCells.item(4).innerHTML
            }
            tableData[i] = object;
        }

        var purchaseObj = {
            vendorId: vendorId,
            documentNo: documentNo,
            purchaseDate: purchaseDate,
            reference: reference,
            subTotal: subTotal,
            discount: discount,
            totalAmount: totalAmount,
            tableData: tableData
        }

        $.ajax({
            url: '/PurchaseInvoice/AddPurchaseInvoiceData?purchseObj=' + JSON.stringify(purchaseObj),
            type: 'POST',
            success: function (data) {
                console.log(data);
                if (data) {
                    toastr.success("Stock added  successfully");
                    setTimeout(function () { location.reload(); }, 1000);
                }
                else {
                    toastr.error("sorry Please Try Again");
                }
            },
            error: function (error) {
                console.log(error);
            }
        })

    }

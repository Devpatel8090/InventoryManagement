var vendorDtOptions = {
    scrollY: '60vh',
    serverSide: true,
    ajax: {
        "url": "/AccountPayable/Vendor/GetVendorsDT",
        "type": "POST",
        "dataType": "json",
        /*"success": function (data) {
            console.log(data);
        },*/
        "error": function (xhr, status) {
            console.log("Ajax error : " + xhr.responseText);
        }
    },
    order: [[1, "desc"]],
    columns: [
        { "data": "firstName", "name": "firstName", "width": "20%" },
        { "data": "lastName", "name": "lastName", "width": "20%" },
        { "data": "email", "name": "email", "width": "20%" },
        { "data": "phoneNumber", "name": "phoneNumber", "width": "20%" },
        { "width": "20%" }
    ],
    columnDefs: [
        {
            targets: [2, 3],
            orderable: false
        },
        {
            targets: 4,
            data: null,
            orderable: false,
            render: function (data, type, row, meta) {
                let actionsObj = {
                    "edit": row.id,
                    "delete": row.id
                };
                let actions = GetHtmlContent(actionsObj);
                return actions;

            }
        }
    ]
};


function GetHtmlContent(obj) {
    var items = "";
    var item = `<button class="border-0 bg-white EditUserButton" onclick="OpenModalToEditVendor()"  value="${obj.edit}">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" fill="currentColor"
                                     class="bi bi-pencil-square" style="color: #20B2AA;pointer-events:none" viewBox="0 0 16 16">
                                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                        <path fill-rule="evenodd"
                                          d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                    </svg>
                                </button>


                                <button class="border-0 bg-white" data-bs-toggle="modal" value="${obj.delete}" onclick="OpenDeleteWarningModal()">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" fill="currentColor"
                                     class="bi bi-trash3" viewBox="0 0 16 16" style="pointer-events:none">
                                        <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z" />
                                    </svg>
                                </button>`
    return item;
}


function AddVendor() {

}

function GetStatesByCountryId() {
    var countryId = $('#CountryId').val();
    /*var countryID2 = $('#CountryId').find(":selected").val();*/
    $.ajax({
        url: "/Vendors/GetStateDetailsByCountry?countryId=" + countryId,
        type: "GET",
        success: function (data) {
            console.log(data);
            var items = `<option value="-1">Please Select the State</option>`;
            $(data).each(function (i, item) {
                items += `<option value=${item.stateId}>` + item.stateName + `</option>`
                console.log(item);
            });
            $('#StateId').html(items);
            $('#CityId').html("");
        },
        error: function (error) {
            console.log(error);
        }
    })

}
function GetCitiesByStateId() {
    var cityId = $('#StateId').val();
    /*var cityId2 = $('#CountryId').find(":selected").val();*/
    $.ajax({
        url: "/Vendors/GetCityDetailsByState?stateId=" + cityId,
        type: "GET",
        success: function (data) {
            console.log(data);
            var items = `<option value="-1">Please Select the City</option>`;
            $(data).each(function (i, item) {
                items += `<option value=${item.cityId}>` + item.cityName + `</option>`
                console.log(item);
            });
            $('#CityId').html(items);
        },
        error: function (error) {
            console.log(error);
        }
    })

}


function AddOrUpdateVendor() {
    var vendorId = $('#VendorId').val();
    var firstName = $('#FirstName').val();
    var lastName = $('#LastName').val();
    var email = $('#Email').val();
    var phoneNumber = $('#PhoneNumber').val();
    var countryId = $('#CountryId').val();
    var stateId = $('#StateId').val();
    var cityId = $('#CityId').val();
    if (vendorId == "" || vendorId == undefined) {
        vendorId = 0;
    }

    var obj = {
        vendorId: vendorId,
        firstName: firstName,
        lastName: lastName,
        email: email,
        phoneNumber: phoneNumber,
        countryId: countryId,
        stateId: stateId,
        cityId: cityId
    }

    $.ajax({
        url: "/Vendors/AddOrUpdateVendor?VendorObj=" + JSON.stringify(obj),
        type: 'GET',
        success: function (data) {
            console.log(data);
            if (data) {
                if (vendorId != 0) {
                    toastr.success("Updated successfully");
                }
                else {
                    toastr.success("Added successfully");
                }
                /*$("#CloseCategoryAddModal").click();*/
            }
            else {
                toastr.error("Oh No! Please Try Again with Different EmailId");
            }
        },
        error: function (error) {
            console.log(error);
        }
    })
}


function OpenModalToEditVendor() {

    /*ar vendorId = $(this).val();*/
    var vendorId = event.target.value;
    $.ajax({
        url: "/Vendors/GetVendorDetailById?id=" + vendorId,
        success: function (data) {
            console.log(data);
            console.log(data.id);
            $('#AddTextInTitlevendor').addClass('hide');
            $('#UpdateTextInTitleVendor').removeClass('hide');
            $('#VendorId').val(data.id);
            $('#FirstName').val(data.firstName);
            $('#LastName').val(data.lastName);
            $('#Email').val(data.email);
            $('#PhoneNumber').val(data.phoneNumber);
            $('#CountryId').val(data.countryId);
            $('#StateId').val(data.stateId);
            $('#CityId').val(data.cityId);
            $('#AddVendorModel').modal('show');


        },
        error: function (error) {
            console.log(error);
        }
    })
}

function OpenDeleteWarningModal() {
    var vendorId = event.target.value;
    /* $('a').attr('action', '/Vendors/DeleteVendor/?id=' + vendorId);*/
    $('#VendorIdForDelete').val(vendorId);
    $('#DeleteVendorModal').modal('show');
}

function DeleteVendor() {
    var id = $('#VendorIdForDelete').val();
    $.ajax({
        url: "/Vendors/DeleteVendor?id=" + id,
        success: function (data) {
            console.log(data);
            toastr.success("Deleted Successfully");
            $('#CloseDeleteModal').click();
            setTimeout(function () { location.reload(); }, 3000);
            
        },
        error: function (error) {
            console.log(error);
        }
    })
}
    
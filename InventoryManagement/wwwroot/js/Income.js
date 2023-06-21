



function OpenCategoryList() {


    $.ajax({
        url: "/InventoryAdmin/GetCategories",
        type: "GET",
        success: function (data) {
            console.log(data);
            $('#CategoryModelView').html(data);
            $('#searchInput').val('');
            $('#CategoryModal').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    })


}
var selectedCategory;
function selectCategory() {


    selectedCategory = event.target.getAttribute("value")

    console.log(selectedCategory);
    $('tr').removeClass("bg__colour");
    $('#CategoryRow_' + selectedCategory).addClass("bg__colour");

}


function OpenAddModal() {
    selectedCategory = 0;
    selectedInventoryItem = 0;
    selectInventoryItemPrice = 0;
    /*$("#CategoryAddModal").html("");*/
    $('tr').removeClass("bg__colour");
    $(".UpdateModalTitle").addClass("hide");
    $(".AddModalTitle").removeClass("hide");
    $('#CategoryId').val('');
    $('#CategoryName').val('');
    $('#CategoryDescription').text('');
    $('#itemId').val('');
    $('#ItemCategory').val('');
    $('#itemDescription').val('');
    $('#itemName').val('');
    $('#PriceId').val('');
    $('#ItemIdOfInventoryPrice').val('');
    $('#ItemPrice').val('');

    /*$('#CategoryAddModal').modal('show');*/
}

function AddOrUpdateCategory() {
    var categoryId = $('#CategoryId').val();
    if (categoryId == "" || categoryId == undefined) {
        categoryId = 0;
    }
    var categoryName = $('#CategoryName').val();
    var description = $('#CategoryDescription').val();
    var obj = {
        categoryId: categoryId,
        categoryName: categoryName,
        description: description
    }

    var url = "/InventoryAdmin/AddCategory?AddObj=" + JSON.stringify(obj);
    $.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            if (data == "Success") {


                if (categoryId != 0) {
                    toastr.success("Updated successfully");
                    selectCategory = 0;
                }
                else {
                    toastr.success("Added successfully");

                }
                $("#CloseCategoryAddModal").click();
            }
            else {
                toastr.success("Oh No! Please Try Again with Different CategoryName");
            }

            OpenCategoryList();




        },
        error: function (error) {
            console.log(error);
        }
    })



}

function FillDataOnEditCategory() {

    if (selectedCategory == null || selectedCategory == undefined) {
        toastr.error("Please Select the row first");
    }
    else {
        $.ajax({
            url: "/InventoryAdmin/EditCategory?categoryId=" + selectedCategory,
            type: 'GET',
            success: function (data) {
                console.log(data);
                console.log(data.CategoryName);

                var obj = JSON.parse(data);
                console.log(obj);
                console.log(obj.CategoryName);
                $('#CategoryId').val(obj.CategoryId);
                $('#CategoryName').val(obj.CategoryName);
                /* $('#DateTimeMission').attr("max", data.maxDate);*/
                $('#CategoryDescription').text(obj.Description);
                $(".UpdateModalTitle").removeClass("hide");
                $(".AddModalTitle").addClass("hide");
                $('#CategoryAddModal').modal('show');

            },
            error: function (error) {
                console.log(error);
            }

        })

    }

}


function SearchCategory() {
    var searchText;
    searchText = $('#searchInput').val();
    /*$('.closeCategoryButton').click();*/
    console.log(searchText);

    $.ajax({
        url: "/InventoryAdmin/SearchCategory?searchText=" + searchText,
        success: function (data) {

            console.log(data);
            /* window.location.reload();*/


            $('#CategoryModelView').html(data);
            $('#searchInput').val(searchText);
            categoryPageNo = 1;




        },
        error: function (error) {
            console.log(error);
        }
    })

}

//var pageNo = parseInt($('#currentPageNo').text());
//function AddPagination() {


//    $.ajax({
//        url: "/InventoryAdmin/CategoryPagination=" + pageNo,
//        type: 'GET',
//        success: function (data) {
//            console.log(data);
//        },
//        error: function (error) {
//            console.log(error);
//        }
//    })

//}

var categoryPageNo = 1;
function AddCategoryPagination() {
    var totalPages = $('#TotalCategoriesForPagination').val();
    var idea = event.target.innerHTML;
    /* $('.closeCategoryButton').click();*/
    if (idea == "Next") {
        if (categoryPageNo < totalPages) {
            categoryPageNo++;
        }
        else {
            categoryPageNo = categoryPageNo;
        }
    }
    else if (idea == "Prev") {
        if (categoryPageNo > 1) {
            categoryPageNo--;
        }
        else {
            categoryPageNo = categoryPageNo
        }
    }
    else {
        categoryPageNo = idea;
    }

    $.ajax({
        url: "/InventoryAdmin/CategoryPagination?pageNo=" + categoryPageNo,
        type: "GET",
        success: function (data) {
            console.log(data);

            $('#CategoryModelView').html(data);
            /*  $('#categoryTab').click();*/

        },
        error: function (error) {
            console.log(error);
        }
    });
}


//function SearchWithPagination() {
//    var PageNo = event.target.innerHTML;
//    var  searchText = $('#searchInput').val();
//    var url;
//    if (searchText == null || searchText == undefined || searchText == '') {
//        url = "/InventoryAdmin/SearchCategoryWithPagination?pageNo=" + PageNo;
//    }
//    else if (PageNo == "Search") {
//        url = "/InventoryAdmin/SearchCategoryWithPagination?pageNo=" + 1 + "&searchText=" + searchText;
//    }
//    else {
//        url = "/InventoryAdmin/SearchCategoryWithPagination?pageNo=" + PageNo + "&searchText=" + searchText;
//    }
//    $.ajax({
//        url: url,
//        type: "GET",
//        success: function (data) {
//            console.log(data);

//            $('#CategoryModelView').html(data);
//            $('#searchInput').val(searchText);
//              /*$('#categoryTab').click();*/

//        },
//        error: function (error) {
//            console.log(error);
//        }
//    });

//}

function PreviousPageCategory() {

    console.log(pageNo);

    var totalPages = $('#TotalItemsForPagination').val();
    if (categoryPageNo <= totalPages) {
        categoryPageNo++;
    }
    else {
        categoryPageNo = categoryPageNo
    }
    AddPagination();


}
function NextPageCategory() {
    pageNo += 1;
    console.log(pageNo);
    AddPagination();

}

function DeleteCategory() {

    if (selectedCategory == null || selectedCategory == undefined) {
        toastr.error("Please Select the row first");
    }
    else {
        $.ajax({
            url: "/InventoryAdmin/DeleteCategory?categoryId=" + selectedCategory,
            type: 'GET',
            success: function (data) {
                console.log(data);
                /*window.location.reload();*/
                toastr.success("deleted successfully");
                console.log(data.CategoryName);
            },
            error: function (error) {
                console.log(error);
            }
        })

    }

}



////       ---------------------------------------------------     Inventor Items    ----------------------------------------------------------------------------


function OpenItemsList() {


    $.ajax({
        url: "/InventoryAdmin/GetItems",
        type: "GET",
        success: function (data) {
            console.log(data);
            $('#InventoryItemModelView').html(data);
            $('#searchInput').val('');
            $('#ItemsModal').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    })


}

var selectedInventoryItem;
function selectInventoryItem() {

    selectedInventoryItem = event.target.getAttribute("value")

    console.log(selectedInventoryItem);
    $('tr').removeClass("bg__colour");
    $('#InventoryRow_' + selectedInventoryItem).addClass("bg__colour");

}

function AddOrUpdateItem() {
    var itemId = $('#itemId').val();
    if (itemId == "" || itemId == undefined) {
        itemId = 0;
    }
    var itemName = $('#itemName').val();
    var itemDescription = $('#itemDescription').val();
    var ItemCategory = $('#ItemCategory').val();
    var ItemActive = $('#ItemActive').val();
    var obj = {
        itemId: itemId,
        itemName: itemName,
        itemDescription: itemDescription,
        ItemCategory: parseInt(ItemCategory),
        ItemActive: ItemActive
    }

    var url = "/InventoryAdmin/AddInventoryItem?AddObj=" + JSON.stringify(obj);
    $.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            if (data == "Success") {


                if (itemId != 0) {
                    toastr.success("Updated successfully");
                    itemId = 0;
                }
                else {
                    toastr.success("Added successfully");

                }
                $("#CloseItemAddModal").click();
            }
            else {
                toastr.success("Oh No! Please Try Again with Different ItemName");
            }
            OpenItemsList();




        },
        error: function (error) {
            console.log(error);
        }
    })



}

function FillDataOnEditItem() {

    if (selectedInventoryItem == null || selectedInventoryItem == undefined) {
        toastr.error("Please Select the row first");
    }
    else {
        $.ajax({
            url: "/InventoryAdmin/EditInventoryItem?itemId=" + selectedInventoryItem,
            type: 'GET',
            success: function (data) {
                console.log(data);
                console.log(data.CategoryName);

                var obj = JSON.parse(data);
                console.log(obj);
                console.log(obj.CategoryName);
                $('#itemId').val(obj.ItemId);
                $('#itemName').val(obj.ItemName);
                /* $('#DateTimeMission').attr("max", data.maxDate);*/
                /*$('#itemDescription').text(obj.Description);*/
                $('#itemDescription').val(obj.Description);
                $(".UpdateModalTitle").removeClass("hide");
                $(".AddModalTitle").addClass("hide");
                $('#ItemCategory').val(obj.CategoryId);
                if (obj.IsActive == true) {
                    /* $('#ItemActive').val(1);*/
                    $("#ItemActive option[value='true']").prop('selected', true);
                }
                else {
                    /* $('#ItemActive').val(0);*/
                    $("#ItemActive option[value='false']").prop('selected', true);
                }
                /* $('#ItemActive').val(obj.IsActive);*/
                $('#InventoryAddModal').modal('show');

            },
            error: function (error) {
                console.log(error);
            }

        })

    }

}



function DeleteInventoryItem() {

    if (selectedInventoryItem == null || selectedInventoryItem == undefined) {
        toastr.error("Please Select the row first");
    }
    else {
        $.ajax({
            url: "/InventoryAdmin/DeleteInventoryItem?itemId=" + selectedInventoryItem,
            type: 'GET',
            success: function (data) {
                console.log(data);
                $('#InventoryItemModelView').html(data);
                /* $('#InventoryModal').modal('toggle');*/
                toastr.success("deleted successfully");
                $('#ItemsModal').modal('show');
                $('#inventoryTab').click();
            },
            error: function (error) {
                console.log(error);
            }
        })

    }

}

function SearchItem() {
    var searchText;
    searchText = $('#searchItemInput').val();
    /*$('.closeCategoryButton').click();*/
    console.log(searchText);

    $.ajax({
        url: "/InventoryAdmin/SearchItem?searchText=" + searchText,
        success: function (data) {

            console.log(data);
            /* window.location.reload();*/


            $('#InventoryItemModelView').html(data);
            $('#searchItemInput').val(searchText);
            ItemPageNo = 1;

            /* windows.location.reload();*/
            /* $('.closeCategoryButton').click();*/
            /*  $('#categoryTab').click();*/
            //$('#CategoryModal').modal('toggle');
            //$('#CategoryModal').modal('show');


            /*$('#CategoryModal').modal('show');*/


        },
        error: function (error) {
            console.log(error);
        }
    })

}

var ItemPageNo = 1;
function AddItemsPagination() {

    var totalPages = $('#TotalItemsForPagination').val();
    var idea = event.target.innerHTML;

    if (idea == "Next") {
        if (ItemPageNo < totalPages) {
            ItemPageNo++;
        }
        else {
            ItemPageNo = ItemPageNo;
        }
    }
    else if (idea == "Prev") {
        if (ItemPageNo > 1) {
            ItemPageNo--;
        }
        else {
            ItemPageNo = ItemPageNo
        }
    }
    else {
        ItemPageNo = idea;
    }
    $.ajax({
        url: "/InventoryAdmin/ItemsPagination?pageNo=" + ItemPageNo,
        type: "GET",
        success: function (data) {
            console.log(data);

            $('#InventoryItemModelView').html(data);
            /*  $('#categoryTab').click();*/

        },
        error: function (error) {
            console.log(error);
        }
    });
}





//////  ------------------------------------------------------------           Item prices  -------------------------------------------------------------------------- 


function OpenPricesList() {

    $.ajax({
        url: "/InventoryAdmin/GetPrices",
        type: "GET",
        success: function (data) {
            console.log(data);
            $('#PricingItemModelView').html(data);
            $('#searchInput').val('');
            $('#InventoryItemsPricingModal').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    })
}
var selectedInventoryItemPrice;
function selectInventoryItemPrice() {

    selectedInventoryItemPrice = event.target.getAttribute("value")

    console.log(selectedInventoryItemPrice);
    $('tr').removeClass("bg__colour");
    $('#InventoryItemPriceRow_' + selectedInventoryItemPrice).addClass("bg__colour");

}


function AddOrUpdatePrice() {
    var PriceId = $('#PriceId').val();
    if (PriceId == "" || PriceId == undefined) {
        PriceId = 0;
    }
    var ItemId = $('#ItemIdOfInventoryPrice').val();
    var ItemPrice = $('#ItemPrice').val();

    var obj = {
        PriceId: PriceId,
        ItemId: parseInt(ItemId),
        ItemPrice: parseInt(ItemPrice),
    }

    var url = "/InventoryAdmin/AddItemPrice?AddObj=" + JSON.stringify(obj);
    $.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            if (data == "Success") {


                if (PriceId != 0) {
                    toastr.success("Updated successfully");
                    PriceId = 0;
                }
                else {
                    toastr.success("Added successfully");

                }
                $("#ClosePriceAddModal").click();
            }
            else {
                toastr.success("Oh No! Please Try Again ");
            }
            OpenPricesList();
        },
        error: function (error) {
            console.log(error);
        }
    })



}

function FillDataOnEditPrice() {
    if (selectedInventoryItemPrice == null || selectedInventoryItemPrice == undefined) {
        toastr.error("Please Select the row first");
    }
    else {
        $.ajax({
            url: "/InventoryAdmin/EditPrice?priceId=" + selectedInventoryItemPrice,
            type: 'GET',
            success: function (data) {
                console.log(data);
                console.log(data.CategoryName);

                var obj = JSON.parse(data);
                console.log(obj);
                $('#PriceId').val(obj.PriceId);
                $('#ItemIdOfInventoryPrice').val(obj.ItemId);
                $('#ItemPrice').val(obj.Price);
                $(".UpdateModalTitle").removeClass("hide");
                $(".AddModalTitle").addClass("hide");

                $('#InventoryItemsPricingAddModal').modal('show');


            },
            error: function (error) {
                console.log(error);
            }

        })

    }

}

function DeleteItemPrice() {


    if (selectedInventoryItemPrice == null || selectedInventoryItemPrice == undefined) {
        toastr.error("Please Select the row first");
    }
    else {
        $.ajax({
            url: "/InventoryAdmin/DeleteInventoryItemPrice?priceId=" + selectedInventoryItemPrice,
            type: 'GET',
            success: function (data) {
                /*console.log(data);
                window.location.reload();
                toastr.success("deleted successfully");
                console.log(data.CategoryName);*/
                console.log(data);

                selectedInventoryItemPrice = null;

                $('#PricingItemModelView').html(data);
            },
            error: function (error) {
                console.log(error);
            }
        })



    }

}

function GetPriceOfSelectedItem() {
    var ItemId = $('#ItemIdOfInventoryPrice').val();
    $.ajax({
        url: "/InventoryAdmin/GetPriceByItemId?itemId=" + ItemId,
        type: "GET",
        success: function (data) {
            console.log(data);
            var obj = JSON.parse(data);
            console.log(obj);
            console.log(obj.length)
            if (obj.length > 0) {
                $('#ItemPrice').val(obj[0].Price);
                $('#PriceId').val(obj[0].PriceId);
            }
        },
        error: function (error) {
            console.log(error);
        }
    })
}



function SearchPrice() {
    var searchText;
    searchText = $('#searchInputPrice').val();
    /*$('.closeCategoryButton').click();*/
    console.log(searchText);

    $.ajax({
        url: "/InventoryAdmin/SearchPrice?searchText=" + searchText,
        success: function (data) {

            console.log(data);
            /* window.location.reload();*/


            $('#PricingItemModelView').html(data);
            $('#searchInput').val(searchText);
            PricePageNo = 1;


            /* windows.location.reload();*/
            /* $('.closeCategoryButton').click();*/
            /*  $('#categoryTab').click();*/
            //$('#CategoryModal').modal('toggle');
            //$('#CategoryModal').modal('show');


            /*$('#CategoryModal').modal('show');*/


        },
        error: function (error) {
            console.log(error);
        }
    })

}

var PricePageNo = 1;
function AddItemsPricingPagination() {

    var totalPages = $('#TotalPricesForPagination').val();
    var idea = event.target.innerHTML;

    if (idea == "Next") {
        if (PricePageNo < totalPages) {
            PricePageNo++;
        }
        else {
            PricePageNo = PricePageNo;
        }
    }
    else if (idea == "Prev") {
        if (PricePageNo > 1) {
            PricePageNo--;
        }
        else {
            PricePageNo = PricePageNo
        }
    }
    else {
        PricePageNo = idea;
    }
    $.ajax({
        url: "/InventoryAdmin/ItemsPricesPagination?pageNo=" + PricePageNo,
        type: "GET",
        success: function (data) {
            console.log(data);

            $('#PricingItemModelView').html(data);
            /*  $('#categoryTab').click();*/

        },
        error: function (error) {
            console.log(error);
        }
    });
}







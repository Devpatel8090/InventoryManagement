﻿


var selectedCategory;

function selectCategory() {
   
    
    selectedCategory = event.target.getAttribute("value")

    console.log(selectedCategory);
    $('tr').removeClass("bg__colour");
    $('#CategoryRow_' + selectedCategory).addClass("bg__colour");
 
}


function OpenAddModal() {
    $(".UpdateModalTitle").addClass("hide");
    $(".AddModalTitle").removeClass("hide");
}



function EditCategory() {

    if (selectedCategory == null || selectedCategory == undefined)
    {
        toastr.error("Please Select the row first");
    }
    else
    {
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

var searchText;
function SearchCategory() {
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

function AddCategoryPagination() {
    var Pagevalue = event.target.innerHTML;
    /* $('.closeCategoryButton').click();*/


   
    $.ajax({
        url: "/InventoryAdmin/CategoryPagination?pageNo=" + Pagevalue,
        type:"GET",
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


function SearchWithPagination() {
    var PageNo = event.target.innerHTML;
    var  searchText = $('#searchInput').val();
    var url;
    if (searchText == null || searchText == undefined || searchText == '') {
        url = "/InventoryAdmin/SearchCategoryWithPagination?pageNo=" + PageNo;
    }
    else if (PageNo == "Search") {
        url = "/InventoryAdmin/SearchCategoryWithPagination?pageNo=" + 1 + "&searchText=" + searchText;
    }
    else {
        url = "/InventoryAdmin/SearchCategoryWithPagination?pageNo=" + PageNo + "&searchText=" + searchText;
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            console.log(data);

            $('#CategoryModelView').html(data);
            $('#searchInput').val(searchText);
              /*$('#categoryTab').click();*/

        },
        error: function (error) {
            console.log(error);
        }
    });

}

function PreviousPage() {
    pageNo -= 1;
    console.log(pageNo);
    AddPagination();


}
function NextPointer() {
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



var selectedInventoryItem;
function selectInventoryItem() {

    selectedInventoryItem = event.target.getAttribute("value")

    console.log(selectedInventoryItem);
    $('tr').removeClass("bg__colour");
    $('#InventoryRow_' + selectedInventoryItem).addClass("bg__colour");

}


function EditInventoryItem() {

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
                $('#itemDescription').text(obj.Description);
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

function AddItemsPagination() {
    var Pagevalue = event.target.innerHTML;
    /* $('.closeCategoryButton').click();*/



    $.ajax({
        url: "/InventoryAdmin/ItemsPagination?pageNo=" + Pagevalue,
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



var selectedInventoryItemPrice;
function selectInventoryItemPrice() {

    selectedInventoryItemPrice = event.target.getAttribute("value")

    console.log(selectedInventoryItemPrice);
    $('tr').removeClass("bg__colour");
    $('#InventoryItemPriceRow_' + selectedInventoryItemPrice).addClass("bg__colour");

}

function EditItemPrice() {
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

function AddItemsPricingPagination() {
    var Pagevalue = event.target.innerHTML;
   
    $.ajax({
        url: "/InventoryAdmin/ItemsPricesPagination?pageNo=" + Pagevalue,
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


function OpenCategoryList() {
    
     
        $.ajax({
            url: "/InventoryAdmin/GetCategories",
            type:"GET",
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
function OpenItemsList() {
    
     
        $.ajax({
            url: "/InventoryAdmin/GetItems",
            type:"GET",
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

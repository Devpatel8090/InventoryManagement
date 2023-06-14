

var selectedCategory;

function selectCategory() {
   
    
    selectedCategory = event.target.getAttribute("value")

    console.log(selectedCategory);
    $('tr').removeClass("bg__colour");
    $('#CategoryRow_' + selectedCategory).addClass("bg__colour");
 
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
                $('#CategoryAddModal').modal('show');
                
              
               
              

            },
            error: function (error) {
                console.log(error);
            }

        })

    }

}

var selectedInventoryItem;
function selectInventoryItem() {

    selectedInventoryItem = event.target.getAttribute("value")

    console.log(selectedInventoryItem);
    $('tr').removeClass("bg__colour");
    $('#CategoryRow_' + selectedInventoryItem).addClass("bg__colour");

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
                $('#ItemCategory').val(obj.CategoryId);
                $('#ItemActive').val(obj.IsActive);
                $('#InventoryAddModal').modal('show');





            },
            error: function (error) {
                console.log(error);
            }

        })

    }

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
                window.location.reload();
                toastr.success("deleted successfully");
                console.log(data.CategoryName);
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
    console.log(searchText);

    $.ajax({
        url: "/InventoryAdmin/SearchCategory?searchText=" + searchText,
        success: function (data) {
            console.log(data);

            $('#CategoryModelView').html(data);
            $('#CategoryModal').modal('toggle');
            $('#CategoryModal').modal('show');
           
            /*$('#CategoryModal').modal('show');*/


        },
        error: function (error) {
            console.log(error);
        }
    })

}
var pageNo = parseInt($('#currentPageNo').text());
console.log(pageNo);
function paginationWithSearch() {
    
}



function PreviousPage() {
    pageNo -= 1;
    console.log(pageNo);
    
}
function NextPointer() {
    pageNo += 1;
    console.log(pageNo);

}
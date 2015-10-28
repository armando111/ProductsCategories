function getServerHost()
{
    return 'http://localhost:55738/';
}

function getCategoryConnectionString(){
    return getServerHost() + 'api/Categories/';
}

function getAllCategories() {
    var data = getAllCategoriesData();
    addCategoriesToGrid(data);
    initDefaults();
}

function getAllCategoriesData() {

    var connStringCategories = getCategoryConnectionString() +  'GetAllCategories';
    
    jQuery.support.cors = true;
    $.ajax({
        url: connStringCategories, 
        type: 'GET',
        dataType: 'json',
        async: !1,
        success: function (data) {            
            result = data;
        },
        error : function(jqXHR, textStatus, errorThrown) {
                console.log("jqXHR statusCode" + jqXHR.statusCode());
                console.log("textStatus " + textStatus);
                console.log("errorThrown " + errorThrown);
            }
    });
    return result;
}

function createCategory() {
    if (validateCategoryInputFields()) {

            var connStringCategories = getCategoryConnectionString() + 'CreateCategory';

            jQuery.support.cors = true;
            var entity = {
                Name: $('#inputName').val(),
                Description: $('#inputDescription').val()
            };
                        
            $.ajax({
                url: connStringCategories ,
                type: 'POST',
                crossDomain:true,
                data: JSON.stringify(entity),
                contentType: "application/json",
                success: function (data) {
                    getAllCategories();
                    initDefaults();
                    $('#successmsg').show();
                },
                error : function(jqXHR, textStatus, errorThrown) {
                    console.log("jqXHR statusCode" + jqXHR.statusCode());
                    console.log("textStatus " + textStatus);
                    console.log("errorThrown " + errorThrown);
                }
            });
    }
}

/*
function GetCategoryById() {
    jQuery.support.cors = true;
    var id = $('#txtCategory').val();
    $.ajax({
        url: connStringCategories + 'GetCategoryById/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowCategory(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}
*/

function updateCategoryInit(){

    $('#updateCategoryForm').show();

    var categoryId = $(this).parent().parent().find("td[name='id']").html();
    var categoryName = $(this).parent().parent().find("td[name='ctgryName']").html();
    var categoryDescription = $(this).parent().parent().find("td[name='ctgryDscrptn']").html();

    $("#updateId").text(categoryId);
    $("#updateName").val(categoryName);
    $("#updateDescription").val(categoryDescription);

    $('#updateBtn').click(updateCategory);
    $('#cancelUpdateBtn').click(cancelUpdateCategory);
}

function updateCategory() {

    var entity = {
        Id : $("#updateId").text(),
        Name: $('#updateName').val(),
        Description: $('#updateDescription').val()
    };

    // Determine ID of current selected table row
    var categoryId = $("#updateId").val();
    var connectionString = getCategoryConnectionString() + "UpdateCategory/" + categoryId;

    $.ajax({
        type : "PUT",
        url : connectionString,
        contentType : "application/json",
        data: JSON.stringify(entity),
        success : function(result) {
            initDefaults();
            addCategoriesToGrid();
            $('#successmsg').show();            
        },
        error : function(jqXHR, textStatus, errorThrown) {
            console.log("jqXHR statusCode" + jqXHR.statusCode());
            console.log("textStatus " + textStatus);
            console.log("errorThrown " + errorThrown);
        }
    });
}

function cancelUpdateCategory(){
    initDefaults()
}

function deleteCategory() {

    // Determine ID of current selected table row
    var categoryId = $(this).parent().parent().find("td[name='id']").html();
    var connectionString = getCategoryConnectionString() + "DeleteCategory/" + categoryId;

    // Perform DELETE request
    $.ajax({
        type : "DELETE",
        url : connectionString,
        contentType : "application/json",
        success : function(result) {
            getAllCategories();
            initDefaults();
            $('#successmsg').show();  
        },
        error : function(jqXHR, textStatus, errorThrown) {
            console.log("jqXHR statusCode" + jqXHR.statusCode());
            console.log("textStatus " + textStatus);
            console.log("errorThrown " + errorThrown);
        }
    });
}

function addCategoriesToGrid(elements)
{
    $('#ctgryDetails tbody > tr').remove();
    $.each(elements, function (index, element) {
        var row = rowConstructor(element);
        $('#ctgryDetails > tbody:last').append((row).toString());

        $("button[action='update']").click(updateCategoryInit);
        $("button[action='delete']").click(deleteCategory);

    });
}


function addCategoryToGrid(element) {

    var row = rowConstructor(element);
    
    $('#ctgryDetails > tbody:last').append((row).toString());

    $("button[action='update']").click(updateCategoryInit);
    $("button[action='delete']").click(deleteCategory);

}

function rowConstructor(element)
{
    return "<tr>" +
                "<td name='id'>" + element.Id + "</td>" +
                "<td name='ctgryName'>" + element.Name + "</td>"+
                "<td name='ctgryDscrptn'>" + element.Description + "</td>" +
                "<td><button id=\"delButton\" class=\"btn btn-primary\" action=\"update\">Update</button></td>" +
                "<td><button id=\"delButton\" class=\"btn btn-primary\" action=\"delete\">Delete</button></td>" +
           "</tr>"

}

function initDefaults() {
    $("#inputName").val('');
    $("#inputDescription").val('');

    $("#inputNameProduct").val('');
    $("#addProdCtgry").val('');
    $("#inputImageUrl").val('');

    $('#alertmsg').hide();
    $('#successmsg').hide();
    $('#updateCategoryForm').hide();
    $('#updateProductForm').hide();
}


function validateCategoryInputFields() {

    var inputName = $('#inputName').val();
    
    // Validate Name Fields

    if($.trim(inputName).length == 0) {
        showErrorMsg("Name is missing!");
        return false;
    } 
    else {
        return true;
    }
}

function showErrorMsg(messageText) {

    $('#alertmsg').html(messageText);
    $('#alertmsg').show();

}
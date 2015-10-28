function getProductConnectionString(){
    return getServerHost() + 'api/Products/';
}

function createProduct() {
    initDefaults();

    if (validateProductInputFields()) {

            var connectionString = getProductConnectionString();

            jQuery.support.cors = true;
            var entity = {
                Name: $('#inputNameProduct').val(),
                CategoryId: $('#addProdCtgry').attr("val"),
                ImageUrl: $('#inputImageUrl').val() //escape()
            };
                        
            $.ajax({
                url: connectionString + 'CreateProduct',
                type: 'POST',
                crossDomain:true,
                data: JSON.stringify(entity),
                contentType: "application/json",
                success: function (data) {
                	getAllProducts();
                    initDefaults();
                    $('#successmsg').show();
                },
                error : function(jqXHR, textStatus, errorThrown) {
                    showErrorMsg("Product vreation Problem!");
                    console.log("jqXHR statusCode" + jqXHR.statusCode());
                    console.log("textStatus " + textStatus);
                    console.log("errorThrown " + errorThrown);
                }
            });
    }
}

function getAllProducts() {

    var connectionString = getProductConnectionString();
    
    jQuery.support.cors = true;
    $.ajax({
        url: connectionString + 'GetAllProducts', 
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            addProductsToGrid(data);
            initDefaults();

        },
        error : function(jqXHR, textStatus, errorThrown) {
        		showErrorMsg("Problem in data Loading!");
            	
                console.log("jqXHR statusCode" + jqXHR.statusCode());
                console.log("textStatus " + textStatus);
                console.log("errorThrown " + errorThrown);
            }
    });
}

function updateProductInit(){

    $('#updateProductForm').show();

    var productId = $(this).parent().parent().find("td[name='id']").html();
    var productName = $(this).parent().parent().find("td[name='productName']").html();
    var categoryId = $(this).parent().parent().find("td[name='productCategory']").attr("val");
    var categoryName = $(this).parent().parent().find("td[name='productCategory']").html();
    var imageUrl = $(this).parent().parent().find("td[name='imageUrl']").html();

    
    $('#updtProdId').html(productId);
    $('#updtPrdtName').val(productName);
	

    $('#updtProdCtgry').text(categoryName);
    $('#updtProdCtgry').attr("val", $(this).attr("val", categoryId));

	$('#updtImmgUrl').val(imageUrl);
	$('#prdtImg').attr('src', imageUrl);
	console.log($('#prdtImg').parent().html());

    $('#updtPrdtBtn').click(updateProduct);
    $('#cancelUpdtPrdt').click(cancelUpdateProduct);
}
function cancelUpdateProduct()
{
	initDefaults()
}

function updateProduct() {

    var entity = {
    	Id : $('#updtProdId').text(),
        Name: $('#updtPrdtName').val(),
        CategoryId: $('#updtProdCtgry').attr("val"),
        ImageUrl: $('#updtImmgUrl').val() //escape()
    };

    var connectionString = getProductConnectionString() + "UpdateProduct";

    $.ajax({
        type : "PUT",
        url : connectionString,
        contentType : "application/json",
        data: JSON.stringify(entity),
        success : function(result) {
            initDefaults();
            $('#successmsg').text("Succeeded update !");
            $('#successmsg').show();            
        },
        error : function(jqXHR, textStatus, errorThrown) {
        	getAllProducts();
            initDefaults();
            showErrorMsg("Update is not succeeded!");
            
            console.log("jqXHR statusCode" + jqXHR.statusCode());
            console.log("textStatus " + textStatus);
            console.log("errorThrown " + errorThrown);
        }
    });
}

function deleteProduct() {

    // Determine ID of current selected table row
    var id = $(this).parent().parent().find("td[name='id']").html();
    var connectionString = getProductConnectionString() + "deleteProduct/" + id;

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
            showErrorMsg("Delete is not succeeded!");
            
            console.log("jqXHR statusCode" + jqXHR.statusCode());
            console.log("textStatus " + textStatus);
            console.log("errorThrown " + errorThrown);
        }
    });
}


function addProductsToGrid(elements){
	var categories = getAllCategoriesData()

    $('#productDetails tbody > tr').remove();
    $.each(elements, function (index, element) {
        categoryName = getCategoryName(categories, element.CategoryId);
		
        $('#productDetails > tbody:last').append("<tr>" +
                "<td name='id'>" + element.Id + "</td>" +
                "<td name='productName'>" + element.Name + "</td>"+
                "<td name='productCategory' val='"+ element.CategoryId +"'>" + categoryName + "</td>" +
                "<td name='imageUrl'>" + element.ImageUrl + "</td>" +
                "<td><button id=\"delButton\" class=\"btn btn-primary\" action=\"updateProduct\">Update</button></td>" +
                "<td><button id=\"delButton\" class=\"btn btn-primary\" action=\"deleteProduct\">Delete</button></td>" +
           "</tr>");

        $("button[action='updateProduct']").click(updateProductInit);
        $("button[action='deleteProduct']").click(deleteProduct);

    });

}

function getCategoryName (data, categoryId) {
    var categoryName = "";
    $.each(data, function(index, element){
        if(element.Id == categoryId)
            categoryName = element.Name;
    });
    return categoryName;
}


function loadCategories() {
	
	var categories = getAllCategoriesData();
	addToCategoryDropDown(categories);

}

function addToCategoryDropDown(elements){
    $.each(elements, function (index, element) {
    	
    	var li = '<li><a val="'+ element.Id +'" href="#">' + element.Name + '</a></li>' 

    	$('#ddAdd .dropdown-menu').append(li.toString());
    	$('#ddUpdt .dropdown-menu').append(li.toString());

    });
}


function validateProductInputFields() {

    var inputName = $('#inputNameProduct').val();
    var category = $('#addProdCtgry').attr("val");
    // Validate Name Fields

    if(($.trim(inputName).length === 0) 
    	|| ( category === undefined 
    		|| category === null 
    		|| category === "Category")
    	 ) 
    {
        showErrorMsg("Name or Category is missing!");
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
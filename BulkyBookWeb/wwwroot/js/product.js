var dataTable;

//js code for loading the datatable API
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "25%" },         //the values have to match with the json returned from GetAll response of the httpRequest. 
            { "data": "isbn", "width": "25%" },
            { "data": "price", "width": "25%" },
            { "data": "author", "width": "25%" },
            { "data": "category", "width": "15%" },
        ]
    });

}

﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: { url: "/admin/order/GetAll" },

        columns: [
            { data: "id", width: "5%" },
            { data: "name", width: "15%" },
            { data: "phoneNumber", width: "20%" },
            { data: "applicationUser.email", width: "20%" },
            { data: "orderStatus", width: "10%" },
            { data: "orderTotal", width: "10%" },
            {
                data: "id",
                render: function (data) {
                    return `
            <div class="w-75 btn-group"role="group">
            <a href="/admin/product/Upsert/${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
            </div>
          `;
                },
                width: "20%",
            },
        ],
    });
}


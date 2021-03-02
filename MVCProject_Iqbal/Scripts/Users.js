$(document).ready(function () {
    loadData();
});
function loadData() {
    $.ajax({
        url: "/Teacher/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.TeacherID + '</td>';
                html += '<td>' + item.TeacherName + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td>' + item.CellPhone + '</td>';
                html += '<td>' + item.ContactAddress + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.TeacherID + ')">Edit</a> | <a href="#" onclick="Delele(' + item.TeacherID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        TeacherID: $('#TeacherID').val(),
        TeacherName: $('#TeacherName').val(),
        Email: $('#Email').val(),
        CellPhone: $('#CellPhone').val(),
        ContactAddress: $('#ContactAddress').val()
    };
    $.ajax({
        url: "/Teacher/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(Id) {
    $('#TeacherName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#CellPhone').css('border-color', 'lightgrey');
    $('#ContactAddress').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Teacher/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TeacherID').val(result.TeacherID);
            $('#TeacherName').val(result.TeacherName);
            $('#Email').val(result.Email);
            $('#CellPhone').val(result.CellPhone);
            $('#ContactAddress').val(result.ContactAddress);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        TeacherID: $('#TeacherID').val(),
        TeacherName: $('#TeacherName').val(),
        Email: $('#Email').val(),
        CellPhone: $('#CellPhone').val(),
        ContactAddress: $('#ContactAddress').val(),
    };
    $.ajax({
        url: "/Teacher/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#TeacherID').val("");
            $('#TeacherName').val("");
            $('#Email').val("");
            $('#CellPhone').val("");
            $('#ContactAddress').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Teacher/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBox() {
    $('#TeacherID').val("");
    $('#TeacherName').val("");
    $('#Email').val("");
    $('#CellPhone').val("");
    $('#ContactAddress').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#TeacherName').css('border-color', 'lightgrey');
    $('#Email').css('border-color', 'lightgrey');
    $('#CellPhone').css('border-color', 'lightgrey');
    $('#ContactAddress').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    if ($('#TeacherName').val().trim() == "") {
        $('#TeacherName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TeacherName').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#CellPhone').val().trim() == "") {
        $('#CellPhone').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CellPhone').css('border-color', 'lightgrey');
    }
    if ($('#ContactAddress').val().trim() == "") {
        $('#ContactAddress').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ContactAddress').css('border-color', 'lightgrey');
    }
    return isValid;
}
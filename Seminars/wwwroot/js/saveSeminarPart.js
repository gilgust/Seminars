// seminar parts CRUD
$("#addPart").on("click", addPartHandler);
$(".edit-part-btn").on("click", editPartBtnHandler);
$("#saveSeminarPart").on("submit", saveSeminarPartHandler);
$(".delete-part-btn").on("click", deletePartBtnPartHandler);

$("#centralModalFluidSuccessDemo").on("show.bs.modal", function () {
    let seminarId = $("#Id").val();
    $('#parent-of-part').val(seminarId);
});
$('#centralModalFluidSuccessDemo').on("hide.bs.modal", function () {
        //alert("hide");
    });
$('#centralModalFluidSuccessDemo').on("hidden.bs.modal",function () {
        $("#saveSeminarPart").trigger("reset");
        $("#new-part-id").val(0);
    });


function addPartHandler() {
    $("#modal-title").text("New part");
    $("#part-name").val("");
    $("#part-order").val("");
    $("#centralModalFluidSuccessDemo").modal("show");
}

function editPartBtnHandler() {
    let wrapper = $(this).parent().parent();
    let partId = $(wrapper).data("id");
    let partOrder = $(wrapper).find("#order-part-" + partId).text();
    let partName = $(wrapper).find("#name-part-" + partId).text();

    $('#modal-title').text(partName);
    $("#part-name").val(partName);
    $("#part-order").val(partOrder);
    $("#new-part-id").val(partId);
    $('#centralModalFluidSuccessDemo').modal('show');
}

function saveSeminarPartHandler(e) {
    e.preventDefault();
    let id = this.elements["Id"].value;
    let seminarId = this.elements["SeminarId"].value;
    let name = this.elements["Name"].value;
    let order = this.elements["Order"].value;

    if (Number(id) === 0) {
        createSeminarPart(seminarId, name, order);
    } else {
        editSeminarPart(id, seminarId, name, order);
    }
}

//create seminar part node ready
function createSeminarPart(seminarId, name, order) {
    $.ajax({
            url: "/api/SeminarParts",
            contentType : "application/json",
            method: "POST",
            data: JSON.stringify({
                seminarId: seminarId,
                name: name,
                order: order
            })
        })
        .done(function(data) {
            console.log(data);
            //create node ready
            createSeminarPartNode(data);
            $('#centralModalFluidSuccessDemo').modal('hide');
        })
        .fail(function (data) {
            console.log("some error");
            console.log(data);
        });
}

//edit seminar part node ready
function editSeminarPart(id, seminarId, name, order) {
    let data = JSON.stringify({
        id: id,
        Name: name,
        Order: order,
        SeminarId: seminarId
    });
    $.ajax({
        url: "/api/SeminarParts/" + id,
        contentType: "application/json",
        method: "PUT",
        data: data})
        .done(function (data) {
            console.log(data);
            editSeminarPartNode(data);
            $("#centralModalFluidSuccessDemo").modal("hide");
        })
        .fail(function (data) {
            console.log("some error");
            console.log(data);
        });
}

//delete seminar part node ready
function deletePartBtnPartHandler() {
    console.log("delete");
    let wrapper = $(this).parent().parent();
    let partId = $(wrapper).data("id");

    $.ajax({
            url: "/api/SeminarParts/" + partId,
            contentType: "application/json",
            method: "DELETE" })
        .done(function (data) {
            console.log(data);
            deleteSeminarPartNode(data);
            $("#centralModalFluidSuccessDemo").modal("hide");
        })
        .fail(function (data) {
            console.log("some error");
            console.log(data);
        });
}

function createSeminarPartNode(data) {
    //create wrapper of all data
    let seminarPartWrapper = document.createElement('div');
    seminarPartWrapper.setAttribute("id", "seminarPart-" + data.id);
    seminarPartWrapper.classList.add("seminar-part");
    seminarPartWrapper.dataset.id = data.id;
    seminarPartWrapper.dataset.seminarId = data.seminarId;

    //create with order
    let orderNod = document.createElement("div");
    let orderTextNod = document.createTextNode(data.order);

    orderNod.setAttribute("id", "order-part-" + data.Id);
    orderNod.appendChild(orderTextNod);

    //create div with name
    let nameNod = document.createElement("div");
    let nameTextNod = document.createTextNode(data.name);

    nameNod.setAttribute("id", "name-part-" + data.Id);
    nameNod.appendChild(nameTextNod);

    //create wrapper of all data
    let buttonsWrapper = document.createElement("div");
    buttonsWrapper.classList.add("btn-group");

    //create wrapper of all data
    let editButton = document.createElement("button");
    let textEditButton = document.createTextNode("edit");
    editButton.classList.add("btn", "btn-sm", "edit-part-btn", "btn-primary");
    editButton.setAttribute("type", "button");
    editButton.appendChild(textEditButton);
    editButton.addEventListener("click", editPartBtnHandler);


    //create wrapper of all data
    let deleteButton = document.createElement('button');
    let textDeleteButton = document.createTextNode("delete");
    deleteButton.classList.add("btn", "btn-sm", "edit-part-btn", "btn-danger");
    deleteButton.setAttribute("type", "button");
    deleteButton.appendChild(textDeleteButton);
    deleteButton.addEventListener("click", deletePartBtnPartHandler);

    buttonsWrapper.appendChild(editButton);
    buttonsWrapper.appendChild(deleteButton);

    seminarPartWrapper.appendChild(orderNod);
    seminarPartWrapper.appendChild(nameNod);
    seminarPartWrapper.appendChild(buttonsWrapper);

    let partList = document.getElementById("partsWrapper");
    partList.appendChild(seminarPartWrapper);
}

function editSeminarPartNode(data) {
    let seminarPart = $(`#seminarPart-${data.id}`);
    $(seminarPart).find(`#order-part-${data.id}`).text(data.order);
    $(seminarPart).find(`#name-part-${data.id}`).text(data.name);
}

function deleteSeminarPartNode(data) {
    $(`#seminarPart-${data.id}`).replaceWith("");
}


// seminar chapters CRUD
$("#addChapter").on("click", addChapterHandler);
$(".edit-chapter-btn").on("click", editChapterBtnHandler);
$("#saveSeminarChapter").on("submit", saveSeminarChapterHandler);
$(".delete-chapter-btn").on("click", deleteChapterBtnPartHandler);

$("#centralModalChapterEdit").on('show.bs.modal', function () {
    let seminarId = $("#Id").val();
    $('#parent-of-part').val(seminarId);
});
$('#centralModalChapterEdit').on('hide.bs.modal', function () {
    //alert("hide");
});
$('#centralModalChapterEdit').on('hidden.bs.modal', function () {
    $("#saveSeminarPart").trigger("reset");
    $("#new-part-id").val(0);
});

function addChapterHandler() {
    $("#chapter-modal-title").text("New Chapter");
    $("#part-name").val("");
    $("#part-order").val("");
    $("#centralModalFluidSuccessDemo").modal("show");
}
function editChapterBtnHandler() {
    console.log("editChapterBtnHandler");
}
function saveSeminarChapterHandler() {
    console.log("saveSeminarChapterHandler");
}
function deleteChapterBtnPartHandler() {
    console.log("deleteChapterBtnPartHandler");
}
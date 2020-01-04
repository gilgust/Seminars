// seminar parts CRUD
$("#addPart").on("click", addPartBtnHandler);
$(".edit-part-btn").on("click", editPartBtnHandler);
$("#saveSeminarPart").on("submit", saveSeminarPartBtnHandler);
$(".delete-part-btn").on("click", deletePartPartBtnHandler);

$("#centralModalFluidSuccessDemo").on("show.bs.modal", function () {
    let seminarId = $("#Id").val();
    $("#parent-of-part").val(seminarId);
});
$("#centralModalFluidSuccessDemo").on("hidden.bs.modal",function () {
        $("#saveSeminarPart").trigger("reset");
        $("#new-part-id").val(0);
    });

function addPartBtnHandler() {
    $("#modal-title").text("New part");
    $("#part-name").val("");
    $("#part-order").val("");
    $("#centralModalFluidSuccessDemo").modal("show");
}

function editPartBtnHandler() {
    const wrapper = $(this).closest(".seminar-part"),
        partId = $(wrapper).data("id"),
        partOrder = $(wrapper).find(`#order-part-${partId}`).text(),
        partName = $(wrapper).find(`#name-part-${partId}`).text();

    $('#modal-title').text(partName);
    $("#part-name").val(partName);
    $("#part-order").val(partOrder);
    $("#new-part-id").val(partId);
    $("#centralModalFluidSuccessDemo").modal("show");
}

function saveSeminarPartBtnHandler(e) {
    e.preventDefault();
    const id = this.elements["Id"].value,
        seminarId = this.elements["SeminarId"].value,
        name = this.elements["Name"].value,
        order = this.elements["Order"].value;

    if (Number(id) === 0) 
        createSeminarPart(seminarId, name, order);
    else 
        editSeminarPart(id, seminarId, name, order);
}

//create seminar part node ready
function createSeminarPart(seminarId, name, order) {
    $.ajax({
        url: "/api/SeminarPartsAdmin",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            seminarId: seminarId,
            name: name,
            order: order
        })
    }).done(function (data) {
        //console.log(data);
        //create node ready
        createSeminarPartNode(data);
        $("#centralModalFluidSuccessDemo").modal("hide");
    }).fail(function (data) {
        console.log("some error");
        console.log(data);
    });
}

//edit seminar part node ready
function editSeminarPart(id, seminarId, name, order) {
    const data = JSON.stringify({
        id: id,
        Name: name,
        Order: order,
        SeminarId: seminarId
    });
    $.ajax({
        url: `/api/SeminarPartsAdmin/${id}`,
        contentType: "application/json",
        method: "PUT",
        data: data
    }).done(function (data) {
        //console.log(data);
        editSeminarPartNode(data);
        $("#centralModalFluidSuccessDemo").modal("hide");
    }).fail(function (data) {
        //console.log("some error");
        console.log(data);
    });
}

//delete seminar part node ready
function deletePartPartBtnHandler() {
    const wrapper = $(this).parent().parent(),
        partId = $(wrapper).data("id");

    $.ajax({
        url: `/api/SeminarPartsAdmin/${partId}`,
        contentType: "application/json",
        method: "DELETE"
    }).done(function (data) {
        deleteSeminarPartNode(data);
        $("#centralModalFluidSuccessDemo").modal("hide");
    }).fail(function (data) {
        console.log(data);
    });
}

function createSeminarPartNode(data) {
    const template = `
        <div id="seminarPart-${data.id}" class="seminar-part border p-1" style="background-color: azure; border-radius: 5px" data-seminar-id="${data.seminarId}" data-id="${data.id}">
            <div class="d-flex">
                <h4 id="name-part-${data.id}"  class="h4 col-10">${data.name}</h4>
                <p class="m-0 col">
                    <span>order : </span><span id="order-part-${data.id}">${data.order}</span>
                </p>
            </div>
            <div class="btn-group">
                <button type="button" class="edit-part-btn btn btn-sm btn-primary">edit</button>
                <button type="button" class="delete-part-btn btn btn-sm btn-danger">delete</button>
                <button type="button" class="create-chapter-btn btn btn-sm btn-primary">add chapter</button>
            </div>
            <div class="chapters-list"></div>
        </div>`;

    const parser = new DOMParser();
    const doc = parser.parseFromString(template, "text/html");
    const elem = doc.body.firstChild;

    elem.getElementsByClassName("edit-part-btn")[0].addEventListener("click", editPartBtnHandler);
    elem.getElementsByClassName("delete-part-btn")[0].addEventListener("click", deletePartPartBtnHandler);
    elem.getElementsByClassName("create-chapter-btn")[0].addEventListener("click", addChapterBtnHandler);

    const partsWrapper = document.getElementById("partsWrapper");
    partsWrapper.appendChild(elem);
}

function editSeminarPartNode(data) {
    const seminarPart = $(`#seminarPart-${data.id}`);
    $(seminarPart).find(`#order-part-${data.id}`).text(data.order);
    $(seminarPart).find(`#name-part-${data.id}`).text(data.name);
}

function deleteSeminarPartNode(data) {
    $(`#seminarPart-${data.id}`).remove();
}


// seminar chapters CRUD
$(".create-chapter-btn").on("click", addChapterBtnHandler);
$(".edit-chapter-btn").on("click", editChapterBtnHandler);
$("#saveSeminarChapter").on("submit", saveSeminarChapterBtnHandler);
$(".delete-chapter-btn").on("click", deleteChapterBtnPartHandler);

$("#centralModalChapterEdit").on("hidden.bs.modal", function () {
    $("#saveSeminarPart").trigger("reset");
    $("#new-part-id").val(0);
});

function addChapterBtnHandler() {
    $("#chapter-modal-title").text("New Chapter");
    $("#chapter-name").val("");
    $("#chapter-order").val("");

    //  SeminarId
    //  ParentPartId
    let seminarId = $("#Id").val();
    let partId = $(this).parents(".seminar-part").data("id");

    $("#new-chapter-seminar-id").val(seminarId);
    $("#new-chapter-parent-part-id").val(partId);

    $("#centralModalChapterEdit").modal("show");
}
function editChapterBtnHandler() {
    const wrapper = $(this).parents(".chapters-item"),
        seminarId = $(wrapper).data("seminar-id"),
        partId = $(wrapper).data("part-id"),
        chapterId = $(wrapper).data("chapter-id"),
        name = $(`#name-chapter-${chapterId}`).text(),
        order = $(`#order-chapter-${chapterId}`).text();

    console.log("wrapper ", wrapper );
    console.log("seminarId", seminarId);
    console.log("partId", partId);
    console.log("chapterId", chapterId);
    console.log("name", name);
    console.log("order", order);
}
function saveSeminarChapterBtnHandler(e) {
    e.preventDefault();
    const id = this.elements["Id"].value,
        seminarId = this.elements["SeminarId"].value,
        parentPartId = this.elements["ParentPartId"].value,
        name = this.elements["Name"].value,
        order = this.elements["Order"].value;

    if (Number(id) === 0) {
        createSeminarChapter(seminarId, parentPartId, name, order);
    } else {
        editSeminarChapter(id, seminarId, parentPartId, name, order);
    }
    console.log("saveSeminarChapterHandler");
}
function deleteChapterBtnPartHandler() {
    console.log("deleteChapterBtnPartHandler");
}

function createSeminarChapter(seminarId, parentPartId, name, order) {
    $.ajax({
        url: "/api/SeminarChaptersAdmin",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            parentPartId: parentPartId,
            seminarId: seminarId,
            name: name,
            order: order
        })
    })
        .done(function (data) {
            console.log(data);
            //create node NOT ready
            //createSeminarPartNode(data);
            $("#centralModalChapterEdit").modal("hide");
        })
        .fail(function (data) {
            console.log("some error");
            console.log(data);
        });
}

function editSeminarChapter(id, seminarId, parentPartId, name, order) {
    
}
var api = {
    "parts": {
        "url": "/api/Parts"
    },
    "chapters": {
        "url": "/api/Chapters"
    }
};

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

function createSeminarPart(seminarId, name, order) {
    $.ajax({
        url: api.parts.url,
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
function editSeminarPart(id, seminarId, name, order) {
    const data = JSON.stringify({
        id: id,
        Name: name,
        Order: order,
        SeminarId: seminarId
    });
    $.ajax({
        url: api.parts.url+`/${id}`,
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
function deletePartPartBtnHandler() {
    const wrapper = $(this).parent().parent(),
        partId = $(wrapper).data("id");

    $.ajax({
        url: api.parts.url + `/${partId}`,
        contentType: "application/json",
        method: "DELETE"
    }).done(function (data) {
        $(`#seminarPart-${data.id}`).remove();
    }).fail(function (data) {
        console.log(data);
    });
}

function createSeminarPartNode(data) {
    const template = `
        <div id="seminarPart-${data.id}" class="seminar-part border p-1" style="background-color: azure; border-radius: 5px" data-seminar-id="${data.seminarId}" data-id="${data.id}">
            <div class="d-flex">
                <div class="col-10">
                    <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#part-content-wrapper-${data.id}">
                        <span id="name-part-${data.id}">
                            ${data.name}
                        </span>
                    </button>
                </div>
                <p class="m-0 col">
                    <span>order : </span><span id="order-part-${data.id}">${data.order}</span>
                </p>
            </div>
            <div class="btn-group">
                <button type="button" class="edit-part-btn btn btn-sm btn-primary">edit</button>
                <button type="button" class="delete-part-btn btn btn-sm btn-danger">delete</button>
                <button type="button" class="create-chapter-btn btn btn-sm btn-primary">add chapter</button>
            </div>
            <div class="collapse" id="part-content-wrapper-${data.id}">
                <div class="chapters-list"></div>
            </div>
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

    $("#new-chapter-id").val(chapterId);
    $("#new-chapter-seminar-id").val(seminarId);
    $("#new-chapter-parent-part-id").val(partId);
    $("#chapter-name").val( name );
    $("#chapter-order").val(order);

    //console.log("wrapper ", wrapper );
    //console.log("seminarId", seminarId);
    //console.log("partId", partId);
    //console.log("chapterId", chapterId);
    //console.log("name", name);
    //console.log("order", order);

    $("#centralModalChapterEdit").modal("show");
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
    const wrapper = $(this).parents(".chapters-item"),
        chapterId = $(wrapper).data("chapter-id");

    $.ajax({
        url: api.chapters.url + `/${chapterId}`,
        contentType: "application/json",
        method: "DELETE"
    }).done(function (data) {
        console.log(data);
        $(wrapper).remove();
    }).fail(function (data) {
        console.log(data);
    });
}
 
function createSeminarChapter(seminarId, parentPartId, name, order) {
    $.ajax({
        url: api.chapters.url,
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
            createSeminarChapterNode(data);
            $("#centralModalChapterEdit").modal("hide");
        })
        .fail(function (data) {
            console.log("some error");
            console.log(data);
        });
}
function editSeminarChapter(id, seminarId, parentPartId, name, order) {
      $.ajax({
          url: api.chapters.url + `/${id}`,
        contentType: "application/json",
        method: "PUT",
          data: JSON.stringify({
            id: id,
            parentPartId: parentPartId,
            seminarId: seminarId,
            name: name,
            order: order
        })
    })
        .done(function (data) {
            editSeminarChapterNode(data);
            $("#centralModalChapterEdit").modal("hide");
        })
        .fail(function (data) {
            console.log("some error");
            console.log(data);
        });
}

function createSeminarChapterNode(data) {
    console.log(data);
    const template = `
                <div class="chapters-item my-1 p-1"
                     style="background-color: beige"
                     id="seminarChapter-${data.id}"
                     data-seminar-id="${data.seminarId}"
                     data-part-id="${data.parentPartId}"
                     data-chapter-id="${data.id}">
                    <div class="d-flex ml-1 mt-1">
                        <h5 class="h5 col-10" id="name-chapter-${data.id}">${data.name}</h5>
                        <p class="m-0 col">
                            <span>order : </span>
                            <span id="order-chapter-${data.id}">${data.order}</span>
                        </p>
                    </div>
                    <div class="btn-group">
                        <button type="button" class="edit-chapter-btn btn btn-sm btn-primary">edit</button>
                        <button type="button" class="delete-chapter-btn btn btn-sm btn-danger">delete</button>
                    </div>
                </div>`;

    const parser = new DOMParser();
    const doc = parser.parseFromString(template, "text/html");
    const elem = doc.body.firstChild;

    elem.getElementsByClassName("edit-chapter-btn")[0].addEventListener("click", editChapterBtnHandler);
    elem.getElementsByClassName("delete-chapter-btn")[0].addEventListener("click", deleteChapterBtnPartHandler);

    const part = document.getElementById(`seminarPart-${data.parentPartId}`);
    part.getElementsByClassName("chapters-list")[0].appendChild(elem);
}
function editSeminarChapterNode(data) {

    const seminarChapter = $(`#seminarChapter-${data.id}`);
    $(seminarChapter).find(`#order-chapter-${data.id}`).text(data.order);
    $(seminarChapter).find(`#name-chapter-${data.id}`).text(data.name);
}
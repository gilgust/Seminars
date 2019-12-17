$("saveSeminarPart").submit("saveSeminarPartHandler");
$("#addPart").on("click",
    function () {
        console.log('add part');
        $('#modal-title').text("New part");
        $("#part-name").val('');
        $("#part-order").val('');
        $('#centralModalFluidSuccessDemo').modal('show');

    });

$("#centralModalFluidSuccessDemo").on('show.bs.modal', function () {
    let seminarId = $("#Id").val();
    alert("show");
});

$('#centralModalFluidSuccessDemo').on('hide.bs.modal',
    function () {
        alert("hide");
    });

$('#centralModalFluidSuccessDemo').on('hidden.bs.modal',
    function () {
        alert("hidden");
    });

$('#modalSavePart').on('click',
    function () {
        $('#centralModalFluidSuccessDemo').modal('hide');
    });

function saveSeminarPartHandler(e) {
    e.preventDefault();
    let id = this.elements["Id"].value;
    let seminarId = this.elements["SeminarId"].value;
    let name = this.elements["Name"].value;
    let order = this.elements["Order"].value;

    if (id === 0) {
        CreateSeminarPart(seminarId, name, order);
    }
}

function CreateSeminarPart(seminarId, name, order) {
    $.ajax({
        url: "api/SeminarParts/",
        type: "application/json",
        method: "POST",
        data: JSON.stringify({
            seminarId: seminarId,
            name: name,
            order: order
        }),
        success: function(data) {
            console.log(data);
        }
    });
}


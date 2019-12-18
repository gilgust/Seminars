$("#addPart").on("click",
    function () {
        $('#modal-title').text("New part");
        $("#part-name").val('');
        $("#part-order").val('');
        $('#centralModalFluidSuccessDemo').modal('show');
    });

$(".edit-part-btn").on("click",
    function () {
        let wrapper = $(this).parent().parent();
        let partId = $(wrapper).data("id");
        let partOrder = $(wrapper).find("#order-part-" + partId).text();
        let partName = $(wrapper).find("#name-part-" + partId).text();
        //console.log("wrapper :", wrapper );
        //console.log("partId :", partId );
        //console.log("partOrder :", partOrder);
        //console.log("partName :", partName );

        $('#modal-title').text(partName);
        $("#part-name").val(partName);
        $("#part-order").val(partOrder);
        $("#new-part-id").val(partId);
        $('#centralModalFluidSuccessDemo').modal('show');
    });

$(".delete-part-btn").on("click",
    function () {
        console.log("delete");
    });


$("#centralModalFluidSuccessDemo").on('show.bs.modal', function () {
    let seminarId = $("#Id").val();
    $('#parent-of-part').val(seminarId);
    //alert("show");
});

$('#centralModalFluidSuccessDemo').on('hide.bs.modal',
    function () {
        //alert("hide");
    });

$('#centralModalFluidSuccessDemo').on('hidden.bs.modal',
    function () {
        $("#saveSeminarPart").trigger("reset");
        $("#new-part-id").val(0);
    });

$('#modalSavePart').on('click',
    function () {
        $('#centralModalFluidSuccessDemo').modal('hide');
    });

$("#saveSeminarPart").on("submit",
    function(e) {
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
    });

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
        })
        .fail(function(data) {
            console.log(data);
        });
}


function editSeminarPart(id, seminarId, name, order) {
    $.ajax({
            url: "/api/SeminarParts/" + id,
            contentType: "application/json",
            method: "PUT",
            data: JSON.stringify({
                    id: id,
                    Name: name,
                    Order: order,
                    SeminarId: seminarId
                
            })
        })
        .done(function (data) {

            console.log(data);
        })
        .fail(function (data) {
            console.log(data);
        });
}
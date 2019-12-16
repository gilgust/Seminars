$("saveSeminarPart").submit( "saveSeminarPartHandler");

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


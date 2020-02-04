$("#get_seminars").on("click", getSeminarsByApi);
$(".delete-seminar-btn").on("click", getSeminarsByApi);
$(".edit-seminar-btn").on("click", getSeminarsByApi);
$("#seminar-form").on("submit", SaveSeminarsByApi);

function getSeminarsByApi() {
    $.ajax({
        url: "/api/Seminars",
        contentType: "application/json",
        method: "GET"
    }).done(function (data) {
        if (data.statusCode !== 200) {
            return;
        }
        $("#seminar_wrapper").empty();

        data.forEach(function(seminar, i, arr) {
            createSeminarNode(seminar);
        });

        console.log(data);
    }).fail(function (data) {
        if (data.status !== 200) {
            console.log(data.status);
            return;
        }
        console.log(data);
    });
}

function SaveSeminarsByApi(e) {
    e.preventDefault();

    var method = "";
    var url = "";
    if (Number(this.elements.id.value) === 0) {
       method = "POST";
        url = "/api/Seminars";
    } else {
        method = "PUT";
        url = `/api/Seminars/${this.elements.id.value}`;
    }
    $.ajax({
        url: url,
        contentType: "application/json",
        method: method,
        data: JSON.stringify({
            id: this.elements.id.value,
            name: this.elements.name.value,
            slug: this.elements.slug.value,
            content: this.elements.content.value,
            excerpt: this.elements.excerpt.value
        })
    }).done(function (data) {
        console.log(data);
    }).fail(function (data) {
        console.log(data);
    });
}

function createSeminarNode(data) {
    const template = `<div data-id="${data.id}" data-name="${data.name}" id="seminar-${data.id}">
                        <div id="seminar-${data.id}-name">${data.name}</div>
                        <div id="seminar-${data.id}-slug">${data.slug}</div>
                        <div id="seminar-${data.id}-excerpt">${data.excerpt}</div>
                        <div id="seminar-${data.id}-content">${data.content}</div>
                        <button type="button" class="delete-seminar-btn">delete</button>
                        <button type="button" class="edit-seminar-btn">edit</button>
                    </div>`;
    const parser = new DOMParser();
    const doc = parser.parseFromString(template, "text/html");
    const elem = doc.body.firstChild;

    $("#seminar_wrapper").append(elem);
}

function displayMessage(message, condition) {
    console.log("displayMessage");
}
$("#get_seminars").on("click", getSeminarsByApi);

function getSeminarsByApi() {
    $.ajax({
        url: "/api/Seminars",
        contentType: "application/json",
        method: "GET"
    }).done(function(data) {
        console.log(data);
    }).fail(function(data) {
        console.log(data);
    });
}

function createNode(data) {
    const templ = `
<div></div>
`;
}
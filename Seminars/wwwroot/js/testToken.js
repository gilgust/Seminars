var tokenKey = "accessToken";

// отпавка запроса к контроллеру AccountController для получения токена
async function getTokenAsync() {

    // получаем данные формы и фомируем объект для отправки
    const formData = new FormData();
    formData.append("grant_type", "password");
    formData.append("username", document.getElementById("emailLogin").value);
    formData.append("password", document.getElementById("passwordLogin").value);

    // отправляет запрос и получаем ответ
    const response = await fetch("/token", {
        method: "POST",
        headers: { "Accept": "application/json" },
        body: formData
    });
    // получаем данные 
    const data = await response.json();
    console.log(data);

    // если запрос прошел нормально
    if (response.ok === true) {

        // изменяем содержимое и видимость блоков на странице
        document.getElementById("userName").innerText = data.username;
        document.getElementById("userInfo").style.display = "block";
        document.getElementById("loginForm").style.display = "none";
        // сохраняем в хранилище sessionStorage токен доступа
        sessionStorage.setItem(tokenKey, data.access_token);
        console.log(data.access_token);
    }
    else {
        // если произошла ошибка, из errorText получаем текст ошибки
        console.log("Error: ", response.status, data.errorText);
    }
}
//async function f() {
//    console.log("emailLogin", document.getElementById("emailLogin").value);
//    console.log("passwordLogin", document.getElementById("passwordLogin").value);
//
//    const formData = new FormData();
//    formData.set("grant_type", "password");
//    formData.set("username", document.getElementById("emailLogin").value);
//    formData.set("password", document.getElementById("passwordLogin").value);
//
//    console.log("formData", formData);
//    console.log("formData username: ", formData.get("username"));
//    console.log("formData password: ", formData.get("password"));
//
//    const response = await fetch("/token", {
//        method: "POST",
//        headers: { "Accept": "application/json" },
//        body: formData
//    });
//
//    console.log("response : ", response);
//}
// отправка запроса к контроллеру ValuesController
async function getData(url) {
    const token = sessionStorage.getItem(tokenKey);

    const response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token  // передача токена в заголовке
        }
    });
    if (response.ok === true) {

        const data = await response.json();
        alert(data);
    }
    else
        console.log("Status: ", response.status);
}

// получаем токен
document.getElementById("submitLogin").addEventListener("click", e => {

    e.preventDefault();
    getTokenAsync();
});

// условный выход - просто удаляем токен и меняем видимость блоков
document.getElementById("logOut").addEventListener("click", e => {

    e.preventDefault();
    document.getElementById("userName").innerText = "";
    document.getElementById("userInfo").style.display = "none";
    document.getElementById("loginForm").style.display = "block";
    sessionStorage.removeItem(tokenKey);
});


// кнопка получения имя пользователя  - /api/TestToken/getlogin
document.getElementById("getDataByLogin").addEventListener("click", e => {

    e.preventDefault();
    getData("/api/TestToken/getlogin");
});

// кнопка получения роли  - /api/TestToken/getrole
document.getElementById("getDataByRole").addEventListener("click", e => {

    e.preventDefault();
    getData("/api/TestToken/getrole");
});
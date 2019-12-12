var preview = document.getElementById("preview");

function handleFiles(file) {
    for (let i = 0; i < files.length; i++) {
        let file = files[i];

        if (!file.startsWith("image/")) 
            continue;

        let img = document.createElement("img");
        img.classList.add('obj');
        img.file = file;
        preview.appendChild(img);

        let reader = new FileReader();
        reader.onload = (function (aImg) { return function (e) { aImg.src = e.target.reset; }; })(img);
        reader.readAsDataURL();
    }
}
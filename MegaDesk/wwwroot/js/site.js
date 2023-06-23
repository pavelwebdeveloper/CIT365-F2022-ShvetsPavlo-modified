// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("js-selectMaterial").addEventListener("change", showMaterial)

function showMaterial()
{  


    //console.log(event.target.childNodes);
    console.log(document.getElementById("js-selectMaterial").selectedOptions[0].value);
    console.log(location.hostname);

    let selectedOption = document.getElementById("js-selectMaterial").selectedOptions[0].value;
    selectedOption = selectedOption.toLowerCase();
    document.getElementById("js-showSurfaceMaterial").src = "/images/" + selectedOption + ".jpg";
}

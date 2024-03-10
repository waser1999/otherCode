document.querySelector("h1").textContent = "Hello.";
let myImage = document.querySelector("img");
let myHeading = document.querySelector("h1");
let myButton = document.querySelector("button");

function multiply(num1, num2){
    return num1 * num2;
}

myImage.onclick = function(){
    let oldImage = myImage.getAttribute("src");
    if(oldImage === "images/firefox-logo.png"){
        myImage.setAttribute("src", "images/apple.webp");
    }else{
        myImage.setAttribute("src", "images/firefox-logo.png");
    }
}

function query(){
    let name = prompt("Please input your name.");
    localStorage.setItem("name", name);
    myHeading.textContent = "非常感谢！" + name;
}

myButton.onclick = function(){
    query();
}
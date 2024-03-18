let equation = "";
let numbers = "0123456789";
let signals = "+-*/0"
let input = document.querySelector("p");

for (let i = 0; i <= 9; i++){
    let numBtn = document.querySelector(`[value = "${i}"]`);
    numBtn.addEventListener("click", () => {
        equation += i.toString();
        if(signals.includes(equation[equation.length - 1]) || input.textContent === "0"){
            input.textContent = i.toString();
        }else{
            input.textContent += i.toString();
        }
    });
}

let addBtn = document.querySelector("#add");
addBtn.addEventListener("click", ()=>{
    if(numbers.includes(equation[equation.length - 1])){
        equation += "+";
        input.textContent = "0";
    }
});

let minusBtn = document.querySelector("#minus");
minusBtn.addEventListener("click", ()=>{
    if(numbers.includes(equation[equation.length - 1])){
        equation += "-";
        input.textContent = "0";
    }    
});

let plusBtn = document.querySelector("#plus");
plusBtn.addEventListener("click", ()=>{
    if(numbers.includes(equation[equation.length - 1])){
        equation += "*";
        input.textContent = "0";
    }
    
});

let devidedBtn = document.querySelector("#devided");
devidedBtn.addEventListener("click", ()=>{
    if(numbers.includes(equation[equation.length - 1])){
        equation += "/";
        input.textContent = "0";
    }
});

let equal = document.querySelector("#equal");
equal.addEventListener("click", ()=>{
    let result = eval(equation);
    input.textContent = result;
    equation = result.toString();
});

let clear = document.querySelector("#clearAll");
clear.addEventListener("click", ()=>{
    equation = "";
    input.textContent = "0";
});

let clearOne = document.querySelector("#clearOne");
clearOne.addEventListener("click", ()=>{
    if(input.textContent.length === 1 && numbers.includes(input.textContent)){
        input.textContent = "0";
        equation[equation.length - 1] = "0";
    }else{
        input.textContent = input.textContent.slice(0, -1);
        equation = equation.slice(0, -1);
    }
});

let dot = document.querySelector("#dot");
dot.addEventListener("click", ()=>{
    if(numbers.includes(equation[equation.length - 1])){
        input.textContent += ".";
        equation += ".";
    }
});
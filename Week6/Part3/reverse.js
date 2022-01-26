const str="sena";

/// 1- Using for loop
let result1='';
for(i=str.length;i>=0;i--){
    result1+=str.charAt(i);
   
}

console.log("Using for loop: ",result1)


//// 2- Using built in reverse function on arrays
let result2='';
const arr= str.split("");
const  reversed =arr.reverse();
result2 = reversed.join("");

console.log("Using reverse and join:",result2)


/// 3- Using recursion
function reverse(word){
    if(word.length==0)
    return "";
    else
    return reverse(word.slice(1)) + word.charAt(0); 
}
console.log("Using recursion : ",reverse(str));

///Using reduce
let result3 = [...str].reduce((accumualtor, current) => current+accumualtor);
console.log("Using reduce: ",result3)

///En optimal cozum reduce kullanmak gibi gorunuyor, execute time olarak for loop ile aralarinda cok fark olmasa da en hizli cozen o.
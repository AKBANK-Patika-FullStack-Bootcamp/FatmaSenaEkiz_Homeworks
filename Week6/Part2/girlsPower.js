function girlsPowerToplami(n){
    return (n/2)+3;

}
let result;
function girlsPower(...arr){
    arr.forEach(element => {
        result = arr.map(element => girlsPowerToplami(element))
    });
    return result;
}
const arr =[2,3,4];
console.log(girlsPower(...arr));
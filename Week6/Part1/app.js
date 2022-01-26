const kopekBakimSaati = require('./kopekBakimUtility');

const kopek = require('./kopek');
console.log("kopek adi : ",kopek.isim);
console.log("kopek boyu : ",kopek.boy);
kopekBakimSaati.kopegiTemizle()
console.log("kopek ilgi saati: ",kopek.kilo*kopekBakimSaati.kopekBakimSaati);


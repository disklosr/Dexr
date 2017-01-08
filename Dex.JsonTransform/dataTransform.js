var fs = require('fs');
var iconv = require('iconv-lite');

const filePath = "D:\\Dev\\MainRepo\\Dex\\Dex.Uwp\\Data\\pokemons.db.json"
var fileBuffer = fs.readFileSync(filePath);
var fileAsString = iconv.decode(fileBuffer, "utf8");
var obj = JSON.parse(fileAsString);

obj.forEach(elm => {
    elm.Types = [elm.Type1];
    if(elm.Type2 != "unknown"){
        elm.Types.push(elm.Type2);
    }

    delete elm.Type1;
    delete elm.Type2; 
}); 


var jsonString = JSON.stringify(obj, null, 4);
buffer = iconv.encode(jsonString, "utf8");
fs.writeFileSync(filePath, buffer);
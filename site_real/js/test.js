getAllCountryCodes().then(
    (data) => {
        console.log(data);
        for(var i in data){
    getCountryArticle(data[i]).then((dat) => {console.log(dat)})
        }
    }
)
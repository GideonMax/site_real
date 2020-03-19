getAllCountryCodes().then(
    (data) => {
        console.log(data);
        for(var i in data){
            getCountryData(data[i]).then((dat) => {console.log(dat)})
        }
    }
)


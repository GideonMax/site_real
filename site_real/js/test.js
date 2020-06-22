/*getAllCountryCodes().then(
    (data) => {
        console.log(data);
        for(var i in data){
            getCountryData(data[i]).then((dat) => {
                console.log(dat)
                console.log(dat.StackTrace)

            })
        }
    }
)*/
Geocode(45.926696,3.407534).then(console.log);
/*getCountryData("ar").then(dat => {
    console.log("yes")
    console.log(dat);
})*/

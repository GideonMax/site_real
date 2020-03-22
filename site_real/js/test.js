getAllCountryCodes().then(
    (data) => {
        console.log(data);
        for(var i in data){
            getCountryData(data[i]).then((dat) => {
                console.log(dat)
                console.log(dat.StackTrace)

            })
        }
    }
)

/*getCountryData("ar").then(dat => {
    console.log("yes")
    console.log(dat);
})*/
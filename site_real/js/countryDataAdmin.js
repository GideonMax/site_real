/**
 * 
 * @param {string} Code 
 * @param {Country} Data 
 */
export default function setCountryDataAdmin(Code, Data) {
    return fetch(`/country/setadmin/${Code}`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Data)
    });
}
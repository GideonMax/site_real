
/**
 * 
 * @param {Number} latitude 
 * @param {Number} longitude 
 * @returns {Promise<string>}
 */

function GetGeocode(latitude, longitude) {
    return fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${latitude}&lon=${longitude}`)
    .then(res=>res.json())
    .then(json=>json.address.country_code)
}
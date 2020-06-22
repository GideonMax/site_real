
/**
 * 
 * @param {Number} latitude 
 * @param {Number} longitude 
 * @returns {Promise<string>}
 */

export default function Geocode(latitude, longitude) {
    return fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${latitude}&lon=${longitude}`)
    .then(res=>res.json())
    .then(json=>json.address.country_code)
}